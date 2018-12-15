using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doadoras_Proprietarios_ProprietarioId",
                table: "Doadoras");

            migrationBuilder.DropForeignKey(
                name: "FK_Proprietarios_Doadoras_ProprietarioId",
                table: "Proprietarios");

            migrationBuilder.DropIndex(
                name: "IX_Proprietarios_ProprietarioId",
                table: "Proprietarios");

            migrationBuilder.DropIndex(
                name: "IX_Doadoras_ProprietarioId",
                table: "Doadoras");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Doadoras");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEstadiaFim",
                table: "TratamentoServico",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorOriginal",
                table: "TratamentoServico",
                nullable: false,
                defaultValue: 0m);

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
                    table.PrimaryKey("PK_DoadoraProprietario", x => new { x.Id, x.DoadoraId, x.ProprietarioId });
                    table.ForeignKey(
                        name: "FK_DoadoraProprietario_Doadoras_ProprietarioId",
                        column: x => x.ProprietarioId,
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

            migrationBuilder.CreateIndex(
                name: "IX_DoadoraProprietario_ProprietarioId",
                table: "DoadoraProprietario",
                column: "ProprietarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoadoraProprietario");

            migrationBuilder.DropColumn(
                name: "DataEstadiaFim",
                table: "TratamentoServico");

            migrationBuilder.DropColumn(
                name: "ValorOriginal",
                table: "TratamentoServico");

            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Proprietarios",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Doadoras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Proprietarios_ProprietarioId",
                table: "Proprietarios",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Doadoras_ProprietarioId",
                table: "Doadoras",
                column: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doadoras_Proprietarios_ProprietarioId",
                table: "Doadoras",
                column: "ProprietarioId",
                principalTable: "Proprietarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proprietarios_Doadoras_ProprietarioId",
                table: "Proprietarios",
                column: "ProprietarioId",
                principalTable: "Doadoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
