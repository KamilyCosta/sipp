using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class Alterandooo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbRelacionandoImoATipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    table.ForeignKey(
                        name: "FK_tbRelacionandoImoATipo_tbImovel_ImovelId",
                        column: x => x.ImovelId,
                        principalTable: "tbImovel",
                        principalColumn: "ImovelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbRelacionandoImoATipo_tbTipodeTransacao_TipodeTransacaoId",
                        column: x => x.TipodeTransacaoId,
                        principalTable: "tbTipodeTransacao",
                        principalColumn: "TipodeTransacaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbRelacionandoImoATipo_ImovelId",
                table: "tbRelacionandoImoATipo",
                column: "ImovelId");

            migrationBuilder.CreateIndex(
                name: "IX_tbRelacionandoImoATipo_TipodeTransacaoId",
                table: "tbRelacionandoImoATipo",
                column: "TipodeTransacaoId");
        }
    }
}
