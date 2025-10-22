using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class updateRequest4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Step_StausId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Status_StausId",
                table: "Requests",
                column: "StausId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Status_StausId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Step_StausId",
                table: "Requests",
                column: "StausId",
                principalTable: "Step",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
