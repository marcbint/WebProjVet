using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Abqm",
                table: "Animais",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Abqm",
                table: "Animais",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
