using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class AgendamentoNovo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Imoveis_ImovelId",
                table: "Agendamento");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImovelId",
                table: "Agendamento",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Imoveis_ImovelId",
                table: "Agendamento",
                column: "ImovelId",
                principalTable: "Imoveis",
                principalColumn: "ImovelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Imoveis_ImovelId",
                table: "Agendamento");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImovelId",
                table: "Agendamento",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Imoveis_ImovelId",
                table: "Agendamento",
                column: "ImovelId",
                principalTable: "Imoveis",
                principalColumn: "ImovelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
