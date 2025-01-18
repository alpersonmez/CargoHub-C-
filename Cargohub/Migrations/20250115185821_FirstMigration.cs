using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ImportStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataImported = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportStatuses", x => x.Id);
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    uid = table.Column<string>(type: "TEXT", nullable: true),
                    code = table.Column<string>(type: "TEXT", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    short_description = table.Column<string>(type: "TEXT", nullable: true),
                    upc_code = table.Column<string>(type: "TEXT", nullable: true),
                    model_number = table.Column<string>(type: "TEXT", nullable: true),
                    commodity_code = table.Column<string>(type: "TEXT", nullable: true),
                    ItemLineid = table.Column<int>(type: "INTEGER", nullable: true),
                    item_line = table.Column<int>(type: "INTEGER", nullable: true),
                    ItemGroupid = table.Column<int>(type: "INTEGER", nullable: true),
                    item_group = table.Column<int>(type: "INTEGER", nullable: true),
                    ItemTypeid = table.Column<int>(type: "INTEGER", nullable: true),
                    item_type = table.Column<int>(type: "INTEGER", nullable: true),
                    unit_purchase_quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    unit_order_quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    pack_order_quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    supplierid = table.Column<int>(type: "INTEGER", nullable: true),
                    supplier_id = table.Column<int>(type: "INTEGER", nullable: false),
                    supplier_code = table.Column<string>(type: "TEXT", nullable: true),
                    supplier_part_number = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isdeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id);
                    table.ForeignKey(
                        name: "FK_Items_ItemGroups_ItemGroupid",
                        column: x => x.ItemGroupid,
                        principalTable: "ItemGroups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_ItemTypeid",
                        column: x => x.ItemTypeid,
                        principalTable: "ItemTypes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Items_Item_lines_ItemLineid",
                        column: x => x.ItemLineid,
                        principalTable: "Item_lines",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Items_Supplier_supplierid",
                        column: x => x.supplierid,
                        principalTable: "Supplier",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<string>(type: "TEXT", nullable: true),
                    amount = table.Column<int>(type: "INTEGER", nullable: false),
                    StockType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShipmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    TransferId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Transfers_TransferId",
                        column: x => x.TransferId,
                        principalTable: "Transfers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemGroupid",
                table: "Items",
                column: "ItemGroupid");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemLineid",
                table: "Items",
                column: "ItemLineid");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeid",
                table: "Items",
                column: "ItemTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Items_supplierid",
                table: "Items",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_OrderId",
                table: "Stocks",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ShipmentId",
                table: "Stocks",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_TransferId",
                table: "Stocks",
                column: "TransferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ImportStatuses");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "ItemGroups");

            migrationBuilder.DropTable(
                name: "ItemTypes");

            migrationBuilder.DropTable(
                name: "Item_lines");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Transfers");
        }
    }
}
