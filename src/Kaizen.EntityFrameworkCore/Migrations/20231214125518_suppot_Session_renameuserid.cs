using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class suppot_Session_renameuserid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportSession_AbpUsers_UserId",
                table: "SupportSession");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SupportSession",
                newName: "SupportProfessionalUserId");

            migrationBuilder.RenameIndex(
                name: "IX_SupportSession_UserId",
                table: "SupportSession",
                newName: "IX_SupportSession_SupportProfessionalUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportSession_AbpUsers_SupportProfessionalUserId",
                table: "SupportSession",
                column: "SupportProfessionalUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportSession_AbpUsers_SupportProfessionalUserId",
                table: "SupportSession");

            migrationBuilder.RenameColumn(
                name: "SupportProfessionalUserId",
                table: "SupportSession",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SupportSession_SupportProfessionalUserId",
                table: "SupportSession",
                newName: "IX_SupportSession_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportSession_AbpUsers_UserId",
                table: "SupportSession",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
