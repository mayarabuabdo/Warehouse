using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class editAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionId",
                table: "RequestDocumentTemp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RequestDocumentTemp_ActionId",
                table: "RequestDocumentTemp",
                column: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDocumentTemp_Actions_ActionId",
                table: "RequestDocumentTemp",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDocumentTemp_Actions_ActionId",
                table: "RequestDocumentTemp");

            migrationBuilder.DropIndex(
                name: "IX_RequestDocumentTemp_ActionId",
                table: "RequestDocumentTemp");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "RequestDocumentTemp");
        }
    }
}
