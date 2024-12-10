using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Item_lines",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Item_lines",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Item_lines",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Item_lines",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Item_lines",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Item_lines",
                newName: "id");
        }
    }
}
