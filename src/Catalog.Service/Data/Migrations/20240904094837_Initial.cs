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
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
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
                columns: new[] { "CategoryId", "CreatedDate", "DeletedAt", "Description", "ImageUrl", "IsDeleted", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5587), null, "", "", false, "Áo thun", new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5588) },
                    { 2, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5590), null, "", "", false, "Quần jean", new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5590) },
                    { 3, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5591), null, "", "", false, "Giày thể thao", new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5592) },
                    { 4, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5593), null, "", "", false, "Đồng hồ", new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5593) },
                    { 5, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5594), null, "", "", false, "Túi xách", new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5594) },
                    { 6, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5594), null, "", "", false, "Mũ", new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5595) },
                    { 7, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5596), null, "", "", false, "Kính râm", new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5596) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "CreatedDate", "DeletedAt", "Description", "ImageUrl", "IsDeleted", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5433), null, "Áo thun nam hàng hiệu màu đỏ", "", false, "Áo thun nam", 15m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5445) },
                    { 2, 1, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5451), null, "Áo thun nữ hàng hiệu màu hồng", "", false, "Áo thun nữ", 20m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5452) },
                    { 3, 2, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5453), null, "Quần jean nam hàng hiệu màu xanh", "", false, "Quần jean nam", 30m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5453) },
                    { 4, 2, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5455), null, "Quần jean nữ hàng hiệu màu xanh", "", false, "Quần jean nữ", 25m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5455) },
                    { 5, 3, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5456), null, "Giày thể thao nam hàng hiệu màu trắng", "", false, "Giày thể thao nam", 50m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5456) },
                    { 6, 3, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5457), null, "Giày thể thao nữ hàng hiệu màu trắng", "", false, "Giày thể thao nữ", 45m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5458) },
                    { 7, 4, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5459), null, "Đồng hồ nam hàng hiệu màu đen", "", false, "Đồng hồ nam", 100m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5459) },
                    { 8, 4, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5460), null, "Đồng hồ nữ hàng hiệu màu đen", "", false, "Đồng hồ nữ", 90m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5461) },
                    { 9, 5, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5462), null, "Túi xách nam hàng hiệu màu nâu", "", false, "Túi xách nam", 70m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5462) },
                    { 10, 5, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5463), null, "Túi xách nữ hàng hiệu màu nâu", "", false, "Túi xách nữ", 60m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5463) },
                    { 11, 6, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5464), null, "Mũ nam hàng hiệu màu xanh", "", false, "Mũ nam", 10m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5465) },
                    { 12, 6, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5466), null, "Mũ nữ hàng hiệu màu xanh", "", false, "Mũ nữ", 10m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5466) },
                    { 13, 7, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5467), null, "Kính râm nam hàng hiệu màu đen", "", false, "Kính râm nam", 20m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5467) },
                    { 14, 7, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5469), null, "Kính râm nữ hàng hiệu màu đen", "", false, "Kính râm nữ", 20m, 100, new DateTime(2024, 9, 4, 16, 48, 37, 362, DateTimeKind.Local).AddTicks(5469) }
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
