using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Service.Data.Migrations
{
    /// <inheritdoc />
    public partial class Schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "public",
                newName: "Products",
                newSchema: "catalog");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "public",
                newName: "Categories",
                newSchema: "catalog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "catalog",
                newName: "Products",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Categories",
                schema: "catalog",
                newName: "Categories",
                newSchema: "public");
        }
    }
}
