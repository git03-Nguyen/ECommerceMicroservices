using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Service.Data.Migrations
{
    /// <inheritdoc />
    public partial class Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "user",
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "CreatedAt", "DeletedAt", "Email", "FullName", "IsDeleted", "PaymentDetails", "PhoneNumber", "Role", "UpdatedAt", "UserName" },
                values: new object[] { new Guid("9de65cd0-9b44-4266-a902-d8d907a13671"), "TP Hồ Chí Minh", "", new DateTimeOffset(new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), null, "admin@admin.com", "Quản trị viên", false, "", "0123456789", "Admin", new DateTimeOffset(new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "user",
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("9de65cd0-9b44-4266-a902-d8d907a13671"));
        }
    }
}
