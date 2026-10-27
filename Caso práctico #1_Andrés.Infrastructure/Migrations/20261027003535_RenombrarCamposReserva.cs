using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caso_práctico__1_Andrés.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarCamposReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Persona",
                table: "Reservas",
                newName: "Encargado");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Reservas",
                newName: "Motivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Motivo",
                table: "Reservas",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Encargado",
                table: "Reservas",
                newName: "Persona");
        }
    }
}
