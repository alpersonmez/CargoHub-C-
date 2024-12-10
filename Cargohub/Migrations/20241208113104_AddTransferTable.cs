using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cargohub.Migrations
{
    /// <inheritdoc />
    public partial class AddTransferTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Transferid",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    reference = table.Column<string>(type: "TEXT", nullable: true),
                    transfer_from = table.Column<int>(type: "INTEGER", nullable: true),
                    transfer_to = table.Column<int>(type: "INTEGER", nullable: false),
                    transfer_status = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Transfers_Transferid",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Items_Transferid",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Transferid",
                table: "Items");
        }
    }
}
