using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class edited_AcademicYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Company_CompanyId",
                table: "AcademicYear");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "AcademicYear",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Company_CompanyId",
                table: "AcademicYear",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicYear_Company_CompanyId",
                table: "AcademicYear");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "AcademicYear",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicYear_Company_CompanyId",
                table: "AcademicYear",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
