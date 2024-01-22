using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class NoteTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EntityId",
                table: "Note",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "EntityType",
                table: "Note",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "Note");
        }
    }
}
