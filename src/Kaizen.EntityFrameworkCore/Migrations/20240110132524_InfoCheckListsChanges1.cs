using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class InfoCheckListsChanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WHayHaveRequestedAssesment",
                table: "InfoCheckList",
                newName: "WhyHaveRequestedAssesment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WhyHaveRequestedAssesment",
                table: "InfoCheckList",
                newName: "WHayHaveRequestedAssesment");
        }
    }
}
