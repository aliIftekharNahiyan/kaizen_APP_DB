using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class _28072023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.CreateTable(
                name: "FundingBody",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingBody", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportTypeId = table.Column<long>(type: "bigint", nullable: false),
                    FundingBodyId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllocatedHours = table.Column<int>(type: "int", nullable: false),
                    AllocatedBudget = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionGroup", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FundingBody");

            migrationBuilder.DropTable(
                name: "SessionGroup");

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });
        }
    }
}
