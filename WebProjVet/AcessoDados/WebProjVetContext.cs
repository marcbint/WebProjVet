﻿using Microsoft.EntityFrameworkCore;
using WebProjVet.Models;

namespace WebProjVet.AcessoDados
{
    public class WebProjVetContext : DbContext
    {
        public DbSet<Animais> Animais { get; set; }
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
        public DbSet<AnimaisProprietario> AnimaisProprietarios { get; set; }
        public DbSet<TratamentoAnimal> TratamentoAnimais { get; set; }
        public DbSet<AnimaisServicos> AnimaisServicos { get; set; }
        public DbSet<AnimaisEntrada> AnimaisEntradas { get; set; }
        public DbSet<Faturamento> Faturamentos { get; set; }
        public DbSet<FaturamentoServicos> FaturamentoServicos { get; set; }
        public DbSet<FaturamentoEntradas> FaturamentoEntradas { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        public WebProjVetContext(DbContextOptions<WebProjVetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            

            modelBuilder.Entity<Receptora>().ToTable("Receptora");
            modelBuilder.Entity<Receptora>().HasKey(p => p.Id);

            //Relacionamento Many to Many
            //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
            //https://stackoverflow.com/questions/37400828/how-to-implement-the-field-decimal5-2-in-entityframeworkcore-1-0-rc2
            //http://www.entityframeworktutorial.net/code-first/configure-property-mappings-using-fluent-api.aspx
            modelBuilder.Entity<Servico>().HasKey(p => p.Id);
            //modelBuilder.Entity<Servico>().Property(p => p.Valor)
                //.HasColumnType("decimal(16,2)")
                //.IsRequired(true);





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


            modelBuilder.Entity<TratamentoAnimal>().ToTable("TratamentoAnimal");
            modelBuilder.Entity<TratamentoAnimal>().HasKey(ta => ta.Id);
            modelBuilder.Entity<TratamentoAnimal>()
                .HasOne(ta => ta.Tratamento)
                .WithMany(t => t.TratamentoAnimais)
                .HasForeignKey(ta => ta.TratamentoId);
            modelBuilder.Entity<TratamentoAnimal>()
                .HasOne(ta => ta.Animais)
                .WithMany(a => a.TratamentoAnimais)
            .HasForeignKey(ta => ta.AnimaisId);



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


            modelBuilder.Entity<Animais>().HasKey(p => p.Id);
            //Relacionamento Many to Many entre garanhão e proprietários
            modelBuilder.Entity<AnimaisProprietario>().ToTable("AnimalProprietario");
            modelBuilder.Entity<AnimaisProprietario>().HasKey(gp => gp.Id);
            modelBuilder.Entity<AnimaisProprietario>()
                 .HasOne(gp => gp.Animais)
                .WithMany(g => g.AnimaisProprietarios)
                .HasForeignKey(gp => gp.AnimaisId);
            modelBuilder.Entity<AnimaisProprietario>()
                .HasOne(gp => gp.Proprietario)
                .WithMany(p => p.AnimaisProprietarios)
                .HasForeignKey(gp => gp.ProprietarioId);

            //Relacionamento Many to Many entre garanhão e proprietários
            /*modelBuilder.Entity<AnimaisServicos>().ToTable("AnimalServicos");
            modelBuilder.Entity<AnimaisServicos>().HasKey(sas => sas.Id);
            modelBuilder.Entity<AnimaisServicos>()
                 .HasOne(sas => sas.Animais)
                .WithMany(a => a.AnimaisServicos)
                .HasForeignKey(sas => sas.AnimaisId);
            modelBuilder.Entity<AnimaisServicos>()
                .HasOne(sas => sas.Servico)
                .WithMany(s => s.AnimaisServicos)
                .HasForeignKey(sas => sas.ServicoId);*/

            modelBuilder.Entity<AnimaisServicos>().ToTable("AnimalServicos");
            modelBuilder.Entity<AnimaisServicos>().HasKey(sas => sas.Id);
            modelBuilder.Entity<AnimaisServicos>()
                 .HasOne(sas => sas.Animais)
                .WithMany(a => a.AnimaisServicos)
                .HasForeignKey(sas => sas.AnimaisId);
            modelBuilder.Entity<AnimaisServicos>()
                .HasOne(sas => sas.Servico)
                .WithMany(s => s.AnimaisServicos)
                .HasForeignKey(sas => sas.ServicoId);

            //Relacionamento para informar animais associados no lançamento do serviço.
            modelBuilder.Entity<AnimaisServicos>()
                .HasOne(d => d.Doadora)
                .WithMany(aass => aass.DoadorasAnimaisServicos)
                .HasForeignKey(aass => aass.DoadoraId);

            modelBuilder.Entity<AnimaisServicos>()
                .HasOne(d => d.Garanhao)
                .WithMany(aass => aass.GaranhoesAnimaisServicos)
                .HasForeignKey(aass => aass.GaranhaoId);

            modelBuilder.Entity<AnimaisServicos>()
                .HasOne(d => d.Receptora)
                .WithMany(aass => aass.ReceptorasAnimaisServicos)
                .HasForeignKey(aass => aass.ReceptoraId);

            modelBuilder.Entity<AnimaisServicos>()
                .HasOne(d => d.Semen)
                .WithMany(aass => aass.SemenAnimaisServicos)
                .HasForeignKey(aass => aass.SemenId);

           

            //Relacionamento Many to Many entre Animais e serviços de diárias
            modelBuilder.Entity<AnimaisEntrada>().ToTable("AnimalEntradas");
            modelBuilder.Entity<AnimaisEntrada>().HasKey(ad => ad.Id);
            modelBuilder.Entity<AnimaisEntrada>()
                 .HasOne(ad => ad.Animais)
                .WithMany(a => a.AnimaisEntradas)
                .HasForeignKey(ad => ad.AnimaisId);
            modelBuilder.Entity<AnimaisEntrada>()
                .HasOne(ad => ad.Servico)
                .WithMany(s => s.AnimaisEntradas)
                .HasForeignKey(ad => ad.ServicoId)
                ;

            modelBuilder.Entity<Faturamento>().ToTable("Faturamento");
            modelBuilder.Entity<Faturamento>().HasKey(f => f.Id);
            modelBuilder.Entity<Faturamento>()        
                .HasMany(fs => fs.FaturamentoServicos)
                .WithOne(f => f.Faturamento);
            modelBuilder.Entity<Faturamento>()
                .HasMany(fe => fe.FaturamentoEntradas)
                .WithOne(f => f.Faturamento);


            modelBuilder.Entity<FaturamentoServicos>().HasKey(p => p.Id);
            modelBuilder.Entity<FaturamentoServicos>()
                .HasOne(f => f.Faturamento)
                .WithMany(fs => fs.FaturamentoServicos)
                .HasForeignKey(fs => fs.FaturamentoId);

            modelBuilder.Entity<FaturamentoServicos>()
                .HasOne(d => d.Doadora)
                .WithMany(aass => aass.DoadorasFaturamentoServicos)
                .HasForeignKey(aass => aass.DoadoraId);

            modelBuilder.Entity<FaturamentoServicos>()
                .HasOne(d => d.Garanhao)
                .WithMany(aass => aass.GarahoesFaturamentoServicos)
                .HasForeignKey(aass => aass.GaranhaoId);

            modelBuilder.Entity<FaturamentoServicos>()
                .HasOne(d => d.Receptora)
                .WithMany(aass => aass.ReceptorasFaturamentoServicos)
                .HasForeignKey(aass => aass.ReceptoraId);

            modelBuilder.Entity<FaturamentoServicos>()
                .HasOne(d => d.Semen)
                .WithMany(aass => aass.SemenFaturamentoServicos)
                .HasForeignKey(aass => aass.SemenId);



            modelBuilder.Entity<FaturamentoEntradas>().HasKey(p => p.Id);
            modelBuilder.Entity<FaturamentoEntradas>()
                .HasOne(f => f.Faturamento)
                .WithMany(fe => fe.FaturamentoEntradas)
                .HasForeignKey(fe => fe.FaturamentoId);

            modelBuilder.Entity<Empresa>().HasKey(p => p.Id);

            modelBuilder.Entity<Usuario>().HasKey(p => p.Id);

            /*
            modelBuilder.Entity<Servico>().HasKey(p => p.Id);
            modelBuilder.Entity<Servico>()
                .HasMany(fs => fs.FaturamentoServicos)
                .WithOne(s => s.Servico);*/

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }




    }
}
