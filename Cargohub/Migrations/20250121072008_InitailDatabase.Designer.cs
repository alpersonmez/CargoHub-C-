﻿// <auto-generated />
using System;
using Cargohub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cargohub.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250121072008_InitailDatabase")]
    partial class InitailDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Cargohub.Models.Client", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("address")
                        .HasColumnType("TEXT");

                    b.Property<string>("city")
                        .HasColumnType("TEXT");

                    b.Property<string>("contact_email")
                        .HasColumnType("TEXT");

                    b.Property<string>("contact_name")
                        .HasColumnType("TEXT");

                    b.Property<string>("contact_phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("province")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("zip_code")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Cargohub.Models.Dock", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("code")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("is_deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.Property<int>("warehouse_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("code")
                        .IsUnique();

                    b.HasIndex("warehouse_id");

                    b.ToTable("Docks");
                });

            modelBuilder.Entity("Cargohub.Models.Inventory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("item_id")
                        .HasColumnType("TEXT");

                    b.Property<string>("item_reference")
                        .HasColumnType("TEXT");

                    b.Property<string>("locations")
                        .HasColumnType("TEXT");

                    b.Property<int?>("total_allocated")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("total_available")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("total_expected")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("total_on_hand")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("total_ordered")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("Cargohub.Models.Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ItemGroupid")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ItemLineid")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ItemTypeid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("code")
                        .HasColumnType("TEXT");

                    b.Property<string>("commodity_code")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("item_group")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("item_line")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("item_type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("model_number")
                        .HasColumnType("TEXT");

                    b.Property<int>("pack_order_quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("short_description")
                        .HasColumnType("TEXT");

                    b.Property<string>("supplier_code")
                        .HasColumnType("TEXT");

                    b.Property<int>("supplier_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("supplier_part_number")
                        .HasColumnType("TEXT");

                    b.Property<int?>("supplierid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("uid")
                        .HasColumnType("TEXT");

                    b.Property<int>("unit_order_quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("unit_purchase_quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("upc_code")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("ItemGroupid");

                    b.HasIndex("ItemLineid");

                    b.HasIndex("ItemTypeid");

                    b.HasIndex("supplierid");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Cargohub.Models.ItemGroup", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("ItemGroups");
                });

            modelBuilder.Entity("Cargohub.Models.ItemLines", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Item_lines");
                });

            modelBuilder.Entity("Cargohub.Models.ItemType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("ItemTypes");
                });

            modelBuilder.Entity("Cargohub.Models.Location", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("code")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.Property<int?>("warehouse_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Cargohub.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("bill_to")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("notes")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("order_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("order_status")
                        .HasColumnType("TEXT");

                    b.Property<string>("picking_notes")
                        .HasColumnType("TEXT");

                    b.Property<string>("reference")
                        .HasColumnType("TEXT");

                    b.Property<string>("reference_extra")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("request_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("ship_to")
                        .HasColumnType("TEXT");

                    b.Property<string>("shipping_notes")
                        .HasColumnType("TEXT");

                    b.Property<int?>("source_id")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("total_amount")
                        .HasColumnType("REAL");

                    b.Property<double?>("total_discount")
                        .HasColumnType("REAL");

                    b.Property<double?>("total_surcharge")
                        .HasColumnType("REAL");

                    b.Property<double?>("total_tax")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.Property<int?>("warehouse_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Cargohub.Models.OrderShipment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("order_id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("shipment_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("order_id");

                    b.HasIndex("shipment_id");

                    b.ToTable("OrderShipments");
                });

            modelBuilder.Entity("Cargohub.Models.Shipment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("carrier_code")
                        .HasColumnType("TEXT");

                    b.Property<string>("carrier_description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("notes")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("order_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("payment_type")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("request_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("service_code")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("shipment_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("shipment_status")
                        .HasColumnType("TEXT");

                    b.Property<string>("shipment_type")
                        .HasColumnType("TEXT");

                    b.Property<int?>("source_id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("total_package_count")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("total_package_weight")
                        .HasColumnType("REAL");

                    b.Property<string>("transfer_mode")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("Cargohub.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StockType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<int>("amount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Stocks", (string)null);

                    b.HasDiscriminator<string>("StockType").HasValue("Stock");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Cargohub.Models.Supplier", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("address")
                        .HasColumnType("TEXT");

                    b.Property<string>("address_extra")
                        .HasColumnType("TEXT");

                    b.Property<string>("city")
                        .HasColumnType("TEXT");

                    b.Property<string>("code")
                        .HasColumnType("TEXT");

                    b.Property<string>("contact_name")
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("phone_number")
                        .HasColumnType("TEXT");

                    b.Property<string>("province")
                        .HasColumnType("TEXT");

                    b.Property<string>("reference")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("zip_code")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Cargohub.Models.Transfer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("reference")
                        .HasColumnType("TEXT");

                    b.Property<int?>("transfer_from")
                        .HasColumnType("INTEGER");

                    b.Property<string>("transfer_status")
                        .HasColumnType("TEXT");

                    b.Property<int?>("transfer_to")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("Cargohub.Models.Warehouse", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("address")
                        .HasColumnType("TEXT");

                    b.Property<string>("city")
                        .HasColumnType("TEXT");

                    b.Property<string>("code")
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<int?>("gevarenclassificatie")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("province")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("zip")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("ImportStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DataImported")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ImportStatuses");
                });

            modelBuilder.Entity("Cargohub.Models.OrderStock", b =>
                {
                    b.HasBaseType("Cargohub.Models.Stock");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("OrderId");

                    b.HasDiscriminator().HasValue("Order");
                });

            modelBuilder.Entity("Cargohub.Models.ShipmentStock", b =>
                {
                    b.HasBaseType("Cargohub.Models.Stock");

                    b.Property<int>("ShipmentId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ShipmentId");

                    b.HasDiscriminator().HasValue("Shipment");
                });

            modelBuilder.Entity("Cargohub.Models.TransferStock", b =>
                {
                    b.HasBaseType("Cargohub.Models.Stock");

                    b.Property<int>("TransferId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("TransferId");

                    b.HasDiscriminator().HasValue("Transfer");
                });

            modelBuilder.Entity("Cargohub.Models.Dock", b =>
                {
                    b.HasOne("Cargohub.Models.Warehouse", "warehouse")
                        .WithMany("docks")
                        .HasForeignKey("warehouse_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("warehouse");
                });

            modelBuilder.Entity("Cargohub.Models.Item", b =>
                {
                    b.HasOne("Cargohub.Models.ItemGroup", "ItemGroup")
                        .WithMany()
                        .HasForeignKey("ItemGroupid");

                    b.HasOne("Cargohub.Models.ItemLines", "ItemLine")
                        .WithMany()
                        .HasForeignKey("ItemLineid");

                    b.HasOne("Cargohub.Models.ItemType", "ItemType")
                        .WithMany()
                        .HasForeignKey("ItemTypeid");

                    b.HasOne("Cargohub.Models.Supplier", "supplier")
                        .WithMany()
                        .HasForeignKey("supplierid");

                    b.Navigation("ItemGroup");

                    b.Navigation("ItemLine");

                    b.Navigation("ItemType");

                    b.Navigation("supplier");
                });

            modelBuilder.Entity("Cargohub.Models.OrderShipment", b =>
                {
                    b.HasOne("Cargohub.Models.Order", "Order")
                        .WithMany("OrderShipments")
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cargohub.Models.Shipment", "Shipment")
                        .WithMany("OrderShipments")
                        .HasForeignKey("shipment_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("Cargohub.Models.Warehouse", b =>
                {
                    b.OwnsOne("Cargohub.Models.Warehouse+Contact", "contact", b1 =>
                        {
                            b1.Property<int>("Warehouseid")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("email")
                                .HasColumnType("TEXT")
                                .HasColumnName("contact_email");

                            b1.Property<string>("name")
                                .HasColumnType("TEXT")
                                .HasColumnName("contact_name");

                            b1.Property<string>("phone")
                                .HasColumnType("TEXT")
                                .HasColumnName("contact_phone");

                            b1.HasKey("Warehouseid");

                            b1.ToTable("Warehouses");

                            b1.WithOwner()
                                .HasForeignKey("Warehouseid");
                        });

                    b.Navigation("contact")
                        .IsRequired();
                });

            modelBuilder.Entity("Cargohub.Models.OrderStock", b =>
                {
                    b.HasOne("Cargohub.Models.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Cargohub.Models.ShipmentStock", b =>
                {
                    b.HasOne("Cargohub.Models.Shipment", "Shipment")
                        .WithMany()
                        .HasForeignKey("ShipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("Cargohub.Models.TransferStock", b =>
                {
                    b.HasOne("Cargohub.Models.Transfer", "Transfer")
                        .WithMany()
                        .HasForeignKey("TransferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transfer");
                });

            modelBuilder.Entity("Cargohub.Models.Order", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("OrderShipments");
                });

            modelBuilder.Entity("Cargohub.Models.Shipment", b =>
                {
                    b.Navigation("OrderShipments");
                });

            modelBuilder.Entity("Cargohub.Models.Warehouse", b =>
                {
                    b.Navigation("docks");
                });
#pragma warning restore 612, 618
        }
    }
}
