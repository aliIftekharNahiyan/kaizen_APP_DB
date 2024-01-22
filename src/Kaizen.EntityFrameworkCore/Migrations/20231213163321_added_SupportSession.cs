using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class added_SupportSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportSession",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionGroupId = table.Column<long>(type: "bigint", nullable: false),
                    SupportTypeId = table.Column<long>(type: "bigint", nullable: false),
                    FundingBodyId = table.Column<long>(type: "bigint", nullable: false),
                    SessionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionTypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportSession_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupportSession_FundingBody_FundingBodyId",
                        column: x => x.FundingBodyId,
                        principalTable: "FundingBody",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupportSession_SessionGroup_SessionGroupId",
                        column: x => x.SessionGroupId,
                        principalTable: "SessionGroup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupportSession_SessionType_SessionTypeId",
                        column: x => x.SessionTypeId,
                        principalTable: "SessionType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SupportSession_SupportType_SupportTypeId",
                        column: x => x.SupportTypeId,
                        principalTable: "SupportType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportSession_FundingBodyId",
                table: "SupportSession",
                column: "FundingBodyId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportSession_SessionGroupId",
                table: "SupportSession",
                column: "SessionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportSession_SessionTypeId",
                table: "SupportSession",
                column: "SessionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportSession_SupportTypeId",
                table: "SupportSession",
                column: "SupportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportSession_UserId",
                table: "SupportSession",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportSession");
        }
    }
}
