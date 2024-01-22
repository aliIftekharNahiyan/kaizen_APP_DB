using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class suppot_Session_added_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "SupportSession",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SupportSession_UserId",
                table: "SupportSession",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportSession_AbpUsers_UserId",
                table: "SupportSession",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id"
          );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportSession_AbpUsers_UserId",
                table: "SupportSession");

            migrationBuilder.DropIndex(
                name: "IX_SupportSession_UserId",
                table: "SupportSession");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SupportSession");
        }
    }
}
