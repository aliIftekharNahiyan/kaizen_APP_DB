using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class _24072023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BulkProcessJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BulkProcessType = table.Column<int>(type: "int", nullable: false),
                    BulkProcessStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: false),
                    TotalRecords = table.Column<int>(type: "int", nullable: false),
                    SuccessfulRecords = table.Column<int>(type: "int", nullable: false),
                    FailedRecords = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: true),
                    DateCancelled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileUploadedUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    FileResultUrl = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkProcessJob", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BulkProcessJob");
        }
    }
}
