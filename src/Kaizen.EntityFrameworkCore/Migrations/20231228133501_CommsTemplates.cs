using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class CommsTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommunicationTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    HeaderTemplateId = table.Column<long>(type: "bigint", nullable: true),
                    FooterTemplateId = table.Column<long>(type: "bigint", nullable: true),
                    TemplateHTMLContentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateType = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationTemplate_CommunicationTemplate_FooterTemplateId",
                        column: x => x.FooterTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommunicationTemplate_CommunicationTemplate_HeaderTemplateId",
                        column: x => x.HeaderTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_FooterTemplateId",
                table: "CommunicationTemplate",
                column: "FooterTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_HeaderTemplateId",
                table: "CommunicationTemplate",
                column: "HeaderTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommunicationTemplate");
        }
    }
}
