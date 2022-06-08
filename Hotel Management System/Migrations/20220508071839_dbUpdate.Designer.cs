﻿// <auto-generated />
using System;
using Hotel_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hotel_Management_System.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220508071839_dbUpdate")]
    partial class dbUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hotel_Management_System.Models.CustomerDetails", b =>
                {
                    b.Property<int>("customer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("customer_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("customer_nid")
                        .HasColumnType("bigint");

                    b.Property<int>("customer_phone")
                        .HasColumnType("int");

                    b.Property<string>("image_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image_path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customer_id");

                    b.ToTable("customer_info");
                });

            modelBuilder.Entity("Hotel_Management_System.Models.ExpenseModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("entry_date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("expense_cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("expense_details")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("expense_data");
                });

            modelBuilder.Entity("Hotel_Management_System.Models.NewRoom", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("room_booked_by")
                        .HasColumnType("int");

                    b.Property<string>("room_booked_date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("room_booked_hour")
                        .HasColumnType("int");

                    b.Property<int>("room_booked_minute")
                        .HasColumnType("int");

                    b.Property<bool>("room_choose")
                        .HasColumnType("bit");

                    b.Property<int>("room_floor")
                        .HasColumnType("int");

                    b.Property<int>("room_no")
                        .HasColumnType("int");

                    b.Property<double>("room_price")
                        .HasColumnType("float");

                    b.Property<bool>("room_status")
                        .HasColumnType("bit");

                    b.Property<string>("room_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("new_room");
                });

            modelBuilder.Entity("Hotel_Management_System.Models.ReportModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("advance_amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("check_in_time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("check_out_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("customer_from")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("customer_phone")
                        .HasColumnType("bigint");

                    b.Property<decimal>("discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("grand_total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("invoice_no")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("room_no")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("room_price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("room_total")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("stayed")
                        .HasColumnType("int");

                    b.Property<decimal>("sub_total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.ToTable("report_data");
                });

            modelBuilder.Entity("Hotel_Management_System.Models.RoomType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("room_price")
                        .HasColumnType("float");

                    b.Property<string>("room_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("room_type");
                });

            modelBuilder.Entity("Hotel_Management_System.Models.UserDetails", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("user_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_phone")
                        .HasColumnType("int");

                    b.Property<int>("user_type")
                        .HasColumnType("int");

                    b.HasKey("user_id");

                    b.ToTable("user_info");
                });
#pragma warning restore 612, 618
        }
    }
}
