using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class IniatilCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    address = table.Column<string>(type: "TEXT", nullable: true),
                    city = table.Column<string>(type: "TEXT", nullable: true),
                    zip_code = table.Column<string>(type: "TEXT", nullable: true),
                    province = table.Column<string>(type: "TEXT", nullable: true),
                    country = table.Column<string>(type: "TEXT", nullable: true),
                    contact_name = table.Column<string>(type: "TEXT", nullable: true),
                    contact_phone = table.Column<string>(type: "TEXT", nullable: true),
                    contact_email = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    item_id = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    item_reference = table.Column<string>(type: "TEXT", nullable: true),
                    locations = table.Column<string>(type: "TEXT", nullable: true),
                    total_on_hand = table.Column<int>(type: "INTEGER", nullable: true),
                    total_expected = table.Column<int>(type: "INTEGER", nullable: true),
                    total_ordered = table.Column<int>(type: "INTEGER", nullable: true),
                    total_allocated = table.Column<int>(type: "INTEGER", nullable: true),
                    total_available = table.Column<int>(type: "INTEGER", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Item_lines",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_lines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ItemGroups",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGroups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    warehouse_id = table.Column<int>(type: "INTEGER", nullable: true),
                    code = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    source_id = table.Column<int>(type: "INTEGER", nullable: true),
                    order_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    request_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    reference = table.Column<string>(type: "TEXT", nullable: true),
                    reference_extra = table.Column<string>(type: "TEXT", nullable: true),
                    order_status = table.Column<string>(type: "TEXT", nullable: true),
                    notes = table.Column<string>(type: "TEXT", nullable: true),
                    shipping_notes = table.Column<string>(type: "TEXT", nullable: true),
                    picking_notes = table.Column<string>(type: "TEXT", nullable: true),
                    warehouse_id = table.Column<int>(type: "INTEGER", nullable: true),
                    ship_to = table.Column<string>(type: "TEXT", nullable: true),
                    bill_to = table.Column<string>(type: "TEXT", nullable: true),
                    shipment_id = table.Column<int>(type: "INTEGER", nullable: true),
                    total_amount = table.Column<double>(type: "REAL", nullable: true),
                    total_discount = table.Column<double>(type: "REAL", nullable: true),
                    total_tax = table.Column<double>(type: "REAL", nullable: true),
                    total_surcharge = table.Column<double>(type: "REAL", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    order_id = table.Column<int>(type: "INTEGER", nullable: true),
                    source_id = table.Column<int>(type: "INTEGER", nullable: true),
                    order_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    request_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    shipment_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    shipment_type = table.Column<string>(type: "TEXT", nullable: true),
                    shipment_status = table.Column<string>(type: "TEXT", nullable: true),
                    notes = table.Column<string>(type: "TEXT", nullable: true),
                    carrier_code = table.Column<string>(type: "TEXT", nullable: true),
                    carrier_description = table.Column<string>(type: "TEXT", nullable: true),
                    service_code = table.Column<string>(type: "TEXT", nullable: true),
                    payment_type = table.Column<string>(type: "TEXT", nullable: true),
                    transfer_mode = table.Column<string>(type: "TEXT", nullable: true),
                    total_package_count = table.Column<int>(type: "INTEGER", nullable: true),
                    total_package_weight = table.Column<double>(type: "REAL", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    code = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    address = table.Column<string>(type: "TEXT", nullable: true),
                    address_extra = table.Column<string>(type: "TEXT", nullable: true),
                    city = table.Column<string>(type: "TEXT", nullable: true),
                    zip_code = table.Column<string>(type: "TEXT", nullable: true),
                    province = table.Column<string>(type: "TEXT", nullable: true),
                    country = table.Column<string>(type: "TEXT", nullable: true),
                    contact_name = table.Column<string>(type: "TEXT", nullable: true),
                    phone_number = table.Column<string>(type: "TEXT", nullable: true),
                    reference = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    reference = table.Column<string>(type: "TEXT", nullable: true),
                    transfer_from = table.Column<int>(type: "INTEGER", nullable: true),
                    transfer_to = table.Column<int>(type: "INTEGER", nullable: true),
                    transfer_status = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    code = table.Column<string>(type: "TEXT", nullable: true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    address = table.Column<string>(type: "TEXT", nullable: true),
                    zip = table.Column<string>(type: "TEXT", nullable: true),
                    city = table.Column<string>(type: "TEXT", nullable: true),
                    province = table.Column<string>(type: "TEXT", nullable: true),
                    country = table.Column<string>(type: "TEXT", nullable: true),
                    contact_name = table.Column<string>(type: "TEXT", nullable: true),
                    contact_phone = table.Column<string>(type: "TEXT", nullable: true),
                    contact_email = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    uid = table.Column<string>(type: "TEXT", nullable: false),
                    code = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    short_description = table.Column<string>(type: "TEXT", nullable: true),
                    upc_code = table.Column<string>(type: "TEXT", nullable: true),
                    model_number = table.Column<string>(type: "TEXT", nullable: true),
                    commodity_code = table.Column<string>(type: "TEXT", nullable: true),
                    ItemLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    unit_purchase_quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    unit_order_quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    pack_order_quantity = table.Column<int>(type: "INTEGER", nullable: true),
                    supplier_id = table.Column<int>(type: "INTEGER", nullable: true),
                    supplier_code = table.Column<string>(type: "TEXT", nullable: true),
                    supplier_part_number = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.uid);
                    table.ForeignKey(
                        name: "FK_Items_ItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "ItemGroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Item_lines_ItemLineId",
                        column: x => x.ItemLineId,
                        principalTable: "Item_lines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderItem",
                columns: table => new
                {
                    Orderid = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    item_id = table.Column<string>(type: "TEXT", nullable: false),
                    amount = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItem", x => new { x.Orderid, x.Id });
                    table.ForeignKey(
                        name: "FK_orderItem_Orders_Orderid",
                        column: x => x.Orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itemShipment",
                columns: table => new
                {
                    Shipmentid = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    item_id = table.Column<string>(type: "TEXT", nullable: false),
                    amount = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemShipment", x => new { x.Shipmentid, x.Id });
                    table.ForeignKey(
                        name: "FK_itemShipment_Shipments_Shipmentid",
                        column: x => x.Shipmentid,
                        principalTable: "Shipments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itemTransfers",
                columns: table => new
                {
                    Transferid = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    item_id = table.Column<string>(type: "TEXT", nullable: false),
                    amount = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemTransfers", x => new { x.Transferid, x.Id });
                    table.ForeignKey(
                        name: "FK_itemTransfers_Transfers_Transferid",
                        column: x => x.Transferid,
                        principalTable: "Transfers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemGroupId",
                table: "Items",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemLineId",
                table: "Items",
                column: "ItemLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "itemShipment");

            migrationBuilder.DropTable(
                name: "itemTransfers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "orderItem");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "ItemGroups");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "Item_lines");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
