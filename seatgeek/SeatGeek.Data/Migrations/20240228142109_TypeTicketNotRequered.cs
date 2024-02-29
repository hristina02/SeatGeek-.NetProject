using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatGeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class TypeTicketNotRequered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ChildCategories_CategoryId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Events",
                newName: "ChildCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                newName: "IX_Events_ChildCategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 28, 14, 21, 8, 647, DateTimeKind.Utc).AddTicks(7950),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 12, 9, 1, 32, 285, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 28, 14, 21, 8, 647, DateTimeKind.Utc).AddTicks(7950));

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ChildCategories_ChildCategoryId",
                table: "Events",
                column: "ChildCategoryId",
                principalTable: "ChildCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_ChildCategories_ChildCategoryId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "ChildCategoryId",
                table: "Events",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ChildCategoryId",
                table: "Events",
                newName: "IX_Events_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 12, 9, 1, 32, 285, DateTimeKind.Utc).AddTicks(5452),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 28, 14, 21, 8, 647, DateTimeKind.Utc).AddTicks(7950));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 12, 9, 1, 32, 285, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ChildCategories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "ChildCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
