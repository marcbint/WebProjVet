using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_MVP_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<int>(
                name: "DoadoraId",
                table: "AnimalServicos",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_DoadoraId",
                table: "AnimalServicos",
                column: "DoadoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalServicos_Animais_DoadoraId",
                table: "AnimalServicos",
                column: "DoadoraId",
                principalTable: "Animais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalServicos_Animais_DoadoraId",
                table: "AnimalServicos");

            migrationBuilder.DropIndex(
                name: "IX_AnimalServicos_DoadoraId",
                table: "AnimalServicos");

            migrationBuilder.DropColumn(
                name: "DoadoraId",
                table: "AnimalServicos");

            migrationBuilder.AddColumn<int>(
                name: "DoadoraId1",
                table: "AnimalServicos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_DoadoraId1",
                table: "AnimalServicos",
                column: "DoadoraId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalServicos_Animais_DoadoraId1",
                table: "AnimalServicos",
                column: "DoadoraId1",
                principalTable: "Animais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
