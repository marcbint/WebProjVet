using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "Uf",
                table: "Proprietarios");

            migrationBuilder.AddColumn<int>(
                name: "PessoaTipo",
                table: "Proprietarios",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProprietarioEndereco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PessoaTipo = table.Column<int>(nullable: false),
                    Endereco = table.Column<string>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: false),
                    Uf = table.Column<string>(nullable: false),
                    ProprietarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProprietarioEndereco", x => x.Id);
                    table.ForeignKey(
                      name: "FK_ProprietarioEndereco_Proprietarios_ProprietarioId",
                    column: x => x.ProprietarioId,
                    principalTable: "Proprietarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                //onDelete: ReferentialAction.Cascade);


        });

            migrationBuilder.CreateIndex(
                name: "IX_ProprietarioEndereco_ProprietarioId",
                table: "ProprietarioEndereco",
                column: "ProprietarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProprietarioEndereco");

            migrationBuilder.DropColumn(
                name: "PessoaTipo",
                table: "Proprietarios");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Proprietarios",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Proprietarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Proprietarios",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "Proprietarios",
                nullable: false,
                defaultValue: "");
        }
    }
}
