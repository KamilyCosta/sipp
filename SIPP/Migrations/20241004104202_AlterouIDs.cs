using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class AlterouIDs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbClientes",
                table: "tbClientes");

            migrationBuilder.RenameTable(
                name: "tbClientes",
                newName: "tbCliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbCliente",
                table: "tbCliente",
                column: "ClienteId");

            migrationBuilder.CreateTable(
                name: "tbAgendamento",
                columns: table => new
                {
                    AgendamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorretorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAge = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraAge = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAgendamento", x => x.AgendamentoId);
                });

            migrationBuilder.CreateTable(
                name: "tbCorretor",
                columns: table => new
                {
                    CorretorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemDeTrabalho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CRECI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCorretor", x => x.CorretorId);
                });

            migrationBuilder.CreateTable(
                name: "tbImovel",
                columns: table => new
                {
                    ImovelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QntDormitorios = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QntGarragem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamanho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TamanhoAreaContuida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetodoPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbImovel", x => x.ImovelId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbAgendamento");

            migrationBuilder.DropTable(
                name: "tbCorretor");

            migrationBuilder.DropTable(
                name: "tbImovel");

            migrationBuilder.DropTable(
                name: "tbRelacionandoImoATipo");

            migrationBuilder.DropTable(
                name: "tbTipodeTransacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbCliente",
                table: "tbCliente");

            migrationBuilder.RenameTable(
                name: "tbCliente",
                newName: "tbClientes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbClientes",
                table: "tbClientes",
                column: "ClienteId");
        }
    }
}
