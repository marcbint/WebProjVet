using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_MVP_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimaisId1",
                table: "AnimalServicos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoadoraId1",
                table: "AnimalServicos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_AnimaisId1",
                table: "AnimalServicos",
                column: "AnimaisId1");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalServicos_DoadoraId1",
                table: "AnimalServicos",
                column: "DoadoraId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalServicos_Animais_AnimaisId1",
                table: "AnimalServicos",
                column: "AnimaisId1",
                principalTable: "Animais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalServicos_Animais_DoadoraId1",
                table: "AnimalServicos",
                column: "DoadoraId1",
                principalTable: "Animais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalServicos_Animais_AnimaisId1",
                table: "AnimalServicos");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalServicos_Animais_DoadoraId1",
                table: "AnimalServicos");

            migrationBuilder.DropIndex(
                name: "IX_AnimalServicos_AnimaisId1",
                table: "AnimalServicos");

            migrationBuilder.DropIndex(
                name: "IX_AnimalServicos_DoadoraId1",
                table: "AnimalServicos");

            migrationBuilder.DropColumn(
                name: "AnimaisId1",
                table: "AnimalServicos");

            migrationBuilder.DropColumn(
                name: "DoadoraId1",
                table: "AnimalServicos");
        }
    }
}
