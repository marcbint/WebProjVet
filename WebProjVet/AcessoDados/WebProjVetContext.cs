using Microsoft.EntityFrameworkCore;
using WebProjVet.Models;
using WebProjVet.Models.ViewModels;

namespace WebProjVet.AcessoDados
{
    public class WebProjVetContext : DbContext
    {

        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Doadora> Doadoras { get; set; }
        public DbSet<Garanhao> Garanhoes { get; set; }
        public DbSet<Receptora> Receptoras { get; set; }
        public DbSet<Tratamento> Tratamentos { get; set; }
        public DbSet<TratamentoServico> TratamentoServicos { get; set; }
        public DbSet<DoadoraProprietario> DoadoraProprietarios { get; set; }

        public WebProjVetContext(DbContextOptions<WebProjVetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Proprietario>().HasKey(p => p.Id);
            //modelBuilder.Entity<Doadora>().HasKey(p => p.Id);
            modelBuilder.Entity<Garanhao>().HasKey(p => p.Id);
            modelBuilder.Entity<Receptora>().ToTable("Receptora");
            modelBuilder.Entity<Receptora>().HasKey(p => p.Id);

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

            modelBuilder.Entity<TratamentoDiaria>().ToTable("TratamentoDiaria");
            modelBuilder.Entity<TratamentoDiaria>().HasKey(ts => new { ts.Id, ts.TratamentoId, ts.ServicoId });
            modelBuilder.Entity<TratamentoDiaria>()
                .HasOne(ts => ts.Tratamento)
                .WithMany(t => t.TratamentoDiarias)
                .HasForeignKey(ts => ts.TratamentoId);
            modelBuilder.Entity<TratamentoServico>()
                .HasOne(ts => ts.Servico);

            //Relacionamento Many to Many entre doadora e proprietarios
            //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
            modelBuilder.Entity<Doadora>().HasKey(p => p.Id);
            modelBuilder.Entity<Proprietario>().HasKey(p => p.Id);
            modelBuilder.Entity<DoadoraProprietario>().ToTable("DoadoraProprietario");
            modelBuilder.Entity<DoadoraProprietario>().HasKey(ts => new { ts.Id, ts.DoadoraId, ts.ProprietarioId });
            modelBuilder.Entity<DoadoraProprietario>()
                 .HasOne(ts => ts.Doadora)
                .WithMany(t => t.DoadoraProprietarios)
                .HasForeignKey(ts => ts.DoadoraId);
            modelBuilder.Entity<DoadoraProprietario>()
                .HasOne(ts => ts.Proprietario);





                 




            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }




    }
}
