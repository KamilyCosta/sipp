using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIPP.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Pessoa");
        }
    }
}
