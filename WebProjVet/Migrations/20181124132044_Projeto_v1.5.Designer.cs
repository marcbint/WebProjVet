﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProjVet.AcessoDados;

namespace WebProjVet.Migrations
{
    [DbContext(typeof(WebProjVetContext))]
    [Migration("20181124132044_Projeto_v1.5")]
    partial class Projeto_v15
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebProjVet.Models.AnimalDoadora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abqm");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int>("ProprietarioId");

                    b.HasKey("Id");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Doadoras");
                });

            modelBuilder.Entity("WebProjVet.Models.AnimalGaranhao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abqm");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Garanhao");
                });

            modelBuilder.Entity("WebProjVet.Models.AnimalReceptora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abqm");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Receptora");
                });

            modelBuilder.Entity("WebProjVet.Models.Proprietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cidade")
                        .IsRequired();

                    b.Property<string>("Complemento");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Endereco")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int?>("ProprietarioId");

                    b.Property<string>("Telefone")
                        .IsRequired();

                    b.Property<string>("Uf")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Proprietarios");
                });

            modelBuilder.Entity("WebProjVet.Models.Servico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<decimal>("Valor");

                    b.HasKey("Id");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("WebProjVet.Models.Tratamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataAtualizacao");

                    b.Property<DateTime?>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<int>("DoadoraId");

                    b.Property<int>("GaranhaoId");

                    b.Property<int>("ReceptoraId");

                    b.Property<int>("TratamentoTipo");

                    b.HasKey("Id");

                    b.HasIndex("DoadoraId");

                    b.HasIndex("GaranhaoId");

                    b.HasIndex("ReceptoraId");

                    b.ToTable("Tratamento");
                });

            modelBuilder.Entity("WebProjVet.Models.AnimalDoadora", b =>
                {
                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany()
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.Proprietario", b =>
                {
                    b.HasOne("WebProjVet.Models.AnimalDoadora")
                        .WithMany("Proprietarios")
                        .HasForeignKey("ProprietarioId");
                });

            modelBuilder.Entity("WebProjVet.Models.Tratamento", b =>
                {
                    b.HasOne("WebProjVet.Models.AnimalDoadora", "Doadora")
                        .WithMany()
                        .HasForeignKey("DoadoraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.AnimalGaranhao", "Garanhao")
                        .WithMany()
                        .HasForeignKey("GaranhaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.AnimalReceptora", "Receptora")
                        .WithMany()
                        .HasForeignKey("ReceptoraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
