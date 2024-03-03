using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatGeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCreatedOn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 29, 14, 23, 7, 558, DateTimeKind.Utc).AddTicks(6779));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 29, 14, 23, 7, 558, DateTimeKind.Utc).AddTicks(6779),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 29, 14, 23, 7, 558, DateTimeKind.Utc).AddTicks(6779));
        }
    }
}
