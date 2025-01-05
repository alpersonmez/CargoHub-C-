using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class fixvoorshipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "items_item_id",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "items_amount",
                table: "Shipments",
                newName: "items_Capacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "items_Capacity",
                table: "Shipments",
                newName: "items_amount");

            migrationBuilder.AddColumn<string>(
                name: "items_item_id",
                table: "Shipments",
                type: "TEXT",
                nullable: true);
        }
    }
}
