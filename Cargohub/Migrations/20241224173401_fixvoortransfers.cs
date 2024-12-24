using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class fixvoortransfers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "items_item_id",
                table: "Transfers");

            migrationBuilder.RenameColumn(
                name: "items_amount",
                table: "Transfers",
                newName: "items_Capacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "items_Capacity",
                table: "Transfers",
                newName: "items_amount");

            migrationBuilder.AddColumn<string>(
                name: "items_item_id",
                table: "Transfers",
                type: "TEXT",
                nullable: true);
        }
    }
}
