using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caso_práctico__1_Andrés.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCantidadPersonas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadPersonas",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadPersonas",
                table: "Reservas");
        }
    }
}
