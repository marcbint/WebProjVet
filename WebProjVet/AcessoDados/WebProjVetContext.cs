using Microsoft.EntityFrameworkCore;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados
{
    public class WebProjVetContext : DbContext
    {
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Animal>Animais { get; set; }

        public WebProjVetContext(DbContextOptions<WebProjVetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servico>().HasKey(p => p.Id);
            modelBuilder.Entity<Proprietario>().HasKey(p => p.Id);
            modelBuilder.Entity<Animal>().HasKey(p => p.Id);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }


    }
}
