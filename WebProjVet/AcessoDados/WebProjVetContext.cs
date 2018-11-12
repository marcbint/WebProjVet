using Microsoft.EntityFrameworkCore;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados
{
    public class WebProjVetContext : DbContext
    {
        public DbSet<Servico> Servicos { get; set; }

        public WebProjVetContext(DbContextOptions<WebProjVetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servico>().HasKey(p => p.Id);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }


    }
}
