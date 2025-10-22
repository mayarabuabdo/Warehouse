using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Migrations
{
    /// <inheritdoc />
    public partial class addRequestTypeStepStatusGroupDocumentcsAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "RequestTypeStepStatusGroupDocumentAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StepId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypeStepStatusGroupDocumentAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentAction_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentAction_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentAction_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentAction_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentAction_RequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentAction_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentAction_Step_StepId",
                        column: x => x.StepId,
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentAction_ActionId",
                table: "RequestTypeStepStatusGroupDocumentAction",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentAction_DocumentId",
                table: "RequestTypeStepStatusGroupDocumentAction",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentAction_DocumentTypeId",
                table: "RequestTypeStepStatusGroupDocumentAction",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentAction_GroupId",
                table: "RequestTypeStepStatusGroupDocumentAction",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentAction_RequestTypeId",
                table: "RequestTypeStepStatusGroupDocumentAction",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentAction_StatusId",
                table: "RequestTypeStepStatusGroupDocumentAction",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentAction_StepId",
                table: "RequestTypeStepStatusGroupDocumentAction",
                column: "StepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestTypeStepStatusGroupDocumentAction");

            migrationBuilder.CreateTable(
                name: "RequestTypeStepStatusGroupDocumentcs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypeStepStatusGroupDocumentcs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentcs_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentcs_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentcs_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentcs_RequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentcs_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestTypeStepStatusGroupDocumentcs_Step_StepId",
                        column: x => x.StepId,
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentcs_DocumentId",
                table: "RequestTypeStepStatusGroupDocumentcs",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentcs_DocumentTypeId",
                table: "RequestTypeStepStatusGroupDocumentcs",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentcs_GroupId",
                table: "RequestTypeStepStatusGroupDocumentcs",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentcs_RequestTypeId",
                table: "RequestTypeStepStatusGroupDocumentcs",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentcs_StatusId",
                table: "RequestTypeStepStatusGroupDocumentcs",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTypeStepStatusGroupDocumentcs_StepId",
                table: "RequestTypeStepStatusGroupDocumentcs",
                column: "StepId");
        }
    }
}
