using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatGeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewNameForCategoryColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEvents_ChildCategories_ChildCategoryId",
                table: "CategoryEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_ChildCategories_CategoryId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildCategories",
                table: "ChildCategories");

            migrationBuilder.RenameTable(
                name: "ChildCategories",
                newName: "Categories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 29, 12, 57, 10, 969, DateTimeKind.Utc).AddTicks(3731),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 29, 12, 55, 5, 514, DateTimeKind.Utc).AddTicks(5301));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 29, 12, 57, 10, 969, DateTimeKind.Utc).AddTicks(3731));

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEvents_Categories_ChildCategoryId",
                table: "CategoryEvents",
                column: "ChildCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEvents_Categories_ChildCategoryId",
                table: "CategoryEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Categories_CategoryId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "ChildCategories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 29, 12, 55, 5, 514, DateTimeKind.Utc).AddTicks(5301),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 29, 12, 57, 10, 969, DateTimeKind.Utc).AddTicks(3731));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildCategories",
                table: "ChildCategories",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 29, 12, 55, 5, 514, DateTimeKind.Utc).AddTicks(5301));

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEvents_ChildCategories_ChildCategoryId",
                table: "CategoryEvents",
                column: "ChildCategoryId",
                principalTable: "ChildCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
