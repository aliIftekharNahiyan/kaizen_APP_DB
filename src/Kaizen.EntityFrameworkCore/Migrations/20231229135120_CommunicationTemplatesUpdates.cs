using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class CommunicationTemplatesUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "CommunicationTemplate",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemplateArea",
                table: "CommunicationTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_CompanyId",
                table: "CommunicationTemplate",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTemplate_Company_CompanyId",
                table: "CommunicationTemplate",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTemplate_Company_CompanyId",
                table: "CommunicationTemplate");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplate_CompanyId",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "TemplateArea",
                table: "CommunicationTemplate");
        }
    }
}
