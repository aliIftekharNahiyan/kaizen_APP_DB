using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class MainLinkEntities27072023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "DeletedDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LastEdited",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "AbpUsers",
                newName: "UniversityId");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Address",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "CustomerTypeId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactMethod",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disability",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageFile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    HasBeenSigned = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDisability",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DisabilityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDisability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDisability_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLivingRegionLocation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RegionLocationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLivingRegionLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLivingRegionLocation_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkRegionLocation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RegionLocationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkRegionLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkRegionLocation_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegionLocation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionLocation_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_CustomerTypeId",
                table: "AbpUsers",
                column: "CustomerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_UniversityId",
                table: "AbpUsers",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionLocation_RegionId",
                table: "RegionLocation",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDisability_UserId",
                table: "UserDisability",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLivingRegionLocation_UserId",
                table: "UserLivingRegionLocation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkRegionLocation_UserId",
                table: "UserWorkRegionLocation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_CustomerType_CustomerTypeId",
                table: "AbpUsers",
                column: "CustomerTypeId",
                principalTable: "CustomerType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_University_UniversityId",
                table: "AbpUsers",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_CustomerType_CustomerTypeId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_University_UniversityId",
                table: "AbpUsers");

            migrationBuilder.DropTable(
                name: "ContactMethod");

            migrationBuilder.DropTable(
                name: "CustomerType");

            migrationBuilder.DropTable(
                name: "Disability");

            migrationBuilder.DropTable(
                name: "RegionLocation");

            migrationBuilder.DropTable(
                name: "StorageFile");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropTable(
                name: "UserDisability");

            migrationBuilder.DropTable(
                name: "UserLivingRegionLocation");

            migrationBuilder.DropTable(
                name: "UserWorkRegionLocation");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_CustomerTypeId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_UniversityId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CustomerTypeId",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "UniversityId",
                table: "AbpUsers",
                newName: "DeletedBy");

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
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdited",
                table: "AbpUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
