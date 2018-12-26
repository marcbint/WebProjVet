using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v191 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_ProprietarioEndereco_Proprietarios_ProprietarioId",
            table: "ProprietarioEndereco");

            migrationBuilder.DropColumn(
            name: "PessoaTipo",
            table: "ProprietarioEndereco");

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Proprietarios",
                nullable: true);
            

            migrationBuilder.AddColumn<string>(
                name: "JnscricaoEstadual",
                table: "Proprietarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                table: "Proprietarios",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProprietarioId",
                table: "ProprietarioEndereco",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoTipo",
                table: "ProprietarioEndereco",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
              name: "FK_ProprietarioEndereco_Proprietarios_ProprietarioId",
            table: "ProprietarioEndereco",
            column: "ProprietarioId",
            principalTable: "Proprietarios",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProprietarioEndereco_Proprietarios_ProprietarioId",
                table: "ProprietarioEndereco");

            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "JnscricaoEstadual",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "EnderecoTipo",
                table: "ProprietarioEndereco");

            migrationBuilder.AlterColumn<int>(
                name: "ProprietarioId",
                table: "ProprietarioEndereco",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PessoaTipo",
                table: "ProprietarioEndereco",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProprietarioEndereco_Proprietarios_ProprietarioId",
                table: "ProprietarioEndereco",
                column: "ProprietarioId",
                principalTable: "Proprietarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
