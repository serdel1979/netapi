using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiNet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripción",
                table: "Equipos",
                newName: "Descripcion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Equipos",
                newName: "Descripción");
        }
    }
}
