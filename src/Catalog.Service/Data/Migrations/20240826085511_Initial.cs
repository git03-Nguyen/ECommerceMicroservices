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
                    AvailableStock = table.Column<int>(type: "integer", nullable: false),
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
                    { 1, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7557), "", "", "Áo thun", new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7558) },
                    { 2, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7561), "", "", "Quần jean", new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7562) },
                    { 3, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7563), "", "", "Giày thể thao", new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7563) },
                    { 4, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7564), "", "", "Đồng hồ", new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7564) },
                    { 5, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7565), "", "", "Túi xách", new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7565) },
                    { 6, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7566), "", "", "Mũ", new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7567) },
                    { 7, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7567), "", "", "Kính râm", new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7568) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AvailableStock", "CategoryId", "CreatedDate", "Description", "ImageUrl", "Name", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 0, 1, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7401), "Áo thun nam hàng hiệu màu đỏ", "", "Áo thun nam", 15m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7411) },
                    { 2, 0, 1, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7416), "Áo thun nữ hàng hiệu màu hồng", "", "Áo thun nữ", 20m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7416) },
                    { 3, 0, 2, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7418), "Quần jean nam hàng hiệu màu xanh", "", "Quần jean nam", 30m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7418) },
                    { 4, 0, 2, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7419), "Quần jean nữ hàng hiệu màu xanh", "", "Quần jean nữ", 25m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7420) },
                    { 5, 0, 3, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7421), "Giày thể thao nam hàng hiệu màu trắng", "", "Giày thể thao nam", 50m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7421) },
                    { 6, 0, 3, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7422), "Giày thể thao nữ hàng hiệu màu trắng", "", "Giày thể thao nữ", 45m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7423) },
                    { 7, 0, 4, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7424), "Đồng hồ nam hàng hiệu màu đen", "", "Đồng hồ nam", 100m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7424) },
                    { 8, 0, 4, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7425), "Đồng hồ nữ hàng hiệu màu đen", "", "Đồng hồ nữ", 90m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7426) },
                    { 9, 0, 5, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7427), "Túi xách nam hàng hiệu màu nâu", "", "Túi xách nam", 70m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7427) },
                    { 10, 0, 5, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7428), "Túi xách nữ hàng hiệu màu nâu", "", "Túi xách nữ", 60m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7429) },
                    { 11, 0, 6, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7430), "Mũ nam hàng hiệu màu xanh", "", "Mũ nam", 10m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7430) },
                    { 12, 0, 6, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7431), "Mũ nữ hàng hiệu màu xanh", "", "Mũ nữ", 10m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7431) },
                    { 13, 0, 7, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7433), "Kính râm nam hàng hiệu màu đen", "", "Kính râm nam", 20m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7433) },
                    { 14, 0, 7, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7434), "Kính râm nữ hàng hiệu màu đen", "", "Kính râm nữ", 20m, new DateTime(2024, 8, 26, 15, 55, 10, 723, DateTimeKind.Local).AddTicks(7434) }
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
