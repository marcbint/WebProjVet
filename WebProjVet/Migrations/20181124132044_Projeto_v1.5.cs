using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tratamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    DoadoraId = table.Column<int>(nullable: false),
                    GaranhaoId = table.Column<int>(nullable: false),
                    ReceptoraId = table.Column<int>(nullable: false),
                    TratamentoTipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tratamento_Doadoras_DoadoraId",
                        column: x => x.DoadoraId,
                        principalTable: "Doadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamento_Garanhao_GaranhaoId",
                        column: x => x.GaranhaoId,
                        principalTable: "Garanhao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamento_Receptora_ReceptoraId",
                        column: x => x.ReceptoraId,
                        principalTable: "Receptora",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_DoadoraId",
                table: "Tratamento",
                column: "DoadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_GaranhaoId",
                table: "Tratamento",
                column: "GaranhaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamento_ReceptoraId",
                table: "Tratamento",
                column: "ReceptoraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tratamento");
        }
    }
}
