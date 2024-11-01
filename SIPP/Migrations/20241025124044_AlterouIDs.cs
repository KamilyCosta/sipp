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
            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Agendamento");

            migrationBuilder.RenameColumn(
                name: "CorretorId",
                table: "Agendamento",
                newName: "PessoaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PessoaId",
                table: "Agendamento",
                newName: "CorretorId");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Agendamento",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
