using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaloGames.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarTablaEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadTotal",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "PrecioBase",
                table: "Eventos",
                newName: "Costo");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Eventos",
                newName: "Ubicacion");

            migrationBuilder.RenameColumn(
                name: "Lugar",
                table: "Eventos",
                newName: "Titulo");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Eventos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "Eventos",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Eventos",
                newName: "Lugar");

            migrationBuilder.RenameColumn(
                name: "Costo",
                table: "Eventos",
                newName: "PrecioBase");

            migrationBuilder.AddColumn<int>(
                name: "CapacidadTotal",
                table: "Eventos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
