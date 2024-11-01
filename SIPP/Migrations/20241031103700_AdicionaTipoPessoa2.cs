using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTipoPessoa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoPessoa",
                columns: table => new
                {
                    TipoPessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPessoa", x => x.TipoPessoaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoPessoa");
        }
    }
}
