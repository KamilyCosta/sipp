using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class RecriaATabelaRelacionandoImoATipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbRelacionandoImoATipo",
                columns: table => new
                {
                    RelacionandoImoATipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipodeTransacaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImovelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbRelacionandoImoATipo", x => x.RelacionandoImoATipoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbRelacionandoImoATipo");
        }
    }
}
