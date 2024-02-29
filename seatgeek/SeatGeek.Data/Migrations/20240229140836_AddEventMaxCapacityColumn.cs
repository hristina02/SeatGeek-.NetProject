using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatGeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventMaxCapacityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 29, 14, 8, 36, 328, DateTimeKind.Utc).AddTicks(8908),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 29, 12, 57, 10, 969, DateTimeKind.Utc).AddTicks(3731));

            migrationBuilder.AddColumn<int>(
                name: "MaxCapacity",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "MaxCapacity" },
                values: new object[] { new DateTime(2024, 2, 29, 14, 8, 36, 328, DateTimeKind.Utc).AddTicks(8908), 100 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCapacity",
                table: "Events");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 29, 12, 57, 10, 969, DateTimeKind.Utc).AddTicks(3731),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 29, 14, 8, 36, 328, DateTimeKind.Utc).AddTicks(8908));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 29, 12, 57, 10, 969, DateTimeKind.Utc).AddTicks(3731));
        }
    }
}
