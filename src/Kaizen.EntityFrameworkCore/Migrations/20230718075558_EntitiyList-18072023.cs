using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class EntitiyList18072023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AgreePrivacyPolicy",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AgreeTermsAndConditions",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "AbpUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AbpUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeletedBy",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DoNotContact",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdited",
                table: "AbpUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<bool>(
                name: "MarketingConsent",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "PaymentTermId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pronouns",
                table: "AbpUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "StoreDetailsConsent",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "AbpUsers",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoteType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerm",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    SkillId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSkill_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_PaymentTermId",
                table: "AbpUsers",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_UserId",
                table: "Note",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_UserId",
                table: "UserSkill",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_PaymentTerm_PaymentTermId",
                table: "AbpUsers",
                column: "PaymentTermId",
                principalTable: "PaymentTerm",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_PaymentTerm_PaymentTermId",
                table: "AbpUsers");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "PaymentTerm");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_PaymentTermId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "AgreePrivacyPolicy",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "AgreeTermsAndConditions",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DoNotContact",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LastEdited",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "MarketingConsent",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "PaymentTermId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Pronouns",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "StoreDetailsConsent",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "AbpUsers");
        }
    }
}
