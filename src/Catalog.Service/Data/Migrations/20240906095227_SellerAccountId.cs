using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Service.Data.Migrations
{
    /// <inheritdoc />
    public partial class SellerAccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerAccountId",
                schema: "catalog",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerAccountId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                schema: "catalog",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
