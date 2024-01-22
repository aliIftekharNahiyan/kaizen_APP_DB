using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class _17012024_user_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "AbpUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "AbpUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AbpUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateofBirth",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DpStatus",
                table: "AbpUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AbpUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ParentFamilyMember",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredCommunicationLanguage",
                table: "AbpUsers",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DateofBirth",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DpStatus",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ParentFamilyMember",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "PreferredCommunicationLanguage",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "AbpUsers");
        }
    }
}
