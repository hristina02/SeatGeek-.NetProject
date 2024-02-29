using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeatGeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveParentCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildCategories_ParentCategories_ParentCategoryId",
                table: "ChildCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_ChildCategories_ChildCategoryId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "ParentCategories");

            migrationBuilder.DropIndex(
                name: "IX_ChildCategories_ParentCategoryId",
                table: "ChildCategories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "ChildCategories");

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
                defaultValue: new DateTime(2024, 2, 29, 12, 55, 5, 514, DateTimeKind.Utc).AddTicks(5301),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 2, 28, 14, 21, 8, 647, DateTimeKind.Utc).AddTicks(7950));

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Events",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.UpdateData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Music");

            migrationBuilder.UpdateData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Sport");

            migrationBuilder.UpdateData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Theatre");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 29, 12, 55, 5, 514, DateTimeKind.Utc).AddTicks(5301));

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ChildCategories_CategoryId",
                table: "Events",
                column: "CategoryId",
                principalTable: "ChildCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2024, 2, 29, 12, 55, 5, 514, DateTimeKind.Utc).AddTicks(5301));

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Events",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "ChildCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParentCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentCategories", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ParentCategoryId" },
                values: new object[] { "Pop", 1 });

            migrationBuilder.UpdateData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ParentCategoryId" },
                values: new object[] { "Rock", 1 });

            migrationBuilder.UpdateData(
                table: "ChildCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "ParentCategoryId" },
                values: new object[] { "Football", 2 });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 28, 14, 21, 8, 647, DateTimeKind.Utc).AddTicks(7950));

            migrationBuilder.InsertData(
                table: "ParentCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Sport" },
                    { 3, "Theatre" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildCategories_ParentCategoryId",
                table: "ChildCategories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildCategories_ParentCategories_ParentCategoryId",
                table: "ChildCategories",
                column: "ParentCategoryId",
                principalTable: "ParentCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_ChildCategories_ChildCategoryId",
                table: "Events",
                column: "ChildCategoryId",
                principalTable: "ChildCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
