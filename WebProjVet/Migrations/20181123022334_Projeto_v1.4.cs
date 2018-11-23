using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receptora",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Abqm = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptora", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doadoras_ProprietarioId",
                table: "Doadoras",
                column: "ProprietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doadoras_Proprietarios_ProprietarioId",
                table: "Doadoras",
                column: "ProprietarioId",
                principalTable: "Proprietarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doadoras_Proprietarios_ProprietarioId",
                table: "Doadoras");

            migrationBuilder.DropTable(
                name: "Receptora");

            migrationBuilder.DropIndex(
                name: "IX_Doadoras_ProprietarioId",
                table: "Doadoras");
        }
    }
}
