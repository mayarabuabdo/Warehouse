using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class editRequestLog2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLog_AspNetUsers_ActionTokendById",
                table: "RequestLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestLog_ActionTokendById",
                table: "RequestLog");

            migrationBuilder.DropColumn(
                name: "ActionTokendById",
                table: "RequestLog");

            migrationBuilder.AlterColumn<string>(
                name: "ActionTokenById",
                table: "RequestLog",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLog_ActionTokenById",
                table: "RequestLog",
                column: "ActionTokenById");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLog_AspNetUsers_ActionTokenById",
                table: "RequestLog",
                column: "ActionTokenById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLog_AspNetUsers_ActionTokenById",
                table: "RequestLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestLog_ActionTokenById",
                table: "RequestLog");

            migrationBuilder.AlterColumn<string>(
                name: "ActionTokenById",
                table: "RequestLog",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}
