using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v164 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.RenameColumn(
                name: "DataServiço",
                table: "TratamentoServico",
                newName: "Data");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Servicos",
                nullable: true,
                oldClrType: typeof(string));*/

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Servicos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Servicos");

            /*migrationBuilder.RenameColumn(
                name: "Data",
                table: "TratamentoServico",
                newName: "DataServiço");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Servicos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);*/
        }
    }
}
