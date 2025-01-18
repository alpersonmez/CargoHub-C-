using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class AddShipmentOrderRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_shipment_id",
                table: "Orders",
                column: "shipment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shipments_shipment_id",
                table: "Orders",
                column: "shipment_id",
                principalTable: "Shipments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shipments_shipment_id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_shipment_id",
                table: "Orders");
        }
    }
}
