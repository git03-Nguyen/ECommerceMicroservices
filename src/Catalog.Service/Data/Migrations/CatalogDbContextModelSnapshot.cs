﻿// <auto-generated />
using System;
using Catalog.Service.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Service.Data.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

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
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2972),
                            Description = "",
                            ImageUrl = "",
                            Name = "Áo thun",
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2973)
                        },
                        new
                        {
                            CategoryId = 2,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2977),
                            Description = "",
                            ImageUrl = "",
                            Name = "Quần jean",
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2977)
                        },
                        new
                        {
                            CategoryId = 3,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2978),
                            Description = "",
                            ImageUrl = "",
                            Name = "Giày thể thao",
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2978)
                        },
                        new
                        {
                            CategoryId = 4,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2979),
                            Description = "",
                            ImageUrl = "",
                            Name = "Đồng hồ",
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2980)
                        },
                        new
                        {
                            CategoryId = 5,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2981),
                            Description = "",
                            ImageUrl = "",
                            Name = "Túi xách",
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2981)
                        },
                        new
                        {
                            CategoryId = 6,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2982),
                            Description = "",
                            ImageUrl = "",
                            Name = "Mũ",
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2983)
                        },
                        new
                        {
                            CategoryId = 7,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2984),
                            Description = "",
                            ImageUrl = "",
                            Name = "Kính râm",
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2984)
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

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
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2779),
                            Description = "Áo thun nam hàng hiệu màu đỏ",
                            ImageUrl = "",
                            Name = "Áo thun nam",
                            Price = 15m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2788)
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2793),
                            Description = "Áo thun nữ hàng hiệu màu hồng",
                            ImageUrl = "",
                            Name = "Áo thun nữ",
                            Price = 20m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2794)
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2796),
                            Description = "Quần jean nam hàng hiệu màu xanh",
                            ImageUrl = "",
                            Name = "Quần jean nam",
                            Price = 30m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2796)
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2798),
                            Description = "Quần jean nữ hàng hiệu màu xanh",
                            ImageUrl = "",
                            Name = "Quần jean nữ",
                            Price = 25m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2798)
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 3,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2799),
                            Description = "Giày thể thao nam hàng hiệu màu trắng",
                            ImageUrl = "",
                            Name = "Giày thể thao nam",
                            Price = 50m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2800)
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 3,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2801),
                            Description = "Giày thể thao nữ hàng hiệu màu trắng",
                            ImageUrl = "",
                            Name = "Giày thể thao nữ",
                            Price = 45m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2802)
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 4,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2803),
                            Description = "Đồng hồ nam hàng hiệu màu đen",
                            ImageUrl = "",
                            Name = "Đồng hồ nam",
                            Price = 100m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2803)
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 4,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2805),
                            Description = "Đồng hồ nữ hàng hiệu màu đen",
                            ImageUrl = "",
                            Name = "Đồng hồ nữ",
                            Price = 90m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2805)
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 5,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2807),
                            Description = "Túi xách nam hàng hiệu màu nâu",
                            ImageUrl = "",
                            Name = "Túi xách nam",
                            Price = 70m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2807)
                        },
                        new
                        {
                            ProductId = 10,
                            CategoryId = 5,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2808),
                            Description = "Túi xách nữ hàng hiệu màu nâu",
                            ImageUrl = "",
                            Name = "Túi xách nữ",
                            Price = 60m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2809)
                        },
                        new
                        {
                            ProductId = 11,
                            CategoryId = 6,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2812),
                            Description = "Mũ nam hàng hiệu màu xanh",
                            ImageUrl = "",
                            Name = "Mũ nam",
                            Price = 10m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2813)
                        },
                        new
                        {
                            ProductId = 12,
                            CategoryId = 6,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2814),
                            Description = "Mũ nữ hàng hiệu màu xanh",
                            ImageUrl = "",
                            Name = "Mũ nữ",
                            Price = 10m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2814)
                        },
                        new
                        {
                            ProductId = 13,
                            CategoryId = 7,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2816),
                            Description = "Kính râm nam hàng hiệu màu đen",
                            ImageUrl = "",
                            Name = "Kính râm nam",
                            Price = 20m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2816)
                        },
                        new
                        {
                            ProductId = 14,
                            CategoryId = 7,
                            CreatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2818),
                            Description = "Kính râm nữ hàng hiệu màu đen",
                            ImageUrl = "",
                            Name = "Kính râm nữ",
                            Price = 20m,
                            Stock = 100,
                            UpdatedDate = new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2818)
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
