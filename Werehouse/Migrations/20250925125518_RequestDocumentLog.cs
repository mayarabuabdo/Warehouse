using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class RequestDocumentLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "RequestDocumentLog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RequestDocumentLog_DocumentId",
                table: "RequestDocumentLog",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDocumentLog_Document_DocumentId",
                table: "RequestDocumentLog",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDocumentLog_Document_DocumentId",
                table: "RequestDocumentLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestDocumentLog_DocumentId",
                table: "RequestDocumentLog");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "RequestDocumentLog");
        }
    }
}
