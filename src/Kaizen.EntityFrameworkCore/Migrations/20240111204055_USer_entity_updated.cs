using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class USer_entity_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<long>(
                name: "UserTypeId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PronounId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VisibilityPermissionId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_PronounId",
                table: "AbpUsers",
                column: "PronounId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_UserTypeId",
                table: "AbpUsers",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_VisibilityPermissionId",
                table: "AbpUsers",
                column: "VisibilityPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Lookup_PronounId",
                table: "AbpUsers",
                column: "PronounId",
                principalTable: "Lookup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Lookup_UserTypeId",
                table: "AbpUsers",
                column: "UserTypeId",
                principalTable: "Lookup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_Lookup_VisibilityPermissionId",
                table: "AbpUsers",
                column: "VisibilityPermissionId",
                principalTable: "Lookup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Lookup_PronounId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Lookup_UserTypeId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_Lookup_VisibilityPermissionId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_PronounId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_UserTypeId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_VisibilityPermissionId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "PronounId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "VisibilityPermissionId",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserTypeId",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

        }
    }
}
