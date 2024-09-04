﻿// <auto-generated />
using System;
using Catalog.Service.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Service.Data.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20240904094837_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Catalog.Service.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5587),
                            Description = "",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Áo thun",
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5588)
                        },
                        new
                        {
                            CategoryId = 2,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5590),
                            Description = "",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Quần jean",
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5590)
                        },
                        new
                        {
                            CategoryId = 3,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5591),
                            Description = "",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Giày thể thao",
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5592)
                        },
                        new
                        {
                            CategoryId = 4,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5593),
                            Description = "",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Đồng hồ",
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5593)
                        },
                        new
                        {
                            CategoryId = 5,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5594),
                            Description = "",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Túi xách",
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5594)
                        },
                        new
                        {
                            CategoryId = 6,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5594),
                            Description = "",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Mũ",
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5595)
                        },
                        new
                        {
                            CategoryId = 7,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5596),
                            Description = "",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Kính râm",
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5596)
                        });
                });

            modelBuilder.Entity("Catalog.Service.Data.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5433),
                            Description = "Áo thun nam hàng hiệu màu đỏ",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Áo thun nam",
                            Price = 15m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5445)
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5451),
                            Description = "Áo thun nữ hàng hiệu màu hồng",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Áo thun nữ",
                            Price = 20m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5452)
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5453),
                            Description = "Quần jean nam hàng hiệu màu xanh",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Quần jean nam",
                            Price = 30m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5453)
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5455),
                            Description = "Quần jean nữ hàng hiệu màu xanh",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Quần jean nữ",
                            Price = 25m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5455)
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 3,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5456),
                            Description = "Giày thể thao nam hàng hiệu màu trắng",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Giày thể thao nam",
                            Price = 50m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5456)
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 3,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5457),
                            Description = "Giày thể thao nữ hàng hiệu màu trắng",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Giày thể thao nữ",
                            Price = 45m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5458)
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 4,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5459),
                            Description = "Đồng hồ nam hàng hiệu màu đen",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Đồng hồ nam",
                            Price = 100m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5459)
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 4,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5460),
                            Description = "Đồng hồ nữ hàng hiệu màu đen",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Đồng hồ nữ",
                            Price = 90m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5461)
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 5,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5462),
                            Description = "Túi xách nam hàng hiệu màu nâu",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Túi xách nam",
                            Price = 70m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5462)
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 5,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5463),
                            Description = "Túi xách nữ hàng hiệu màu nâu",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Túi xách nữ",
                            Price = 60m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5463)
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryId = 6,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5464),
                            Description = "Mũ nam hàng hiệu màu xanh",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Mũ nam",
                            Price = 10m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5465)
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 6,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5466),
                            Description = "Mũ nữ hàng hiệu màu xanh",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Mũ nữ",
                            Price = 10m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5466)
                        },
                        new
                        {
                            ProductId = 13,
                            CategoryId = 7,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5467),
                            Description = "Kính râm nam hàng hiệu màu đen",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Kính râm nam",
                            Price = 20m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5467)
                        },
                        new
                        {
                            ProductId = 14,
                            CategoryId = 7,
                            CreatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5469),
                            Description = "Kính râm nữ hàng hiệu màu đen",
                            ImageUrl = "",
                            IsDeleted = false,
                            Name = "Kính râm nữ",
                            Price = 20m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5469)
                        });
                });

            modelBuilder.Entity("Catalog.Service.Data.Models.Product", b =>
                {
                    b.HasOne("Catalog.Service.Data.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Catalog.Service.Data.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}