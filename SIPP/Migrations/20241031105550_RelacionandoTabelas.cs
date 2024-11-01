using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class RelacionandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "TipoPessoa");

            migrationBuilder.RenameColumn(
                name: "Descricacao",
                table: "TipoPessoa",
                newName: "Descricao");

            migrationBuilder.AddColumn<Guid>(
                name: "TipoPessoaId",
                table: "Pessoa",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_TipoPessoaId",
                table: "Pessoa",
                column: "TipoPessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_TipoPessoa_TipoPessoaId",
                table: "Pessoa",
                column: "TipoPessoaId",
                principalTable: "TipoPessoa",
                principalColumn: "TipoPessoaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_TipoPessoa_TipoPessoaId",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_TipoPessoaId",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "TipoPessoaId",
                table: "Pessoa");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TipoPessoa",
                newName: "Descricacao");

            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId",
                table: "TipoPessoa",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
