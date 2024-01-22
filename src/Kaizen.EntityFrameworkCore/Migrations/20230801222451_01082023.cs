using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class _01082023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportType_Checklist_ChecklistId",
                table: "SupportType");

            migrationBuilder.RenameColumn(
                name: "ChecklistId",
                table: "SupportType",
                newName: "CheckListId");

            migrationBuilder.RenameIndex(
                name: "IX_SupportType_ChecklistId",
                table: "SupportType",
                newName: "IX_SupportType_CheckListId");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "SessionGroup",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SessionGroup_UserId",
                table: "SessionGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItemOption_ChecklistItemId",
                table: "ChecklistItemOption",
                column: "ChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistChecklistItem_CheckListId",
                table: "ChecklistChecklistItem",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistChecklistItem_ChecklistItemId",
                table: "ChecklistChecklistItem",
                column: "ChecklistItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistChecklistItem_ChecklistItem_ChecklistItemId",
                table: "ChecklistChecklistItem",
                column: "ChecklistItemId",
                principalTable: "ChecklistItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistChecklistItem_Checklist_CheckListId",
                table: "ChecklistChecklistItem",
                column: "CheckListId",
                principalTable: "Checklist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistItemOption_ChecklistItem_ChecklistItemId",
                table: "ChecklistItemOption",
                column: "ChecklistItemId",
                principalTable: "ChecklistItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionGroup_AbpUsers_UserId",
                table: "SessionGroup",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportType_Checklist_CheckListId",
                table: "SupportType",
                column: "CheckListId",
                principalTable: "Checklist",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistChecklistItem_ChecklistItem_ChecklistItemId",
                table: "ChecklistChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistChecklistItem_Checklist_CheckListId",
                table: "ChecklistChecklistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistItemOption_ChecklistItem_ChecklistItemId",
                table: "ChecklistItemOption");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionGroup_AbpUsers_UserId",
                table: "SessionGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportType_Checklist_CheckListId",
                table: "SupportType");

            migrationBuilder.DropIndex(
                name: "IX_SessionGroup_UserId",
                table: "SessionGroup");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistItemOption_ChecklistItemId",
                table: "ChecklistItemOption");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistChecklistItem_CheckListId",
                table: "ChecklistChecklistItem");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistChecklistItem_ChecklistItemId",
                table: "ChecklistChecklistItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SessionGroup");

            migrationBuilder.RenameColumn(
                name: "CheckListId",
                table: "SupportType",
                newName: "ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_SupportType_CheckListId",
                table: "SupportType",
                newName: "IX_SupportType_ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportType_Checklist_ChecklistId",
                table: "SupportType",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id");
        }
    }
}
