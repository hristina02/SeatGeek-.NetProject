using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatGeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditNameOfOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Events_EventID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "Orders",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EventID",
                table: "Orders",
                newName: "IX_Orders_EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Events_EventId",
                table: "Orders",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Events_EventId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Orders",
                newName: "EventID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EventId",
                table: "Orders",
                newName: "IX_Orders_EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Events_EventID",
                table: "Orders",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
