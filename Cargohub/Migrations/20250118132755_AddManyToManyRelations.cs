using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shipments_shipment_id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_shipment_id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "order_id",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "shipment_id",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderShipments",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    order_id = table.Column<int>(type: "INTEGER", nullable: false),
                    shipment_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderShipments_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderShipments_Shipments_shipment_id",
                        column: x => x.shipment_id,
                        principalTable: "Shipments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderShipments_order_id",
                table: "OrderShipments",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShipments_shipment_id",
                table: "OrderShipments",
                column: "shipment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderShipments");

            migrationBuilder.AddColumn<int>(
                name: "order_id",
                table: "Shipments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "shipment_id",
                table: "Orders",
                type: "INTEGER",
                nullable: true);

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
    }
}
