using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Auth.Service.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add3Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("470fa3b5-1b0e-4d11-b992-8b2ada4825b8"), 0, "6e995744-d06e-499c-beff-b494ee11ca3c", "seller@seller.com", false, "Nguời Bán", false, null, null, "SELLER", "AQAAAAIAAYagAAAAEDJkSPcr2H8KP7AOrUyVs+vVund5GaF8wvJS/AlnOgmTOT/IIjaTBjdlPZZypeRegA==", null, false, "UYHS6CDSNIGDOYH5HDOTS4A2YWMSU7CO", false, "seller" },
                    { new Guid("961e9858-1a50-4879-b387-1c2482148515"), 0, "77bfdf97-1d0e-42a1-af9b-92e58e5871c3", "customer@customer.com", false, "Nguời Mua", false, null, null, "CUSTOMER", "AQAAAAIAAYagAAAAEKYgUl4MwIt17ye0TpTrB37oyo8f+xhS7PyqndfRgjDw7d5kSuLzvuCFb4RtyT5e2A==", null, false, "WYCJPDDE7OWUJLMPDNTJYIRCK2IGWOJN", false, "customer" },
                    { new Guid("9de65cd0-9b44-4266-a902-d8d907a13671"), 0, "b5c97c3c-4201-452b-a3c8-e3a74cc1e1f9", "admin@admin.com", false, "Quản Trị Viên", false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEFbM0iIX4wZv1ay/yZApBfh5f6Rv60QDiMxUAvvu+lUfdj3SNhAJpoI+jcvg+v9DbQ==", null, false, "TQXRJCFWDCRPAM7NWGC6DL2G3W5MMXKT", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("eb161112-0780-4099-94cc-c89a78257aff"), new Guid("470fa3b5-1b0e-4d11-b992-8b2ada4825b8") },
                    { new Guid("d999706f-5829-4be8-bc51-05383533dfb3"), new Guid("961e9858-1a50-4879-b387-1c2482148515") },
                    { new Guid("c32ba259-6094-474b-a730-60b8aae724e2"), new Guid("9de65cd0-9b44-4266-a902-d8d907a13671") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("eb161112-0780-4099-94cc-c89a78257aff"), new Guid("470fa3b5-1b0e-4d11-b992-8b2ada4825b8") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d999706f-5829-4be8-bc51-05383533dfb3"), new Guid("961e9858-1a50-4879-b387-1c2482148515") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c32ba259-6094-474b-a730-60b8aae724e2"), new Guid("9de65cd0-9b44-4266-a902-d8d907a13671") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("470fa3b5-1b0e-4d11-b992-8b2ada4825b8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("961e9858-1a50-4879-b387-1c2482148515"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9de65cd0-9b44-4266-a902-d8d907a13671"));
        }
    }
}
