using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basket.Service.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsBeingChecked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBeingCheckedOut",
                schema: "basket",
                table: "Baskets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBeingCheckedOut",
                schema: "basket",
                table: "Baskets");
        }
    }
}
