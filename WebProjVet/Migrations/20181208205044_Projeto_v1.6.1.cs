using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v161 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TratamentoServico",
                table: "TratamentoServico");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TratamentoServico",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TratamentoServico",
                table: "TratamentoServico",
                columns: new[] { "Id", "TratamentoId", "ServicoId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TratamentoServico",
                table: "TratamentoServico");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TratamentoServico",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TratamentoServico",
                table: "TratamentoServico",
                column: "Id");
        }
    }
}
