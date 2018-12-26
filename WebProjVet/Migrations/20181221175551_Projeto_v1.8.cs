using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    table.PrimaryKey("PK_GaranhaoProprietario", x => new { x.Id, x.GaranhaoId, x.ProprietarioId });
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

            migrationBuilder.CreateIndex(
                name: "IX_GaranhaoProprietario_GaranhaoId",
                table: "GaranhaoProprietario",
                column: "GaranhaoId");

            migrationBuilder.CreateIndex(
                name: "IX_GaranhaoProprietario_ProprietarioId",
                table: "GaranhaoProprietario",
                column: "ProprietarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GaranhaoProprietario");
        }
    }
}
