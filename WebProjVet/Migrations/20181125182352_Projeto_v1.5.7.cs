using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v157 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
  

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Receptora",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Receptora",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Receptora");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Receptora",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Abqm",
                table: "Receptora",
                nullable: true);
        }
    }
}
