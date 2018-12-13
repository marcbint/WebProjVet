using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v165 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropColumn(
                 name: "DataAtualizacao",
                 table: "Tratamentos");

             migrationBuilder.DropColumn(
                 name: "Descricao",
                 table: "Servicos");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Tratamentos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Servicos",
                nullable: true);*/
        }
    }
}
