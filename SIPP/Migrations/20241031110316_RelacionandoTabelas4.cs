using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class RelacionandoTabelas4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_TipoPessoa_TipoPessoaId",
                table: "Pessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPessoa",
                table: "TipoPessoa");

            migrationBuilder.RenameTable(
                name: "TipoPessoa",
                newName: "TipoPessoas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPessoas",
                table: "TipoPessoas",
                column: "TipoPessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_TipoPessoas_TipoPessoaId",
                table: "Pessoa",
                column: "TipoPessoaId",
                principalTable: "TipoPessoas",
                principalColumn: "TipoPessoaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_TipoPessoas_TipoPessoaId",
                table: "Pessoa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoPessoas",
                table: "TipoPessoas");

            migrationBuilder.RenameTable(
                name: "TipoPessoas",
                newName: "TipoPessoa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoPessoa",
                table: "TipoPessoa",
                column: "TipoPessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_TipoPessoa_TipoPessoaId",
                table: "Pessoa",
                column: "TipoPessoaId",
                principalTable: "TipoPessoa",
                principalColumn: "TipoPessoaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
