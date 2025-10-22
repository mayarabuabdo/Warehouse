using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class updateRequest3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseItemRequestDetails_RequestId",
                table: "WarehouseItemRequestDetails");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItemRequestDetails_RequestId",
                table: "WarehouseItemRequestDetails",
                column: "RequestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseItemRequestDetails_RequestId",
                table: "WarehouseItemRequestDetails");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItemRequestDetails_RequestId",
                table: "WarehouseItemRequestDetails",
                column: "RequestId");
        }
    }
}
