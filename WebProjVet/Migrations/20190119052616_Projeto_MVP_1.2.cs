using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_MVP_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoadoraId",
                table: "AnimalServicos");

            migrationBuilder.AddColumn<int>(
                name: "DoadoraId1",
                table: "FaturamentoServicos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoServicos_DoadoraId1",
                table: "FaturamentoServicos",
                column: "DoadoraId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FaturamentoServicos_Animais_DoadoraId1",
                table: "FaturamentoServicos",
                column: "DoadoraId1",
                principalTable: "Animais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaturamentoServicos_Animais_DoadoraId1",
                table: "FaturamentoServicos");

            migrationBuilder.DropIndex(
                name: "IX_FaturamentoServicos_DoadoraId1",
                table: "FaturamentoServicos");

            migrationBuilder.DropColumn(
                name: "DoadoraId1",
                table: "FaturamentoServicos");

            migrationBuilder.AddColumn<string>(
                name: "DoadoraId",
                table: "AnimalServicos",
                nullable: true);
        }
    }
}
