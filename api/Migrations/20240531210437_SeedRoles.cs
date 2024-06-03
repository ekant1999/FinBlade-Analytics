using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25a25b38-bd5a-4e60-8ac9-1a538130a1fa", null, "Admin", "ADMIN" },
                    { "6c8a7822-baff-4f31-bf26-e79e6fab5ab3", null, "Moderator", "MODERATOR" },
                    { "6de573f1-72ab-48d5-a3d7-1080fffe0b15", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25a25b38-bd5a-4e60-8ac9-1a538130a1fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c8a7822-baff-4f31-bf26-e79e6fab5ab3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6de573f1-72ab-48d5-a3d7-1080fffe0b15");
        }
    }
}
