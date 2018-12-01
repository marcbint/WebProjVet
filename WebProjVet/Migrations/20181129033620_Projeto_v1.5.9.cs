using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProjVet.Migrations
{
    public partial class Projeto_v159 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tratamento_Doadoras_DoadoraId",
                table: "Tratamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Tratamento_Garanhao_GaranhaoId",
                table: "Tratamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Tratamento_Receptora_ReceptoraId",
                table: "Tratamento");

            migrationBuilder.DropForeignKey(
                name: "FK_TratamentoServico_Tratamento_TratamentoId",
                table: "TratamentoServico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tratamento",
                table: "Tratamento");

            migrationBuilder.RenameTable(
                name: "Tratamento",
                newName: "Tratamentos");

            migrationBuilder.RenameIndex(
                name: "IX_Tratamento_ReceptoraId",
                table: "Tratamentos",
                newName: "IX_Tratamentos_ReceptoraId");

            migrationBuilder.RenameIndex(
                name: "IX_Tratamento_GaranhaoId",
                table: "Tratamentos",
                newName: "IX_Tratamentos_GaranhaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tratamento_DoadoraId",
                table: "Tratamentos",
                newName: "IX_Tratamentos_DoadoraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tratamentos",
                table: "Tratamentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tratamentos_Doadoras_DoadoraId",
                table: "Tratamentos",
                column: "DoadoraId",
                principalTable: "Doadoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tratamentos_Garanhao_GaranhaoId",
                table: "Tratamentos",
                column: "GaranhaoId",
                principalTable: "Garanhao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tratamentos_Receptora_ReceptoraId",
                table: "Tratamentos",
                column: "ReceptoraId",
                principalTable: "Receptora",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TratamentoServico_Tratamentos_TratamentoId",
                table: "TratamentoServico",
                column: "TratamentoId",
                principalTable: "Tratamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tratamentos_Doadoras_DoadoraId",
                table: "Tratamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tratamentos_Garanhao_GaranhaoId",
                table: "Tratamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Tratamentos_Receptora_ReceptoraId",
                table: "Tratamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_TratamentoServico_Tratamentos_TratamentoId",
                table: "TratamentoServico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tratamentos",
                table: "Tratamentos");

            migrationBuilder.RenameTable(
                name: "Tratamentos",
                newName: "Tratamento");

            migrationBuilder.RenameIndex(
                name: "IX_Tratamentos_ReceptoraId",
                table: "Tratamento",
                newName: "IX_Tratamento_ReceptoraId");

            migrationBuilder.RenameIndex(
                name: "IX_Tratamentos_GaranhaoId",
                table: "Tratamento",
                newName: "IX_Tratamento_GaranhaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Tratamentos_DoadoraId",
                table: "Tratamento",
                newName: "IX_Tratamento_DoadoraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tratamento",
                table: "Tratamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tratamento_Doadoras_DoadoraId",
                table: "Tratamento",
                column: "DoadoraId",
                principalTable: "Doadoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tratamento_Garanhao_GaranhaoId",
                table: "Tratamento",
                column: "GaranhaoId",
                principalTable: "Garanhao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tratamento_Receptora_ReceptoraId",
                table: "Tratamento",
                column: "ReceptoraId",
                principalTable: "Receptora",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TratamentoServico_Tratamento_TratamentoId",
                table: "TratamentoServico",
                column: "TratamentoId",
                principalTable: "Tratamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
