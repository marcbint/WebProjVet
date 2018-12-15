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
    [Migration("20181214162913_Projeto_v1.7.2")]
    partial class Projeto_v172
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebProjVet.Models.Doadora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abqm");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Doadoras");
                });

            modelBuilder.Entity("WebProjVet.Models.DoadoraProprietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DoadoraId");

                    b.Property<int>("ProprietarioId");

                    b.Property<DateTime>("Data");

                    b.HasKey("Id", "DoadoraId", "ProprietarioId");

                    b.HasIndex("DoadoraId");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("DoadoraProprietario");
                });

            modelBuilder.Entity("WebProjVet.Models.Garanhao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abqm");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Garanhao");
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

                    b.Property<string>("Telefone")
                        .IsRequired();

                    b.Property<string>("Uf")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Proprietarios");
                });

            modelBuilder.Entity("WebProjVet.Models.Receptora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Receptora");
                });

            modelBuilder.Entity("WebProjVet.Models.Servico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("WebProjVet.Models.Tratamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<int>("DoadoraId");

                    b.Property<int>("GaranhaoId");

                    b.Property<int>("ReceptoraId");

                    b.Property<int>("TratamentoSituacao");

                    b.HasKey("Id");

                    b.HasIndex("DoadoraId");

                    b.HasIndex("GaranhaoId");

                    b.HasIndex("ReceptoraId");

                    b.ToTable("Tratamentos");
                });

            modelBuilder.Entity("WebProjVet.Models.TratamentoServico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TratamentoId");

                    b.Property<int>("ServicoId");

                    b.Property<DateTime>("Data");

                    b.Property<DateTime?>("DataEstadiaFim");

                    b.Property<decimal>("Valor");

                    b.Property<decimal>("ValorOriginal");

                    b.HasKey("Id", "TratamentoId", "ServicoId");

                    b.HasIndex("ServicoId");

                    b.HasIndex("TratamentoId");

                    b.ToTable("TratamentoServico");
                });

            modelBuilder.Entity("WebProjVet.Models.DoadoraProprietario", b =>
                {
                    b.HasOne("WebProjVet.Models.Doadora", "Doadora")
                        .WithMany("DoadoraProprietarios")
                        .HasForeignKey("DoadoraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany()
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.Tratamento", b =>
                {
                    b.HasOne("WebProjVet.Models.Doadora", "Doadora")
                        .WithMany()
                        .HasForeignKey("DoadoraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Garanhao", "Garanhao")
                        .WithMany()
                        .HasForeignKey("GaranhaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Receptora", "Receptora")
                        .WithMany()
                        .HasForeignKey("ReceptoraId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.TratamentoServico", b =>
                {
                    b.HasOne("WebProjVet.Models.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Tratamento", "Tratamento")
                        .WithMany("TratamentoServicos")
                        .HasForeignKey("TratamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
