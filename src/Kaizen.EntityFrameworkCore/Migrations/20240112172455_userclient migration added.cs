using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class userclientmigrationadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserClientSupport_ClientId",
                table: "UserClientSupport");

            migrationBuilder.AddColumn<string>(
                name: "MedicalHistory",
                table: "AbpUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClientSupport_ClientId",
                table: "UserClientSupport",
                column: "ClientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserClientSupport_ClientId",
                table: "UserClientSupport");

            migrationBuilder.AlterColumn<string>(
                name: "MedicalHistory",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClientSupport_ClientId",
                table: "UserClientSupport",
                column: "ClientId");
        }
    }
}
