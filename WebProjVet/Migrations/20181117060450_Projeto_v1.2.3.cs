using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proprietarios_Animais_AnimalId",
                table: "Proprietarios");

            migrationBuilder.DropIndex(
                name: "IX_Proprietarios_AnimalId",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Proprietarios");

            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Animais",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animais_ProprietarioId",
                table: "Animais",
                column: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animais_Proprietarios_ProprietarioId",
                table: "Animais",
                column: "ProprietarioId",
                principalTable: "Proprietarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animais_Proprietarios_ProprietarioId",
                table: "Animais");

            migrationBuilder.DropIndex(
                name: "IX_Animais_ProprietarioId",
                table: "Animais");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Animais");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Proprietarios",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proprietarios_AnimalId",
                table: "Proprietarios",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proprietarios_Animais_AnimalId",
                table: "Proprietarios",
                column: "AnimalId",
                principalTable: "Animais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
