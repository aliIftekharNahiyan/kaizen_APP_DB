using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class Checklists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChecklistId",
                table: "SupportType",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checklist",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistChecklistItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListId = table.Column<long>(type: "bigint", nullable: false),
                    ChecklistItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistChecklistItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItemOption",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ChecklistItemId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItemOption", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportType_ChecklistId",
                table: "SupportType",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionGroup_FundingBodyId",
                table: "SessionGroup",
                column: "FundingBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionGroup_SupportTypeId",
                table: "SessionGroup",
                column: "SupportTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionGroup_FundingBody_FundingBodyId",
                table: "SessionGroup",
                column: "FundingBodyId",
                principalTable: "FundingBody",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionGroup_SupportType_SupportTypeId",
                table: "SessionGroup",
                column: "SupportTypeId",
                principalTable: "SupportType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupportType_Checklist_ChecklistId",
                table: "SupportType",
                column: "ChecklistId",
                principalTable: "Checklist",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionGroup_FundingBody_FundingBodyId",
                table: "SessionGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionGroup_SupportType_SupportTypeId",
                table: "SessionGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_SupportType_Checklist_ChecklistId",
                table: "SupportType");

            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.DropTable(
                name: "ChecklistChecklistItem");

            migrationBuilder.DropTable(
                name: "ChecklistItem");

            migrationBuilder.DropTable(
                name: "ChecklistItemOption");

            migrationBuilder.DropIndex(
                name: "IX_SupportType_ChecklistId",
                table: "SupportType");

            migrationBuilder.DropIndex(
                name: "IX_SessionGroup_FundingBodyId",
                table: "SessionGroup");

            migrationBuilder.DropIndex(
                name: "IX_SessionGroup_SupportTypeId",
                table: "SessionGroup");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "SupportType");
        }
    }
}
