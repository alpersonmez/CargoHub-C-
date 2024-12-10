using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryTable : Migration
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

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    item_id = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    item_reference = table.Column<string>(type: "TEXT", nullable: false),
                    locations = table.Column<string>(type: "TEXT", nullable: false),
                    total_on_hand = table.Column<int>(type: "INTEGER", nullable: false),
                    total_expected = table.Column<int>(type: "INTEGER", nullable: false),
                    total_ordered = table.Column<int>(type: "INTEGER", nullable: false),
                    total_allocated = table.Column<int>(type: "INTEGER", nullable: false),
                    total_available = table.Column<int>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

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
