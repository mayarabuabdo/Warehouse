using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWarehouseItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseName",
                table: "WarehouseItemRequestDetails");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItemRequestDetails_WarehouseId",
                table: "WarehouseItemRequestDetails",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseItemRequestDetails_warehouses_WarehouseId",
                table: "WarehouseItemRequestDetails",
                column: "WarehouseId",
                principalTable: "warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseItemRequestDetails_warehouses_WarehouseId",
                table: "WarehouseItemRequestDetails");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseItemRequestDetails_WarehouseId",
                table: "WarehouseItemRequestDetails");

            migrationBuilder.AddColumn<string>(
                name: "WarehouseName",
                table: "WarehouseItemRequestDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
