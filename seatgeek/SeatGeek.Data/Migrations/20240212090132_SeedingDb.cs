using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeatGeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 12, 9, 1, 32, 285, DateTimeKind.Utc).AddTicks(5452),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 12, 8, 47, 54, 203, DateTimeKind.Utc).AddTicks(5722));

            migrationBuilder.InsertData(
                table: "ParentCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Sport" },
                    { 3, "Theatre" }
                });

            migrationBuilder.InsertData(
                table: "ChildCategories",
                columns: new[] { "Id", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "Pop", 1 },
                    { 2, "Rock", 1 },
                    { 3, "Football", 2 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Address", "AgentId", "CategoryId", "City", "Description", "ImageUrl", "Title" },
                values: new object[] { 1, "North London, UK (near the border)", new Guid("4bb6ee6b-0068-4112-91d5-475706808d40"), 1, "London", "Dara Ekimova ushers in 2024. with a concept show event on Valentine's Day. Spend February 14 at Bar Petak with the pop girl of the Bulgarian scene and your favorite songs of hers.", "https://bg.content.eventim.com/static/uploaded/bg/3/v/9/g/3v9g_300_300.jpeg", "Dara Ekimova" });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "ApplicationUser", "EventId", "Price", "Quantity", "Type" },
                values: new object[] { 1, new Guid("00000000-0000-0000-0000-000000000000"), 1, 250m, 1, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParentCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParentCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParentCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 12, 8, 47, 54, 203, DateTimeKind.Utc).AddTicks(5722),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 12, 9, 1, 32, 285, DateTimeKind.Utc).AddTicks(5452));
        }
    }
}
