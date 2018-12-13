using Microsoft.EntityFrameworkCore;
using WebProjVet.Models;
using WebProjVet.Models.ViewModels;

namespace WebProjVet.AcessoDados
{
    public class WebProjVetContext : DbContext
    {

        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<AnimalDoadora> Doadoras { get; set; }
        public DbSet<AnimalGaranhao> Garanhoes { get; set; }
        public DbSet<AnimalReceptora> Receptoras { get; set; }
        public DbSet<Tratamento> Tratamentos { get; set; }
        public DbSet<TratamentoServico> TratamentoServicos { get; set; }

        public WebProjVetContext(DbContextOptions<WebProjVetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proprietario>().HasKey(p => p.Id);
            modelBuilder.Entity<AnimalDoadora>().HasKey(p => p.Id);
            modelBuilder.Entity<AnimalGaranhao>().HasKey(p => p.Id);
            modelBuilder.Entity<AnimalReceptora>().ToTable("Receptora");
            modelBuilder.Entity<AnimalReceptora>().HasKey(p => p.Id);

            //Relacionamento Many to Many
            //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
            modelBuilder.Entity<Servico>().HasKey(p => p.Id);
            modelBuilder.Entity<Tratamento>().HasKey(p => p.Id);
            modelBuilder.Entity<TratamentoServico>().ToTable("TratamentoServico");
            modelBuilder.Entity<TratamentoServico>().HasKey(ts => new { ts.Id, ts.TratamentoId, ts.ServicoId });
            modelBuilder.Entity<TratamentoServico>()
                .HasOne(ts => ts.Tratamento)
                .WithMany(t => t.TratamentoServicos)
                .HasForeignKey(ts => ts.TratamentoId);
            modelBuilder.Entity<TratamentoServico>()
                .HasOne(ts => ts.Servico);
                //.WithMany(s => s.TratamentoServicos)
                //.HasForeignKey(ts => ts.ServicoId);



            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }




    }
}
