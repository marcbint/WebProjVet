using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v174 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoDiaria_ServicoId",
                table: "TratamentoDiaria",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoDiaria_TratamentoId",
                table: "TratamentoDiaria",
                column: "TratamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TratamentoDiaria");
        }
    }
}
