using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Service.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedDate", "Description", "ImageUrl", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2972), "", "", "Áo thun", new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2973) },
                    { 2, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2977), "", "", "Quần jean", new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2977) },
                    { 3, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2978), "", "", "Giày thể thao", new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2978) },
                    { 4, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2979), "", "", "Đồng hồ", new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2980) },
                    { 5, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2981), "", "", "Túi xách", new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2981) },
                    { 6, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2982), "", "", "Mũ", new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2983) },
                    { 7, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2984), "", "", "Kính râm", new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2984) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedDate", "Description", "ImageUrl", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2779), "Áo thun nam hàng hiệu màu đỏ", "", "Áo thun nam", 15m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2788) },
                    { 2, 1, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2793), "Áo thun nữ hàng hiệu màu hồng", "", "Áo thun nữ", 20m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2794) },
                    { 3, 2, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2796), "Quần jean nam hàng hiệu màu xanh", "", "Quần jean nam", 30m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2796) },
                    { 4, 2, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2798), "Quần jean nữ hàng hiệu màu xanh", "", "Quần jean nữ", 25m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2798) },
                    { 5, 3, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2799), "Giày thể thao nam hàng hiệu màu trắng", "", "Giày thể thao nam", 50m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2800) },
                    { 6, 3, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2801), "Giày thể thao nữ hàng hiệu màu trắng", "", "Giày thể thao nữ", 45m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2802) },
                    { 7, 4, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2803), "Đồng hồ nam hàng hiệu màu đen", "", "Đồng hồ nam", 100m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2803) },
                    { 8, 4, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2805), "Đồng hồ nữ hàng hiệu màu đen", "", "Đồng hồ nữ", 90m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2805) },
                    { 9, 5, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2807), "Túi xách nam hàng hiệu màu nâu", "", "Túi xách nam", 70m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2807) },
                    { 10, 5, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2808), "Túi xách nữ hàng hiệu màu nâu", "", "Túi xách nữ", 60m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2809) },
                    { 11, 6, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2812), "Mũ nam hàng hiệu màu xanh", "", "Mũ nam", 10m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2813) },
                    { 12, 6, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2814), "Mũ nữ hàng hiệu màu xanh", "", "Mũ nữ", 10m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2814) },
                    { 13, 7, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2816), "Kính râm nam hàng hiệu màu đen", "", "Kính râm nam", 20m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2816) },
                    { 14, 7, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2818), "Kính râm nữ hàng hiệu màu đen", "", "Kính râm nữ", 20m, 100, new DateTime(2024, 8, 30, 10, 12, 31, 619, DateTimeKind.Local).AddTicks(2818) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
