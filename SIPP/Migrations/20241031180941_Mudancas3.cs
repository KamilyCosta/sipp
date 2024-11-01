using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class Mudancas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_TipoPessoas_TipoPessoaId",
                table: "Pessoa");

            migrationBuilder.AlterColumn<Guid>(
                name: "TipoPessoaId",
                table: "Pessoa",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_TipoPessoas_TipoPessoaId",
                table: "Pessoa",
                column: "TipoPessoaId",
                principalTable: "TipoPessoas",
                principalColumn: "TipoPessoaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_TipoPessoas_TipoPessoaId",
                table: "Pessoa");

            migrationBuilder.AlterColumn<Guid>(
                name: "TipoPessoaId",
                table: "Pessoa",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_TipoPessoas_TipoPessoaId",
                table: "Pessoa",
                column: "TipoPessoaId",
                principalTable: "TipoPessoas",
                principalColumn: "TipoPessoaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
