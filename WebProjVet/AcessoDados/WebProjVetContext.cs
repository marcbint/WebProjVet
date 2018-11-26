﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<TratamentoServicoViewModel> TratamentoServicos { get; set; }

        public WebProjVetContext(DbContextOptions<WebProjVetContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Servico>().HasKey(p => p.Id);
            modelBuilder.Entity<Proprietario>().HasKey(p => p.Id);
            modelBuilder.Entity<AnimalDoadora>().HasKey(p => p.Id);
            modelBuilder.Entity<AnimalGaranhao>().HasKey(p => p.Id);
            modelBuilder.Entity<AnimalReceptora>().ToTable("Receptora");
            modelBuilder.Entity<AnimalReceptora>().HasKey(p => p.Id);
            modelBuilder.Entity<Tratamento>().HasKey(p => p.Id);
            modelBuilder.Entity<TratamentoServicoViewModel>().ToTable("TratamentoServico");
            modelBuilder.Entity<TratamentoServicoViewModel>().HasKey(p => p.Id);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

        public DbSet<WebProjVet.Models.Tratamento> Tratamento { get; set; }


    }
}
