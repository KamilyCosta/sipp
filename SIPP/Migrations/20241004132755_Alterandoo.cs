using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class Alterandoo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbRelacionandoImoATipo",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.AddColumn<Guid>(
                name: "RelacionandoImoATipoId",
                table: "tbRelacionandoImoATipo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbRelacionandoImoATipo",
                table: "tbRelacionandoImoATipo",
                column: "RelacionandoImoATipoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbRelacionandoImoATipo_TipodeTransacaoId",
                table: "tbRelacionandoImoATipo",
                column: "TipodeTransacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbRelacionandoImoATipo",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.DropIndex(
                name: "IX_tbRelacionandoImoATipo_TipodeTransacaoId",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.DropColumn(
                name: "RelacionandoImoATipoId",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbRelacionandoImoATipo",
                table: "tbRelacionandoImoATipo",
                columns: new[] { "TipodeTransacaoId", "ImovelId" });
        }
    }
}
