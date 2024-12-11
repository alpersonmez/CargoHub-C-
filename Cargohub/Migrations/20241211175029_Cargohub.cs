using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class Cargohub : Migration
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
