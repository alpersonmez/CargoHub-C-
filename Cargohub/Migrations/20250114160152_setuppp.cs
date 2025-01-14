using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class setuppp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemGroups_ItemGroupId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Item_lines_ItemLineId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "itemShipment");

            migrationBuilder.DropTable(
                name: "itemTransfers");

            migrationBuilder.DropTable(
                name: "orderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemTypeId",
                table: "Items",
                newName: "ItemTypeid");

            migrationBuilder.RenameColumn(
                name: "ItemLineId",
                table: "Items",
                newName: "ItemLineid");

            migrationBuilder.RenameColumn(
                name: "ItemGroupId",
                table: "Items",
                newName: "ItemGroupid");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                newName: "IX_Items_ItemTypeid");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemLineId",
                table: "Items",
                newName: "IX_Items_ItemLineid");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemGroupId",
                table: "Items",
                newName: "IX_Items_ItemGroupid");

            migrationBuilder.AlterColumn<int>(
                name: "unit_purchase_quantity",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "unit_order_quantity",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "supplier_id",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "pack_order_quantity",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeid",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ItemLineid",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ItemGroupid",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Items",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "item_group",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "item_line",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "item_type",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "supplierid",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemGroups_ItemGroupid",
                table: "Items",
                column: "ItemGroupid",
                principalTable: "ItemGroups",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeid",
                table: "Items",
                column: "ItemTypeid",
                principalTable: "ItemTypes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Item_lines_ItemLineid",
                table: "Items",
                column: "ItemLineid",
                principalTable: "Item_lines",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Supplier_supplierid",
                table: "Items",
                column: "supplierid",
                principalTable: "Supplier",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemGroups_ItemGroupid",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeid",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Item_lines_ItemLineid",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Supplier_supplierid",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_supplierid",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "item_group",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "item_line",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "item_type",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "supplierid",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemTypeid",
                table: "Items",
                newName: "ItemTypeId");

            migrationBuilder.RenameColumn(
                name: "ItemLineid",
                table: "Items",
                newName: "ItemLineId");

            migrationBuilder.RenameColumn(
                name: "ItemGroupid",
                table: "Items",
                newName: "ItemGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemTypeid",
                table: "Items",
                newName: "IX_Items_ItemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemLineid",
                table: "Items",
                newName: "IX_Items_ItemLineId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemGroupid",
                table: "Items",
                newName: "IX_Items_ItemGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "unit_purchase_quantity",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "unit_order_quantity",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "uid",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "supplier_id",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "pack_order_quantity",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemLineId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemGroupId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "uid");

            migrationBuilder.CreateTable(
                name: "itemShipment",
                columns: table => new
                {
                    Shipmentid = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    amount = table.Column<int>(type: "INTEGER", nullable: true),
                    item_id = table.Column<string>(type: "TEXT", nullable: false)
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
                    amount = table.Column<int>(type: "INTEGER", nullable: true),
                    item_id = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "orderItem",
                columns: table => new
                {
                    Orderid = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    amount = table.Column<int>(type: "INTEGER", nullable: true),
                    item_id = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemGroups_ItemGroupId",
                table: "Items",
                column: "ItemGroupId",
                principalTable: "ItemGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemTypes_ItemTypeId",
                table: "Items",
                column: "ItemTypeId",
                principalTable: "ItemTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Item_lines_ItemLineId",
                table: "Items",
                column: "ItemLineId",
                principalTable: "Item_lines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
