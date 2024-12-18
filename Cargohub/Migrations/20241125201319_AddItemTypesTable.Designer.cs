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
    [Migration("20241125201319_AddItemTypesTable")]
    partial class AddItemTypesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Cargohub.Models.Item", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CommodityCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemGroup")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemLine")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModelNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PackOrderQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SupplierCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SupplierPartNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UnitOrderQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UnitPurchaseQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpcCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Uid");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Cargohub.Models.ItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemTypes");
                });

            modelBuilder.Entity("Cargohub.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("bill_to")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("order_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("order_status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("picking_notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("reference")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("reference_extra")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("request_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("ship_to")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("shipment_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("shipping_notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("source_id")
                        .HasColumnType("INTEGER");

                    b.Property<double>("total_amount")
                        .HasColumnType("REAL");

                    b.Property<double>("total_discount")
                        .HasColumnType("REAL");

                    b.Property<double>("total_surcharge")
                        .HasColumnType("REAL");

                    b.Property<double>("total_tax")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.Property<int>("warehouse_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Cargohub.Models.Warehouse", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("contactEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("contactName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("contactPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("province")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("TEXT");

                    b.Property<string>("zip")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("WareHouse_Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
