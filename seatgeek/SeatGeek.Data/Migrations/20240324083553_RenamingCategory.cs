using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatGeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamingCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEvents_Categories_ChildCategoryId",
                table: "CategoryEvents");

            migrationBuilder.RenameColumn(
                name: "ChildCategoryId",
                table: "CategoryEvents",
                newName: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEvents_Categories_CategoryId",
                table: "CategoryEvents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEvents_Categories_CategoryId",
                table: "CategoryEvents");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CategoryEvents",
                newName: "ChildCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEvents_Categories_ChildCategoryId",
                table: "CategoryEvents",
                column: "ChildCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
