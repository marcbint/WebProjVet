using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v128 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animais_Proprietarios_ProprietarioId",
                table: "Animais");

            migrationBuilder.DropIndex(
                name: "IX_Animais_ProprietarioId",
                table: "Animais");

            migrationBuilder.CreateTable(
                name: "ProprietarioViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Endereco = table.Column<string>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: false),
                    Uf = table.Column<string>(nullable: false),
                    ProprietarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProprietarioViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProprietarioViewModel_Animais_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Animais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProprietarioViewModel_ProprietarioId",
                table: "ProprietarioViewModel",
                column: "ProprietarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProprietarioViewModel");

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
