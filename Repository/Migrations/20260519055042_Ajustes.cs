using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Ajustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesenvolvedorLinguagemProgramacao_Desenvolvedores_DesenvolvedoresId",
                table: "DesenvolvedorLinguagemProgramacao");

            migrationBuilder.DropForeignKey(
                name: "FK_DesenvolvedorLinguagemProgramacao_Linguagens_LinguagensId",
                table: "DesenvolvedorLinguagemProgramacao");

            migrationBuilder.RenameColumn(
                name: "LinguagensId",
                table: "DesenvolvedorLinguagemProgramacao",
                newName: "LinguagemId");

            migrationBuilder.RenameColumn(
                name: "DesenvolvedoresId",
                table: "DesenvolvedorLinguagemProgramacao",
                newName: "DesenvolvedorId");

            migrationBuilder.RenameIndex(
                name: "IX_DesenvolvedorLinguagemProgramacao_LinguagensId",
                table: "DesenvolvedorLinguagemProgramacao",
                newName: "IX_DesenvolvedorLinguagemProgramacao_LinguagemId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesenvolvedorLinguagemProgramacao_Desenvolvedores_DesenvolvedorId",
                table: "DesenvolvedorLinguagemProgramacao",
                column: "DesenvolvedorId",
                principalTable: "Desenvolvedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DesenvolvedorLinguagemProgramacao_Linguagens_LinguagemId",
                table: "DesenvolvedorLinguagemProgramacao",
                column: "LinguagemId",
                principalTable: "Linguagens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesenvolvedorLinguagemProgramacao_Desenvolvedores_DesenvolvedorId",
                table: "DesenvolvedorLinguagemProgramacao");

            migrationBuilder.DropForeignKey(
                name: "FK_DesenvolvedorLinguagemProgramacao_Linguagens_LinguagemId",
                table: "DesenvolvedorLinguagemProgramacao");

            migrationBuilder.RenameColumn(
                name: "LinguagemId",
                table: "DesenvolvedorLinguagemProgramacao",
                newName: "LinguagensId");

            migrationBuilder.RenameColumn(
                name: "DesenvolvedorId",
                table: "DesenvolvedorLinguagemProgramacao",
                newName: "DesenvolvedoresId");

            migrationBuilder.RenameIndex(
                name: "IX_DesenvolvedorLinguagemProgramacao_LinguagemId",
                table: "DesenvolvedorLinguagemProgramacao",
                newName: "IX_DesenvolvedorLinguagemProgramacao_LinguagensId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesenvolvedorLinguagemProgramacao_Desenvolvedores_DesenvolvedoresId",
                table: "DesenvolvedorLinguagemProgramacao",
                column: "DesenvolvedoresId",
                principalTable: "Desenvolvedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DesenvolvedorLinguagemProgramacao_Linguagens_LinguagensId",
                table: "DesenvolvedorLinguagemProgramacao",
                column: "LinguagensId",
                principalTable: "Linguagens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
