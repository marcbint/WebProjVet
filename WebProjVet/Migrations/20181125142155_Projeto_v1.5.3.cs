using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v153 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TratamentoId",
                table: "Servicos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_TratamentoId",
                table: "Servicos",
                column: "TratamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Tratamento_TratamentoId",
                table: "Servicos",
                column: "TratamentoId",
                principalTable: "Tratamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Tratamento_TratamentoId",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_TratamentoId",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "TratamentoId",
                table: "Servicos");
        }
    }
}
