using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v154 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_TratamentoServico_ServicoId",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Tratamento_TratamentoId",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_ServicoId",
                table: "Servicos");

            migrationBuilder.DropIndex(
                name: "IX_Servicos_TratamentoId",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "Servicos");

            migrationBuilder.DropColumn(
                name: "TratamentoId",
                table: "Servicos");

            migrationBuilder.CreateIndex(
                name: "IX_TratamentoServico_ServicoId",
                table: "TratamentoServico",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TratamentoServico_Servicos_ServicoId",
                table: "TratamentoServico",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TratamentoServico_Servicos_ServicoId",
                table: "TratamentoServico");

            migrationBuilder.DropIndex(
                name: "IX_TratamentoServico_ServicoId",
                table: "TratamentoServico");

            migrationBuilder.AddColumn<int>(
                name: "ServicoId",
                table: "Servicos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TratamentoId",
                table: "Servicos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_ServicoId",
                table: "Servicos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_TratamentoId",
                table: "Servicos",
                column: "TratamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_TratamentoServico_ServicoId",
                table: "Servicos",
                column: "ServicoId",
                principalTable: "TratamentoServico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Tratamento_TratamentoId",
                table: "Servicos",
                column: "TratamentoId",
                principalTable: "Tratamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
