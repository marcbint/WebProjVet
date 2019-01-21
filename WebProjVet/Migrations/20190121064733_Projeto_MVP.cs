using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_MVP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Abqm = table.Column<string>(maxLength: 20, nullable: true),
                    AnimalTipo = table.Column<int>(nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    DataNascimento = table.Column<string>(maxLength: 10, nullable: true),
                    Mae = table.Column<string>(maxLength: 100, nullable: true),
                    Pai = table.Column<string>(maxLength: 100, nullable: true),
                    Pelagem = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doadoras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Abqm = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doadoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garanhao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Abqm = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garanhao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietarios",
                columns: table => new
                {
                    PessoaTipo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Documento = table.Column<string>(maxLength: 20, nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RazaoSocial = table.Column<string>(maxLength: 100, nullable: true),
                    InscricaoEstadual = table.Column<string>(maxLength: 20, nullable: true),
                    Telefone = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receptora",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(maxLength: 10, nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptora", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(maxLength: 10, nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Valor = table.Column<string>(nullable: false),
                    ServicoTipo = table.Column<int>(nullable: false),
                    Situacao = table.Column<int>(nullable: false),
                    ServicoUnidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tratamentos",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 20, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(maxLength: 50, nullable: true),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    TratamentoSituacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimalProprietario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AnimaisId = table.Column<int>(nullable: false),
                    ProprietarioId = table.Column<int>(nullable: true),
                    DataAquisicao = table.Column<DateTime>(nullable: false),
                    DataDesassociacao = table.Column<DateTime>(nullable: true),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    Motivo = table.Column<string>(maxLength: 300, nullable: true),
                    DataValidade = table.Column<DateTime>(nullable: false),
                    DataUltimaApuracao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalProprietario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalProprietario_Animais_AnimaisId",
                        column: x => x.AnimaisId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalProprietario_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoadoraProprietario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DoadoraId = table.Column<int>(nullable: false),
                    ProprietarioId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoadoraProprietario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoadoraProprietario_Doadoras_DoadoraId",
                        column: x => x.DoadoraId,
                        principalTable: "Doadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoadoraProprietario_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faturamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProprietarioId = table.Column<int>(nullable: false),
                    Valor = table.Column<string>(nullable: true),
                    Situacao = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Referencia = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faturamento_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GaranhaoProprietario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GaranhaoId = table.Column<int>(nullable: false),
                    ProprietarioId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GaranhaoProprietario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GaranhaoProprietario_Garanhao_GaranhaoId",
                        column: x => x.GaranhaoId,
                        principalTable: "Garanhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GaranhaoProprietario_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProprietarioEndereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EnderecoTipo = table.Column<int>(nullable: false),
                    Endereco = table.Column<string>(maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(maxLength: 600, nullable: false),
                    Complemento = table.Column<string>(maxLength: 30, nullable: true),
                    Cidade = table.Column<string>(maxLength: 100, nullable: false),
                    Uf = table.Column<string>(maxLength: 2, nullable: false),
                    ProprietarioId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: true),
                    CodigoRural = table.Column<string>(maxLength: 20, nullable: true),
                    Documento = table.Column<string>(maxLength: 20, nullable: true),
                    InscricaoEstadual = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProprietarioEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProprietarioEndereco_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalEntradas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AnimaisId = table.Column<int>(nullable: false),
                    ServicoId = table.Column<int>(nullable: false),
                    Valor = table.Column<string>(nullable: true),
                    ValorOriginal = table.Column<string>(nullable: true),
                    DataEntrada = table.Column<DateTime>(nullable: false),
                    DataSaida = table.Column<DateTime>(nullable: true),
                    DiariaSituacao = table.Column<int>(nullable: false),
                    DataCancelamento = table.Column<DateTime>(nullable: true),
                    Motivo = table.Column<string>(maxLength: 100, nullable: true),
                    Gta = table.Column<string>(maxLength: 13, nullable: true),
                    Anemia = table.Column<int>(nullable: false),
                    Mormo = table.Column<int>(nullable: false),
                    AnimalTipoCasco = table.Column<int>(nullable: false),
                    ObservacoesClinicas = table.Column<string>(maxLength: 300, nullable: true),
                    DataUltimaApuracao = table.Column<DateTime>(nullable: true),
                    DataValidade = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalEntradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalEntradas_Animais_AnimaisId",
                        column: x => x.AnimaisId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalEntradas_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalServicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AnimaisId = table.Column<int>(nullable: false),
                    ServicoId = table.Column<int>(nullable: false),
                    Valor = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    ValorOriginal = table.Column<string>(nullable: true),
                    Motivo = table.Column<string>(maxLength: 100, nullable: true),
                    DataCancelamento = table.Column<DateTime>(nullable: true),
                    ServicoSituacao = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    ValorTotal = table.Column<string>(nullable: false),
                    DoadoraId = table.Column<int>(nullable: true),
                    GaranhaoId = table.Column<int>(nullable: true),
                    ReceptoraId = table.Column<int>(nullable: true),
                    SemenId = table.Column<int>(nullable: true),
                    Faturamento = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalServicos_Animais_AnimaisId",
                        column: x => x.AnimaisId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalServicos_Animais_DoadoraId",
                        column: x => x.DoadoraId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalServicos_Animais_GaranhaoId",
                        column: x => x.GaranhaoId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalServicos_Animais_ReceptoraId",
                        column: x => x.ReceptoraId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalServicos_Animais_SemenId",
                        column: x => x.SemenId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnimalServicos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TratamentoAnimal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TratamentoId = table.Column<int>(nullable: false),
                    AnimaisId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamentoAnimal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TratamentoAnimal_Animais_AnimaisId",
                        column: x => x.AnimaisId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TratamentoAnimal_Tratamentos_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TratamentoDiaria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TratamentoId = table.Column<int>(nullable: false),
                    ServicoId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    ValorOriginal = table.Column<decimal>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamentoDiaria", x => new { x.Id, x.TratamentoId, x.ServicoId });
                    table.ForeignKey(
                        name: "FK_TratamentoDiaria_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TratamentoDiaria_Tratamentos_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TratamentoServico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TratamentoId = table.Column<int>(nullable: false),
                    ServicoId = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    ValorOriginal = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamentoServico", x => new { x.Id, x.TratamentoId, x.ServicoId });
                    table.ForeignKey(
                        name: "FK_TratamentoServico_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TratamentoServico_Tratamentos_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaturamentoEntradas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProprietarioId = table.Column<int>(nullable: false),
                    AnimaisEntradasId = table.Column<int>(nullable: false),
                    AnimaisId = table.Column<int>(nullable: false),
                    ServicoId = table.Column<int>(nullable: false),
                    Dias = table.Column<int>(nullable: false),
                    Diaria = table.Column<decimal>(nullable: false),
                    Valor = table.Column<string>(nullable: true),
                    DataFaturamento = table.Column<DateTime>(nullable: false),
                    FaturamentoId = table.Column<int>(nullable: false),
                    Referencia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturamentoEntradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaturamentoEntradas_AnimalEntradas_AnimaisEntradasId",
                        column: x => x.AnimaisEntradasId,
                        principalTable: "AnimalEntradas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoEntradas_Animais_AnimaisId",
                        column: x => x.AnimaisId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoEntradas_Faturamento_FaturamentoId",
                        column: x => x.FaturamentoId,
                        principalTable: "Faturamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoEntradas_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoEntradas_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaturamentoServicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProprietarioId = table.Column<int>(nullable: false),
                    AnimaisServicosId = table.Column<int>(nullable: false),
                    AnimaisId = table.Column<int>(nullable: false),
                    ServicoId = table.Column<int>(nullable: false),
                    Valor = table.Column<string>(nullable: true),
                    DataFaturamento = table.Column<DateTime>(nullable: false),
                    FaturamentoId = table.Column<int>(nullable: false),
                    Referencia = table.Column<string>(nullable: true),
                    DoadoraId = table.Column<int>(nullable: true),
                    GaranhaoId = table.Column<int>(nullable: true),
                    ReceptoraId = table.Column<int>(nullable: true),
                    SemenId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturamentoServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_Animais_AnimaisId",
                        column: x => x.AnimaisId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_AnimalServicos_AnimaisServicosId",
                        column: x => x.AnimaisServicosId,
                        principalTable: "AnimalServicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_Animais_DoadoraId",
                        column: x => x.DoadoraId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_Faturamento_FaturamentoId",
                        column: x => x.FaturamentoId,
                        principalTable: "Faturamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_Animais_GaranhaoId",
                        column: x => x.GaranhaoId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_Proprietarios_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Proprietarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_Animais_ReceptoraId",
                        column: x => x.ReceptoraId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_Animais_SemenId",
                        column: x => x.SemenId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoServicos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalEntradas_AnimaisId",
                table: "AnimalEntradas",
                column: "AnimaisId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalEntradas_ServicoId",
                table: "AnimalEntradas",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalProprietario_AnimaisId",
                table: "AnimalProprietario",
                column: "AnimaisId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalProprietario_ProprietarioId",
                table: "AnimalProprietario",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_AnimaisId",
                table: "AnimalServicos",
                column: "AnimaisId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_DoadoraId",
                table: "AnimalServicos",
                column: "DoadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_GaranhaoId",
                table: "AnimalServicos",
                column: "GaranhaoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_ReceptoraId",
                table: "AnimalServicos",
                column: "ReceptoraId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_SemenId",
                table: "AnimalServicos",
                column: "SemenId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_ServicoId",
                table: "AnimalServicos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_DoadoraProprietario_DoadoraId",
                table: "DoadoraProprietario",
                column: "DoadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_DoadoraProprietario_ProprietarioId",
                table: "DoadoraProprietario",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturamento_ProprietarioId",
                table: "Faturamento",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoEntradas_AnimaisEntradasId",
                table: "FaturamentoEntradas",
                column: "AnimaisEntradasId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoEntradas_AnimaisId",
                table: "FaturamentoEntradas",
                column: "AnimaisId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoEntradas_FaturamentoId",
                table: "FaturamentoEntradas",
                column: "FaturamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoEntradas_ProprietarioId",
                table: "FaturamentoEntradas",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoEntradas_ServicoId",
                table: "FaturamentoEntradas",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_AnimaisId",
                table: "FaturamentoServicos",
                column: "AnimaisId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_AnimaisServicosId",
                table: "FaturamentoServicos",
                column: "AnimaisServicosId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_DoadoraId",
                table: "FaturamentoServicos",
                column: "DoadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_FaturamentoId",
                table: "FaturamentoServicos",
                column: "FaturamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_GaranhaoId",
                table: "FaturamentoServicos",
                column: "GaranhaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_ProprietarioId",
                table: "FaturamentoServicos",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_ReceptoraId",
                table: "FaturamentoServicos",
                column: "ReceptoraId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_SemenId",
                table: "FaturamentoServicos",
                column: "SemenId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_ServicoId",
                table: "FaturamentoServicos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_GaranhaoProprietario_GaranhaoId",
                table: "GaranhaoProprietario",
                column: "GaranhaoId");

            migrationBuilder.CreateIndex(
                name: "IX_GaranhaoProprietario_ProprietarioId",
                table: "GaranhaoProprietario",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProprietarioEndereco_ProprietarioId",
                table: "ProprietarioEndereco",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoAnimal_AnimaisId",
                table: "TratamentoAnimal",
                column: "AnimaisId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoAnimal_TratamentoId",
                table: "TratamentoAnimal",
                column: "TratamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoDiaria_ServicoId",
                table: "TratamentoDiaria",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoDiaria_TratamentoId",
                table: "TratamentoDiaria",
                column: "TratamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoServico_ServicoId",
                table: "TratamentoServico",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoServico_TratamentoId",
                table: "TratamentoServico",
                column: "TratamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalProprietario");

            migrationBuilder.DropTable(
                name: "DoadoraProprietario");

            migrationBuilder.DropTable(
                name: "FaturamentoEntradas");

            migrationBuilder.DropTable(
                name: "FaturamentoServicos");

            migrationBuilder.DropTable(
                name: "GaranhaoProprietario");

            migrationBuilder.DropTable(
                name: "ProprietarioEndereco");

            migrationBuilder.DropTable(
                name: "Receptora");

            migrationBuilder.DropTable(
                name: "TratamentoAnimal");

            migrationBuilder.DropTable(
                name: "TratamentoDiaria");

            migrationBuilder.DropTable(
                name: "TratamentoServico");

            migrationBuilder.DropTable(
                name: "Doadoras");

            migrationBuilder.DropTable(
                name: "AnimalEntradas");

            migrationBuilder.DropTable(
                name: "AnimalServicos");

            migrationBuilder.DropTable(
                name: "Faturamento");

            migrationBuilder.DropTable(
                name: "Garanhao");

            migrationBuilder.DropTable(
                name: "Tratamentos");

            migrationBuilder.DropTable(
                name: "Animais");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Proprietarios");
        }
    }
}
