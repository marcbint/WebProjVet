using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v151 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicoId",
                table: "Servicos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TratamentoServico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TratamentoId = table.Column<int>(nullable: false),
                    ServicoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamentoServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TratamentoServico_Tratamento_TratamentoId",
                        column: x => x.TratamentoId,
                        principalTable: "Tratamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_ServicoId",
                table: "Servicos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoServico_TratamentoId",
                table: "TratamentoServico",
                column: "TratamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_TratamentoServico_ServicoId",
                table: "Servicos",
                column: "ServicoId",
                principalTable: "TratamentoServico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_TratamentoServico_ServicoId",
                table: "Servicos");

            migrationBuilder.DropTable(
                name: "TratamentoServico");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_ServicoId",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "Servicos");
        }
    }
}
