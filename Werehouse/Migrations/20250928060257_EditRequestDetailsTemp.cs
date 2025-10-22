using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class EditRequestDetailsTemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedAt",
                table: "RequestDocumentTemp",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "RequestDocumentTemp",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDocumentTemp_CreatedById",
                table: "RequestDocumentTemp",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDocumentTemp_AspNetUsers_CreatedById",
                table: "RequestDocumentTemp",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDocumentTemp_AspNetUsers_CreatedById",
                table: "RequestDocumentTemp");

            migrationBuilder.DropIndex(
                name: "IX_RequestDocumentTemp_CreatedById",
                table: "RequestDocumentTemp");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RequestDocumentTemp");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "RequestDocumentTemp");
        }
    }
}
