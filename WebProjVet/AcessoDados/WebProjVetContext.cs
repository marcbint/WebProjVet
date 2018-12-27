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
        public DbSet<GaranhaoProprietario> GaranhaoProprietarios { get; set; }
        public DbSet<ProprietarioEndereco> ProprietarioEnderecos { get; set; }

        public WebProjVetContext(DbContextOptions<WebProjVetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Proprietario>().HasKey(p => p.Id);
            //modelBuilder.Entity<Doadora>().HasKey(p => p.Id);          

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



            modelBuilder.Entity<Proprietario>().HasKey(p => p.Id);
            //Relacionamento entre proprietario e endereços
            modelBuilder.Entity<Proprietario>()
                .HasMany(c => c.ProprietarioEnderecos)
                .WithOne(e => e.Proprietario);

            modelBuilder.Entity<ProprietarioEndereco>().ToTable("ProprietarioEndereco");
            modelBuilder.Entity<ProprietarioEndereco>().HasKey(p => p.Id);
            modelBuilder.Entity<ProprietarioEndereco>()
                .HasOne(e => e.Proprietario)
                .WithMany(c => c.ProprietarioEnderecos);



            modelBuilder.Entity<Doadora>().HasKey(p => p.Id);
            //modelBuilder.Entity<Doadora>()
                //.HasMany(dp => dp.DoadoraProprietarios)
                //.WithOne(d => d.Doadora);

            //Relacionamento Many to Many entre doadora e proprietarios
            //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
            modelBuilder.Entity<DoadoraProprietario>().ToTable("DoadoraProprietario");
            //modelBuilder.Entity<DoadoraProprietario>().HasKey(dp => new { dp.Id, dp.DoadoraId, dp.ProprietarioId });
            modelBuilder.Entity<DoadoraProprietario>().HasKey(dp => dp.Id);
            modelBuilder.Entity<DoadoraProprietario>()
                 .HasOne(dp => dp.Doadora)
                .WithMany(d => d.DoadoraProprietarios)
                .HasForeignKey(dp => dp.DoadoraId);
            modelBuilder.Entity<DoadoraProprietario>()
                .HasOne(dp => dp.Proprietario)
                .WithMany(p => p.DoadoraProprietarios)
                .HasForeignKey(dp => dp.ProprietarioId);
            


            modelBuilder.Entity<Garanhao>().HasKey(p => p.Id);
            //Relacionamento com Garanhao Proprietarios
            //modelBuilder.Entity<Garanhao>()
                //.HasMany(gp => gp.GaranhaoProprietarios)
                //.WithOne(g => g.Garanhao);

            //Relacionamento Many to Many entre garanhão e proprietários
            modelBuilder.Entity<GaranhaoProprietario>().ToTable("GaranhaoProprietario");
            modelBuilder.Entity<GaranhaoProprietario>().HasKey(gp => gp.Id);
            modelBuilder.Entity<GaranhaoProprietario>()
                 .HasOne(gp => gp.Garanhao)
                .WithMany(g => g.GaranhaoProprietarios)
                .HasForeignKey(gp => gp.GaranhaoId);
            modelBuilder.Entity<GaranhaoProprietario>()
                .HasOne(gp => gp.Proprietario)
                .WithMany(p => p.GaranhaoProprietarios)
                .HasForeignKey(gp => gp.ProprietarioId);
           









            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }




    }
}
