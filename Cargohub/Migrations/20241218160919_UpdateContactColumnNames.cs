using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "contactPhone",
                table: "Warehouses",
                newName: "contact_phone");

            migrationBuilder.RenameColumn(
                name: "contactName",
                table: "Warehouses",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "contactEmail",
                table: "Warehouses",
                newName: "contact_email");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Supplier",
                newName: "reference");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Supplier",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Supplier",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Supplier",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Supplier",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Supplier",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Supplier",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Supplier",
                newName: "zip_code");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Supplier",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Supplier",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Supplier",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "Supplier",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "AddressExtra",
                table: "Supplier",
                newName: "address_extra");

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
                newName: "transfer_mode");

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
                newName: "shipment_type");

            migrationBuilder.RenameColumn(
                name: "ShipmentStatus",
                table: "Shipments",
                newName: "shipment_status");

            migrationBuilder.RenameColumn(
                name: "ShipmentDate",
                table: "Shipments",
                newName: "shipment_date");

            migrationBuilder.RenameColumn(
                name: "ServiceCode",
                table: "Shipments",
                newName: "service_code");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "Shipments",
                newName: "request_date");

            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "Shipments",
                newName: "payment_type");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Shipments",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Shipments",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Shipments",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CarrierDescription",
                table: "Shipments",
                newName: "carrier_description");

            migrationBuilder.RenameColumn(
                name: "CarrierCode",
                table: "Shipments",
                newName: "carrier_code");

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

            migrationBuilder.RenameColumn(
                name: "WareHouse_Id",
                table: "Locations",
                newName: "warehouse_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Locations",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Locations",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Locations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Locations",
                newName: "update_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Locations",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ItemTypes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ItemTypes",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ItemTypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ItemTypes",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ItemTypes",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Items",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Items",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "Items",
                newName: "uid");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Items",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "UpcCode",
                table: "Items",
                newName: "upc_code");

            migrationBuilder.RenameColumn(
                name: "UnitPurchaseQuantity",
                table: "Items",
                newName: "unit_purchase_quantity");

            migrationBuilder.RenameColumn(
                name: "UnitOrderQuantity",
                table: "Items",
                newName: "unit_order_quantity");

            migrationBuilder.RenameColumn(
                name: "SupplierPartNumber",
                table: "Items",
                newName: "supplier_part_number");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "Items",
                newName: "supplier_id");

            migrationBuilder.RenameColumn(
                name: "SupplierCode",
                table: "Items",
                newName: "supplier_code");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Items",
                newName: "short_description");

            migrationBuilder.RenameColumn(
                name: "PackOrderQuantity",
                table: "Items",
                newName: "pack_order_quantity");

            migrationBuilder.RenameColumn(
                name: "ModelNumber",
                table: "Items",
                newName: "model_number");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "Items",
                newName: "item_type");

            migrationBuilder.RenameColumn(
                name: "ItemLine",
                table: "Items",
                newName: "item_line");

            migrationBuilder.RenameColumn(
                name: "ItemGroup",
                table: "Items",
                newName: "item_group");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Items",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CommodityCode",
                table: "Items",
                newName: "commodity_code");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ItemGroups",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ItemGroups",
                newName: "created_at");

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

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Item_lines",
                newName: "update_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Item_lines",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Clients",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Clients",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Clients",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Clients",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Clients",
                newName: "zip_code");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Clients",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Clients",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Clients",
                newName: "contact_phone");

            migrationBuilder.RenameColumn(
                name: "ContactName",
                table: "Clients",
                newName: "contact_name");

            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "Clients",
                newName: "contact_email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "contact_phone",
                table: "Warehouses",
                newName: "contactPhone");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Warehouses",
                newName: "contactName");

            migrationBuilder.RenameColumn(
                name: "contact_email",
                table: "Warehouses",
                newName: "contactEmail");

            migrationBuilder.RenameColumn(
                name: "reference",
                table: "Supplier",
                newName: "Reference");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Supplier",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Supplier",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Supplier",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Supplier",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Supplier",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Supplier",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "zip_code",
                table: "Supplier",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Supplier",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Supplier",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Supplier",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Supplier",
                newName: "ContactName");

            migrationBuilder.RenameColumn(
                name: "address_extra",
                table: "Supplier",
                newName: "AddressExtra");

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
                name: "transfer_mode",
                table: "Shipments",
                newName: "TransferMode");

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
                name: "shipment_type",
                table: "Shipments",
                newName: "ShipmentType");

            migrationBuilder.RenameColumn(
                name: "shipment_status",
                table: "Shipments",
                newName: "ShipmentStatus");

            migrationBuilder.RenameColumn(
                name: "shipment_date",
                table: "Shipments",
                newName: "ShipmentDate");

            migrationBuilder.RenameColumn(
                name: "service_code",
                table: "Shipments",
                newName: "ServiceCode");

            migrationBuilder.RenameColumn(
                name: "request_date",
                table: "Shipments",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "payment_type",
                table: "Shipments",
                newName: "PaymentType");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Shipments",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Shipments",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Shipments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "carrier_description",
                table: "Shipments",
                newName: "CarrierDescription");

            migrationBuilder.RenameColumn(
                name: "carrier_code",
                table: "Shipments",
                newName: "CarrierCode");

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

            migrationBuilder.RenameColumn(
                name: "warehouse_id",
                table: "Locations",
                newName: "WareHouse_Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Locations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Locations",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Locations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "update_at",
                table: "Locations",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Locations",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ItemTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ItemTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ItemTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "ItemTypes",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ItemTypes",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Items",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Items",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "uid",
                table: "Items",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Items",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "upc_code",
                table: "Items",
                newName: "UpcCode");

            migrationBuilder.RenameColumn(
                name: "unit_purchase_quantity",
                table: "Items",
                newName: "UnitPurchaseQuantity");

            migrationBuilder.RenameColumn(
                name: "unit_order_quantity",
                table: "Items",
                newName: "UnitOrderQuantity");

            migrationBuilder.RenameColumn(
                name: "supplier_part_number",
                table: "Items",
                newName: "SupplierPartNumber");

            migrationBuilder.RenameColumn(
                name: "supplier_id",
                table: "Items",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "supplier_code",
                table: "Items",
                newName: "SupplierCode");

            migrationBuilder.RenameColumn(
                name: "short_description",
                table: "Items",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "pack_order_quantity",
                table: "Items",
                newName: "PackOrderQuantity");

            migrationBuilder.RenameColumn(
                name: "model_number",
                table: "Items",
                newName: "ModelNumber");

            migrationBuilder.RenameColumn(
                name: "item_type",
                table: "Items",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "item_line",
                table: "Items",
                newName: "ItemLine");

            migrationBuilder.RenameColumn(
                name: "item_group",
                table: "Items",
                newName: "ItemGroup");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Items",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "commodity_code",
                table: "Items",
                newName: "CommodityCode");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "ItemGroups",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ItemGroups",
                newName: "CreatedAt");

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

            migrationBuilder.RenameColumn(
                name: "update_at",
                table: "Item_lines",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Item_lines",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "Clients",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Clients",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Clients",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Clients",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Clients",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "zip_code",
                table: "Clients",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Clients",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Clients",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "contact_phone",
                table: "Clients",
                newName: "ContactPhone");

            migrationBuilder.RenameColumn(
                name: "contact_name",
                table: "Clients",
                newName: "ContactName");

            migrationBuilder.RenameColumn(
                name: "contact_email",
                table: "Clients",
                newName: "ContactEmail");
        }
    }
}
