using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class InfoCheckListChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReadingFromWhitePaperBRight",
                table: "InfoCheckList",
                newName: "ReadingFromWhitePaperBright");

            migrationBuilder.AlterColumn<string>(
                name: "WereYouLateLearningToTalk",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "LearnSkillsWalkingRunning",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "HadSpeechTherapies",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "DoYouHardPronounceWordsNow",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "DifficultToSayAnySounds",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "CanSwimAndRideBicycle",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "CanExpressIdeasFluently",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "AreYouCurrentlyHealthy",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "AnySeriousMedicalConditionsInThePast",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "AnyMentalHealtDifficulties",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "AnyHearingDifficulties",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "MoveEyesAndBlinkWhenReading",
                table: "InfoCheckList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoveEyesAndBlinkWhenReading",
                table: "InfoCheckList");

            migrationBuilder.RenameColumn(
                name: "ReadingFromWhitePaperBright",
                table: "InfoCheckList",
                newName: "ReadingFromWhitePaperBRight");

            migrationBuilder.AlterColumn<bool>(
                name: "WereYouLateLearningToTalk",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "LearnSkillsWalkingRunning",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "HadSpeechTherapies",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "DoYouHardPronounceWordsNow",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "DifficultToSayAnySounds",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "CanSwimAndRideBicycle",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "CanExpressIdeasFluently",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "AreYouCurrentlyHealthy",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "AnySeriousMedicalConditionsInThePast",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "AnyMentalHealtDifficulties",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "AnyHearingDifficulties",
                table: "InfoCheckList",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
