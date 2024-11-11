using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    source_id = table.Column<int>(type: "INTEGER", nullable: false),
                    order_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    request_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    reference = table.Column<string>(type: "TEXT", nullable: false),
                    reference_extra = table.Column<string>(type: "TEXT", nullable: false),
                    order_status = table.Column<string>(type: "TEXT", nullable: false),
                    notes = table.Column<string>(type: "TEXT", nullable: false),
                    shipping_notes = table.Column<string>(type: "TEXT", nullable: false),
                    picking_notes = table.Column<string>(type: "TEXT", nullable: false),
                    warehouse_id = table.Column<int>(type: "INTEGER", nullable: false),
                    ship_to = table.Column<string>(type: "TEXT", nullable: false),
                    bill_to = table.Column<string>(type: "TEXT", nullable: false),
                    shipment_id = table.Column<int>(type: "INTEGER", nullable: false),
                    total_amount = table.Column<double>(type: "REAL", nullable: false),
                    total_discount = table.Column<double>(type: "REAL", nullable: false),
                    total_tax = table.Column<double>(type: "REAL", nullable: false),
                    total_surcharge = table.Column<double>(type: "REAL", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
