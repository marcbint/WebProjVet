using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v122 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
