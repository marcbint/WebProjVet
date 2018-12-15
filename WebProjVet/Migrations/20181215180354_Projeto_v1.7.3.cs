using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v173 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEstadiaFim",
                table: "TratamentoServico");

            migrationBuilder.AddColumn<int>(
                name: "ServicoTipo",
                table: "Servicos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicoTipo",
                table: "Servicos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEstadiaFim",
                table: "TratamentoServico",
                nullable: true);
        }
    }
}
