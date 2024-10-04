using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class SimplificandoAsTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbRelacionandoImoATipo");

            migrationBuilder.DropTable(
                name: "tbTipodeTransacao");

            migrationBuilder.AddColumn<string>(
                name: "TipoDeServico",
                table: "tbImovel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoDeServico",
                table: "tbImovel");

            migrationBuilder.CreateTable(
                name: "tbRelacionandoImoATipo",
                columns: table => new
                {
                    RelacionandoImoATipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImovelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipodeTransacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbRelacionandoImoATipo", x => x.RelacionandoImoATipoId);
                });

            migrationBuilder.CreateTable(
                name: "tbTipodeTransacao",
                columns: table => new
                {
                    TipodeTransacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTipodeTransacao", x => x.TipodeTransacaoId);
                });
        }
    }
}
