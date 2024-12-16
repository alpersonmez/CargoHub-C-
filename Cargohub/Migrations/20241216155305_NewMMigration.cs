using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class NewMMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "carrier_code",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "carrier_description",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "payment_type",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "service_code",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "shipment_status",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "shipment_type",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "transfer_mode",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Shipments",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Shipments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Shipments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "total_package_weight",
                table: "Shipments",
                newName: "TotalPackageWeight");

            migrationBuilder.RenameColumn(
                name: "total_package_count",
                table: "Shipments",
                newName: "TotalPackageCount");

            migrationBuilder.RenameColumn(
                name: "source_id",
                table: "Shipments",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "shipment_date",
                table: "Shipments",
                newName: "TransferMode");

            migrationBuilder.RenameColumn(
                name: "request_date",
                table: "Shipments",
                newName: "ShipmentType");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Shipments",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Shipments",
                newName: "ShipmentStatus");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Shipments",
                newName: "ShipmentDate");

            migrationBuilder.RenameColumn(
                name: "reference",
                table: "Orders",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Orders",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "warehouse_id",
                table: "Orders",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Orders",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "total_tax",
                table: "Orders",
                newName: "TotalTax");

            migrationBuilder.RenameColumn(
                name: "total_surcharge",
                table: "Orders",
                newName: "TotalSurcharge");

            migrationBuilder.RenameColumn(
                name: "total_discount",
                table: "Orders",
                newName: "TotalDiscount");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "source_id",
                table: "Orders",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "shipping_notes",
                table: "Orders",
                newName: "ShippingNotes");

            migrationBuilder.RenameColumn(
                name: "shipment_id",
                table: "Orders",
                newName: "ShipmentId");

            migrationBuilder.RenameColumn(
                name: "ship_to",
                table: "Orders",
                newName: "ShipTo");

            migrationBuilder.RenameColumn(
                name: "request_date",
                table: "Orders",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "reference_extra",
                table: "Orders",
                newName: "ReferenceExtra");

            migrationBuilder.RenameColumn(
                name: "picking_notes",
                table: "Orders",
                newName: "PickingNotes");

            migrationBuilder.RenameColumn(
                name: "order_status",
                table: "Orders",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Orders",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "bill_to",
                table: "Orders",
                newName: "BillTo");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarrierCode",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CarrierDescription",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ServiceCode",
                table: "Shipments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.DropColumn(
                name: "CarrierCode",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CarrierDescription",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ServiceCode",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Shipments",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shipments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Shipments",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TransferMode",
                table: "Shipments",
                newName: "shipment_date");

            migrationBuilder.RenameColumn(
                name: "TotalPackageWeight",
                table: "Shipments",
                newName: "total_package_weight");

            migrationBuilder.RenameColumn(
                name: "TotalPackageCount",
                table: "Shipments",
                newName: "total_package_count");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Shipments",
                newName: "source_id");

            migrationBuilder.RenameColumn(
                name: "ShipmentType",
                table: "Shipments",
                newName: "request_date");

            migrationBuilder.RenameColumn(
                name: "ShipmentStatus",
                table: "Shipments",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "ShipmentDate",
                table: "Shipments",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Shipments",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Orders",
                newName: "reference");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Orders",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "Orders",
                newName: "warehouse_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Orders",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TotalTax",
                table: "Orders",
                newName: "total_tax");

            migrationBuilder.RenameColumn(
                name: "TotalSurcharge",
                table: "Orders",
                newName: "total_surcharge");

            migrationBuilder.RenameColumn(
                name: "TotalDiscount",
                table: "Orders",
                newName: "total_discount");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Orders",
                newName: "source_id");

            migrationBuilder.RenameColumn(
                name: "ShippingNotes",
                table: "Orders",
                newName: "shipping_notes");

            migrationBuilder.RenameColumn(
                name: "ShipmentId",
                table: "Orders",
                newName: "shipment_id");

            migrationBuilder.RenameColumn(
                name: "ShipTo",
                table: "Orders",
                newName: "ship_to");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "Orders",
                newName: "request_date");

            migrationBuilder.RenameColumn(
                name: "ReferenceExtra",
                table: "Orders",
                newName: "reference_extra");

            migrationBuilder.RenameColumn(
                name: "PickingNotes",
                table: "Orders",
                newName: "picking_notes");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "Orders",
                newName: "order_status");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Orders",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BillTo",
                table: "Orders",
                newName: "bill_to");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "Shipments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "carrier_code",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "carrier_description",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "payment_type",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_code",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shipment_status",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "shipment_type",
                table: "Shipments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "transfer_mode",
                table: "Shipments",
                type: "TEXT",
                nullable: true);
        }
    }
}
