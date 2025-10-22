using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class EditRequestLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLog_AspNetUsers_CreatedById",
                table: "RequestLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestLog_CreatedById",
                table: "RequestLog");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "RequestLog");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "RequestLog",
                newName: "ActionTokenById");

            migrationBuilder.AddColumn<string>(
                name: "ActionTokenAt",
                table: "RequestLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActionTokendById",
                table: "RequestLog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestLog_ActionTokendById",
                table: "RequestLog",
                column: "ActionTokendById");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLog_AspNetUsers_ActionTokendById",
                table: "RequestLog",
                column: "ActionTokendById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLog_AspNetUsers_ActionTokendById",
                table: "RequestLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestLog_ActionTokendById",
                table: "RequestLog");

            migrationBuilder.DropColumn(
                name: "ActionTokenAt",
                table: "RequestLog");

            migrationBuilder.DropColumn(
                name: "ActionTokendById",
                table: "RequestLog");

            migrationBuilder.RenameColumn(
                name: "ActionTokenById",
                table: "RequestLog",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "RequestLog",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLog_CreatedById",
                table: "RequestLog",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLog_AspNetUsers_CreatedById",
                table: "RequestLog",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
