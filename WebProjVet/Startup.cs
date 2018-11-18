using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.AcessoDados.Servicos;

namespace WebProjVet
{
    public class Startup
    {
        private IConfiguration _configuration;


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: true)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true); ;

            _configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Adicionar o Pomelo Entity Framework Core Mysql através do Nuget
            var sqlConnection = _configuration.GetConnectionString("WebProjVet");
            services.AddDbContext<WebProjVetContext>(options =>
            options.UseMySql(sqlConnection, b => b.MigrationsAssembly("WebProjVet")));



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            //Mapeamento entre a interface e classe concreta
            services.AddScoped<IServicoRepository, ServicoRepository>();
            services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton(typeof(AnimalRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
