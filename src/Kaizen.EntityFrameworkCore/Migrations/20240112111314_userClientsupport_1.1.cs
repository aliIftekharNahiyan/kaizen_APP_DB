using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class userClientsupport_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserClientSupport",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClientSupport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClientSupport_AbpUsers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id"                        );
                    table.ForeignKey(
                        name: "FK_UserClientSupport_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id"                        );
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClientSupport_ClientId",
                table: "UserClientSupport",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClientSupport_UserId",
                table: "UserClientSupport",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClientSupport");
        }
    }
}
