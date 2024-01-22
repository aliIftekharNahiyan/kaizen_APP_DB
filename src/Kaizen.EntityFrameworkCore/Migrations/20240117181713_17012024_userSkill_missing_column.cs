using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class _17012024_userSkill_missing_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "UserSkill",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserSkill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserSkill",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserSkill",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "UserSkill",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "UserSkill",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserSkill");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserSkill");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserSkill");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserSkill");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserSkill");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "UserSkill");
        }
    }
}
