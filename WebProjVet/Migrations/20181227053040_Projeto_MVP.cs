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
                    Valor = table.Column<decimal>(type: "decimal", nullable: false),
                    ServicoTipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
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
                    Complemento = table.Column<string>(maxLength: 30, nullable: true),
                    Cidade = table.Column<string>(maxLength: 100, nullable: false),
                    Uf = table.Column<string>(maxLength: 2, nullable: false),
                    ProprietarioId = table.Column<int>(nullable: false)
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
                name: "Tratamentos",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 20, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    DoadoraId = table.Column<int>(nullable: false),
                    GaranhaoId = table.Column<int>(nullable: false),
                    ReceptoraId = table.Column<int>(nullable: false),
                    TratamentoSituacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Doadoras_DoadoraId",
                        column: x => x.DoadoraId,
                        principalTable: "Doadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Garanhao_GaranhaoId",
                        column: x => x.GaranhaoId,
                        principalTable: "Garanhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamentos_Receptora_ReceptoraId",
                        column: x => x.ReceptoraId,
                        principalTable: "Receptora",
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

            migrationBuilder.CreateIndex(
                name: "IX_DoadoraProprietario_DoadoraId",
                table: "DoadoraProprietario",
                column: "DoadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_DoadoraProprietario_ProprietarioId",
                table: "DoadoraProprietario",
                column: "ProprietarioId");

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
                name: "IX_TratamentoDiaria_ServicoId",
                table: "TratamentoDiaria",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoDiaria_TratamentoId",
                table: "TratamentoDiaria",
                column: "TratamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_DoadoraId",
                table: "Tratamentos",
                column: "DoadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_GaranhaoId",
                table: "Tratamentos",
                column: "GaranhaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_ReceptoraId",
                table: "Tratamentos",
                column: "ReceptoraId");

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
                name: "DoadoraProprietario");

            migrationBuilder.DropTable(
                name: "GaranhaoProprietario");

            migrationBuilder.DropTable(
                name: "ProprietarioEndereco");

            migrationBuilder.DropTable(
                name: "TratamentoDiaria");

            migrationBuilder.DropTable(
                name: "TratamentoServico");

            migrationBuilder.DropTable(
                name: "Proprietarios");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Tratamentos");

            migrationBuilder.DropTable(
                name: "Doadoras");

            migrationBuilder.DropTable(
                name: "Garanhao");

            migrationBuilder.DropTable(
                name: "Receptora");
        }
    }
}
