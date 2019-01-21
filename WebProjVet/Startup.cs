using jsreport.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rotativa.AspNetCore;
using WebProjVet.AcessoDados;
using WebProjVet.AcessoDados.Interfaces;
using WebProjVet.AcessoDados.Repository;
using WebProjVet.AcessoDados.Servicos;
using jsreport.Binary;
using jsreport.Local;

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
                .AddJsonFile("config.json", optional: true, reloadOnChange: true); 

            //.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

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

            // Configuração do JS Report para gerar o relatório em PDF
            services.AddJsReport(new LocalReporting()
                    .UseBinary(JsReportBinary.GetBinary())
                    .AsUtility()
                    .Create());


            //Adicionar Session
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            //Mapeamento entre a interface e classe concreta
            services.AddScoped<IServicoRepository, ServicoRepository>();
            services.AddScoped<IProprietarioRepository, ProprietarioRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<IAnimalDoadoraRepository, AnimalDoadoraRepository>();
            services.AddScoped<IAnimalGaranhaoRepository, AnimalGaranhaoRepository>();
            services.AddScoped<IAnimalReceptoraRepository, AnimalReceptoraRepository>();
            services.AddScoped<ITratamentoRepository, TratamentoRepository>();
            services.AddScoped<IProprietarioEnderecoRepository, ProprietarioEnderecoRepository>();
            services.AddScoped<IFaturamentoRepository, FaturamentoRepository>();
            //services.AddScoped<IAnimalReceptoraRepository, AnimalReceptoraRepository>();
            //services.AddScoped(typeof(IRepository<AnimalReceptora>), typeof(AnimalReceptoraRepository));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            //services.AddSingleton(typeof(AnimalDoadoraRepository));

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

            //Para habilitar o uso de Session
            app.UseSession();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                

            });

            RotativaConfiguration.Setup(env);


        }
    }
}
