﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebProjVet.AcessoDados;

namespace WebProjVet.Migrations
{
    [DbContext(typeof(WebProjVetContext))]
    partial class WebProjVetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebProjVet.Models.Animais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abqm")
                        .HasMaxLength(20);

                    b.Property<int>("AnimalTipo");

                    b.Property<string>("DataNascimento")
                        .HasMaxLength(10);

                    b.Property<string>("Mae")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Pai")
                        .HasMaxLength(100);

                    b.Property<string>("Pelagem")
                        .HasMaxLength(50);

                    b.Property<int>("Situacao");

                    b.HasKey("Id");

                    b.ToTable("Animais");
                });

            modelBuilder.Entity("WebProjVet.Models.AnimaisEntrada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Anemia");

                    b.Property<int>("AnimaisId");

                    b.Property<int>("AnimalTipoCasco");

                    b.Property<DateTime?>("DataCancelamento");

                    b.Property<DateTime>("DataEntrada");

                    b.Property<DateTime?>("DataSaida");

                    b.Property<DateTime?>("DataUltimaApuracao");

                    b.Property<DateTime?>("DataValidade");

                    b.Property<int>("DiariaSituacao");

                    b.Property<string>("Gta")
                        .HasMaxLength(13);

                    b.Property<int>("Mormo");

                    b.Property<string>("Motivo")
                        .HasMaxLength(100);

                    b.Property<string>("ObservacoesClinicas")
                        .HasMaxLength(300);

                    b.Property<int>("ServicoId");

                    b.Property<string>("Valor");

                    b.Property<string>("ValorOriginal");

                    b.HasKey("Id");

                    b.HasIndex("AnimaisId");

                    b.HasIndex("ServicoId");

                    b.ToTable("AnimalEntradas");
                });

            modelBuilder.Entity("WebProjVet.Models.AnimaisProprietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimaisId");

                    b.Property<DateTime>("DataAquisicao");

                    b.Property<DateTime?>("DataDesassociacao");

                    b.Property<DateTime>("DataInclusao");

                    b.Property<DateTime?>("DataUltimaApuracao");

                    b.Property<DateTime>("DataValidade");

                    b.Property<string>("Motivo")
                        .HasMaxLength(300);

                    b.Property<int>("ProprietarioId");

                    b.HasKey("Id");

                    b.HasIndex("AnimaisId");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("AnimalProprietario");
                });

            modelBuilder.Entity("WebProjVet.Models.AnimaisServicos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimaisId");

                    b.Property<DateTime>("Data");

                    b.Property<DateTime?>("DataCancelamento");

                    b.Property<string>("DoadoraId");

                    b.Property<string>("Faturamento")
                        .HasMaxLength(1);

                    b.Property<string>("GaranhaoId");

                    b.Property<string>("Motivo")
                        .HasMaxLength(100);

                    b.Property<int>("Quantidade");

                    b.Property<string>("ReceptoraId");

                    b.Property<string>("SemenId");

                    b.Property<int>("ServicoId");

                    b.Property<int>("ServicoSituacao");

                    b.Property<string>("Valor");

                    b.Property<string>("ValorOriginal");

                    b.Property<string>("ValorTotal")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AnimaisId");

                    b.HasIndex("ServicoId");

                    b.ToTable("AnimalServicos");
                });

            modelBuilder.Entity("WebProjVet.Models.Doadora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abqm")
                        .HasMaxLength(20);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Doadoras");
                });

            modelBuilder.Entity("WebProjVet.Models.DoadoraProprietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("DoadoraId");

                    b.Property<int>("ProprietarioId");

                    b.HasKey("Id");

                    b.HasIndex("DoadoraId");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("DoadoraProprietario");
                });

            modelBuilder.Entity("WebProjVet.Models.Faturamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("ProprietarioId");

                    b.Property<string>("Referencia")
                        .IsRequired();

                    b.Property<int>("Situacao");

                    b.Property<string>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Faturamento");
                });

            modelBuilder.Entity("WebProjVet.Models.FaturamentoEntradas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimaisEntradasId");

                    b.Property<int>("AnimaisId");

                    b.Property<DateTime>("DataFaturamento");

                    b.Property<decimal>("Diaria");

                    b.Property<int>("Dias");

                    b.Property<int>("FaturamentoId");

                    b.Property<int>("ProprietarioId");

                    b.Property<string>("Referencia");

                    b.Property<int>("ServicoId");

                    b.Property<string>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("AnimaisEntradasId");

                    b.HasIndex("AnimaisId");

                    b.HasIndex("FaturamentoId");

                    b.HasIndex("ProprietarioId");

                    b.HasIndex("ServicoId");

                    b.ToTable("FaturamentoEntradas");
                });

            modelBuilder.Entity("WebProjVet.Models.FaturamentoServicos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimaisId");

                    b.Property<int>("AnimaisServicosId");

                    b.Property<DateTime>("DataFaturamento");

                    b.Property<string>("DoadoraId");

                    b.Property<int>("FaturamentoId");

                    b.Property<string>("GaranhaoId");

                    b.Property<int>("ProprietarioId");

                    b.Property<string>("ReceptoraId");

                    b.Property<string>("Referencia");

                    b.Property<string>("SemenId");

                    b.Property<int>("ServicoId");

                    b.Property<string>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("AnimaisId");

                    b.HasIndex("AnimaisServicosId");

                    b.HasIndex("FaturamentoId");

                    b.HasIndex("ProprietarioId");

                    b.HasIndex("ServicoId");

                    b.ToTable("FaturamentoServicos");
                });

            modelBuilder.Entity("WebProjVet.Models.Garanhao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abqm")
                        .HasMaxLength(20);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Garanhao");
                });

            modelBuilder.Entity("WebProjVet.Models.GaranhaoProprietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("GaranhaoId");

                    b.Property<int>("ProprietarioId");

                    b.HasKey("Id");

                    b.HasIndex("GaranhaoId");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("GaranhaoProprietario");
                });

            modelBuilder.Entity("WebProjVet.Models.Proprietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("InscricaoEstadual")
                        .HasMaxLength(20);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("PessoaTipo");

                    b.Property<string>("RazaoSocial")
                        .HasMaxLength(100);

                    b.Property<int>("Situacao");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Proprietarios");
                });

            modelBuilder.Entity("WebProjVet.Models.ProprietarioEndereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(600);

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("CodigoRural")
                        .HasMaxLength(20);

                    b.Property<string>("Complemento")
                        .HasMaxLength(30);

                    b.Property<string>("Documento")
                        .HasMaxLength(20);

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("EnderecoTipo");

                    b.Property<string>("InscricaoEstadual")
                        .HasMaxLength(20);

                    b.Property<string>("Nome")
                        .HasMaxLength(50);

                    b.Property<int>("ProprietarioId");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("ProprietarioEndereco");
                });

            modelBuilder.Entity("WebProjVet.Models.Receptora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Nome")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Receptora");
                });

            modelBuilder.Entity("WebProjVet.Models.Servico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("ServicoTipo");

                    b.Property<int>("ServicoUnidade");

                    b.Property<int>("Situacao");

                    b.Property<string>("Valor")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Servicos");
                });

            modelBuilder.Entity("WebProjVet.Models.Tratamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<string>("Codigo")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<int>("TratamentoSituacao");

                    b.HasKey("Id");

                    b.ToTable("Tratamentos");
                });

            modelBuilder.Entity("WebProjVet.Models.TratamentoAnimal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimaisId");

                    b.Property<DateTime>("Data");

                    b.Property<int>("TratamentoId");

                    b.HasKey("Id");

                    b.HasIndex("AnimaisId");

                    b.HasIndex("TratamentoId");

                    b.ToTable("TratamentoAnimal");
                });

            modelBuilder.Entity("WebProjVet.Models.TratamentoDiaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TratamentoId");

                    b.Property<int>("ServicoId");

                    b.Property<DateTime?>("DataFim");

                    b.Property<DateTime>("DataInicio");

                    b.Property<decimal>("Valor");

                    b.Property<decimal>("ValorOriginal");

                    b.HasKey("Id", "TratamentoId", "ServicoId");

                    b.HasIndex("ServicoId");

                    b.HasIndex("TratamentoId");

                    b.ToTable("TratamentoDiaria");
                });

            modelBuilder.Entity("WebProjVet.Models.TratamentoServico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TratamentoId");

                    b.Property<int>("ServicoId");

                    b.Property<DateTime>("Data");

                    b.Property<decimal>("Valor");

                    b.Property<decimal>("ValorOriginal");

                    b.HasKey("Id", "TratamentoId", "ServicoId");

                    b.HasIndex("ServicoId");

                    b.HasIndex("TratamentoId");

                    b.ToTable("TratamentoServico");
                });

            modelBuilder.Entity("WebProjVet.Models.AnimaisEntrada", b =>
                {
                    b.HasOne("WebProjVet.Models.Animais", "Animais")
                        .WithMany("AnimaisEntradas")
                        .HasForeignKey("AnimaisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Servico", "Servico")
                        .WithMany("AnimaisEntradas")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.AnimaisProprietario", b =>
                {
                    b.HasOne("WebProjVet.Models.Animais", "Animais")
                        .WithMany("AnimaisProprietarios")
                        .HasForeignKey("AnimaisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany("AnimaisProprietarios")
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.AnimaisServicos", b =>
                {
                    b.HasOne("WebProjVet.Models.Animais", "Animais")
                        .WithMany("AnimaisServicos")
                        .HasForeignKey("AnimaisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Servico", "Servico")
                        .WithMany("AnimaisServicos")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.DoadoraProprietario", b =>
                {
                    b.HasOne("WebProjVet.Models.Doadora", "Doadora")
                        .WithMany("DoadoraProprietarios")
                        .HasForeignKey("DoadoraId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany("DoadoraProprietarios")
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.Faturamento", b =>
                {
                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany("Faturamentos")
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.FaturamentoEntradas", b =>
                {
                    b.HasOne("WebProjVet.Models.AnimaisEntrada", "AnimaisEntradas")
                        .WithMany()
                        .HasForeignKey("AnimaisEntradasId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Animais", "Animais")
                        .WithMany()
                        .HasForeignKey("AnimaisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Faturamento", "Faturamento")
                        .WithMany("FaturamentoEntradas")
                        .HasForeignKey("FaturamentoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany()
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.FaturamentoServicos", b =>
                {
                    b.HasOne("WebProjVet.Models.Animais", "Animais")
                        .WithMany()
                        .HasForeignKey("AnimaisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.AnimaisServicos", "AnimaisServicos")
                        .WithMany()
                        .HasForeignKey("AnimaisServicosId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Faturamento", "Faturamento")
                        .WithMany("FaturamentoServicos")
                        .HasForeignKey("FaturamentoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany()
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.GaranhaoProprietario", b =>
                {
                    b.HasOne("WebProjVet.Models.Garanhao", "Garanhao")
                        .WithMany("GaranhaoProprietarios")
                        .HasForeignKey("GaranhaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany("GaranhaoProprietarios")
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.ProprietarioEndereco", b =>
                {
                    b.HasOne("WebProjVet.Models.Proprietario", "Proprietario")
                        .WithMany("ProprietarioEnderecos")
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.TratamentoAnimal", b =>
                {
                    b.HasOne("WebProjVet.Models.Animais", "Animais")
                        .WithMany("TratamentoAnimais")
                        .HasForeignKey("AnimaisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Tratamento", "Tratamento")
                        .WithMany("TratamentoAnimais")
                        .HasForeignKey("TratamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebProjVet.Models.TratamentoDiaria", b =>
                {
                    b.HasOne("WebProjVet.Models.Servico", "Servico")
                        .WithMany()
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebProjVet.Models.Tratamento", "Tratamento")
                        .WithMany("TratamentoDiarias")
                        .HasForeignKey("TratamentoId")
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
