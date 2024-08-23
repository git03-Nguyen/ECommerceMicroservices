using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Customer.Service.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St", "john-doe@gmail.com", "John Doe", "1234567890" },
                    { 2, "456 Elm St", "jane-doe@gmail.com", "Jane Doe", "0987654321" },
                    { 3, "789 Oak St", "bob.smith@gmail.com", "Bob Smith", "1112223333" },
                    { 4, "321 Maple St", "alice.johnson@gmail.com", "Alice Johnson", "4445556666" },
                    { 5, "654 Pine St", "charlie.brown@gmail.com", "Charlie Brown", "7778889999" },
                    { 6, "987 Cedar St", "emily.davis@gmail.com", "Emily Davis", "2223334444" },
                    { 7, "123 Birch St", "frank.wilson@gmail.com", "Frank Wilson", "5556667777" },
                    { 8, "456 Spruce St", "grace.lee@gmail.com", "Grace Lee", "8889990000" },
                    { 9, "789 Redwood St", "henry.clark@gmail.com", "Henry Clark", "1112224444" },
                    { 10, "321 Fir St", "isabella.martinez@gmail.com", "Isabella Martinez", "3334445555" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
