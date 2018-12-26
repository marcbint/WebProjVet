using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v192 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.RenameColumn(
                name: "JnscricaoEstadual",
                table: "Proprietarios",
                newName: "InscricaoEstadual");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InscricaoEstadual",
                table: "Proprietarios",
                newName: "JnscricaoEstadual");

            
        }
    }
}
