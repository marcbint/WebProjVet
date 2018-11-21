using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animais");

            migrationBuilder.AddColumn<int>(
                name: "ProprietarioId",
                table: "Proprietarios",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Doadoras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Abqm = table.Column<string>(nullable: true),
                    ProprietarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doadoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garanhao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Abqm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garanhao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proprietarios_ProprietarioId",
                table: "Proprietarios",
                column: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proprietarios_Doadoras_ProprietarioId",
                table: "Proprietarios",
                column: "ProprietarioId",
                principalTable: "Doadoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proprietarios_Doadoras_ProprietarioId",
                table: "Proprietarios");

            migrationBuilder.DropTable(
                name: "Doadoras");

            migrationBuilder.DropTable(
                name: "Garanhao");

            migrationBuilder.DropIndex(
                name: "IX_Proprietarios_ProprietarioId",
                table: "Proprietarios");

            migrationBuilder.DropColumn(
                name: "ProprietarioId",
                table: "Proprietarios");

            migrationBuilder.CreateTable(
                name: "Animais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Abqm = table.Column<string>(nullable: true),
                    AnimalTipo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animais", x => x.Id);
                });
        }
    }
}
