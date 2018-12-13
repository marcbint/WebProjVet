using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v162 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TratamentoTipo",
                table: "Tratamentos");

            migrationBuilder.RenameColumn(
                name: "DataRegistro",
                table: "TratamentoServico",
                newName: "DataServiço");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TratamentoServico",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TratamentoSituacao",
                table: "Tratamentos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TratamentoSituacao",
                table: "Tratamentos");

            migrationBuilder.RenameColumn(
                name: "DataServiço",
                table: "TratamentoServico",
                newName: "DataRegistro");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TratamentoServico",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "TratamentoTipo",
                table: "Tratamentos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
