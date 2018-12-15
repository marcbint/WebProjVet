using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v172 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoadoraProprietario_Doadoras_ProprietarioId",
                table: "DoadoraProprietario");

            migrationBuilder.CreateIndex(
                name: "IX_DoadoraProprietario_DoadoraId",
                table: "DoadoraProprietario",
                column: "DoadoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoadoraProprietario_Doadoras_DoadoraId",
                table: "DoadoraProprietario",
                column: "DoadoraId",
                principalTable: "Doadoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoadoraProprietario_Doadoras_DoadoraId",
                table: "DoadoraProprietario");

            migrationBuilder.DropIndex(
                name: "IX_DoadoraProprietario_DoadoraId",
                table: "DoadoraProprietario");

            migrationBuilder.AddForeignKey(
                name: "FK_DoadoraProprietario_Doadoras_ProprietarioId",
                table: "DoadoraProprietario",
                column: "ProprietarioId",
                principalTable: "Doadoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
