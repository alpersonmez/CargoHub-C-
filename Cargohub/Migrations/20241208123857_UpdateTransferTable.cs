using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransferTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Transfers_Transferid",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Transferid",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Transferid",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Transferid",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Transferid",
                table: "Items",
                column: "Transferid");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Transfers_Transferid",
                table: "Items",
                column: "Transferid",
                principalTable: "Transfers",
                principalColumn: "id");
        }
    }
}
