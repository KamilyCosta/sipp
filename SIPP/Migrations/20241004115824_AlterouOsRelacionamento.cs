using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class AlterouOsRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbRelacionandoImoATipo",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.DropColumn(
                name: "RelacionandoImoATipoId",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbRelacionandoImoATipo",
                table: "tbRelacionandoImoATipo",
                columns: new[] { "TipodeTransacaoId", "ImovelId" });

            migrationBuilder.CreateIndex(
                name: "IX_tbRelacionandoImoATipo_ImovelId",
                table: "tbRelacionandoImoATipo",
                column: "ImovelId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbRelacionandoImoATipo_tbImovel_ImovelId",
                table: "tbRelacionandoImoATipo",
                column: "ImovelId",
                principalTable: "tbImovel",
                principalColumn: "ImovelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbRelacionandoImoATipo_tbTipodeTransacao_TipodeTransacaoId",
                table: "tbRelacionandoImoATipo",
                column: "TipodeTransacaoId",
                principalTable: "tbTipodeTransacao",
                principalColumn: "TipodeTransacaoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbRelacionandoImoATipo_tbImovel_ImovelId",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.DropForeignKey(
                name: "FK_tbRelacionandoImoATipo_tbTipodeTransacao_TipodeTransacaoId",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbRelacionandoImoATipo",
                table: "tbRelacionandoImoATipo");

            migrationBuilder.DropIndex(
                name: "IX_tbRelacionandoImoATipo_ImovelId",
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
        }
    }
}
