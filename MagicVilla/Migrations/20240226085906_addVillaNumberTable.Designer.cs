﻿// <auto-generated />
using System;
using MagicVilla.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240226085906_addVillaNumberTable")]
    partial class addVillaNumberTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "Villa 1 Amenity",
                            CreatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(918),
                            Details = "Villa 1 Details",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Villa 1",
                            Occupancy = 4,
                            Rate = 100.0,
                            Sqft = 1000,
                            UpdatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(932)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "Villa 2 Amenity",
                            CreatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(934),
                            Details = "Villa 2 Details",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Villa 2",
                            Occupancy = 6,
                            Rate = 200.0,
                            Sqft = 2000,
                            UpdatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(935)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "Villa 3 Amenity",
                            CreatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(936),
                            Details = "Villa 3 Details",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Villa 3",
                            Occupancy = 8,
                            Rate = 300.0,
                            Sqft = 3000,
                            UpdatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(937)
                        },
                        new
                        {
                            Id = 4,
                            Amenity = "Villa 4 Amenity",
                            CreatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(939),
                            Details = "Villa 4 Details",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Villa 4",
                            Occupancy = 10,
                            Rate = 400.0,
                            Sqft = 4000,
                            UpdatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(939)
                        },
                        new
                        {
                            Id = 5,
                            Amenity = "Villa 5 Amenity",
                            CreatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(941),
                            Details = "Villa 5 Details",
                            ImageUrl = "https://via.placeholder.com/150",
                            Name = "Villa 5",
                            Occupancy = 12,
                            Rate = 500.0,
                            Sqft = 5000,
                            UpdatedAt = new DateTime(2024, 2, 26, 15, 59, 5, 120, DateTimeKind.Local).AddTicks(941)
                        });
                });

            modelBuilder.Entity("MagicVilla.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("VillaNo");

                    b.ToTable("villasNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
