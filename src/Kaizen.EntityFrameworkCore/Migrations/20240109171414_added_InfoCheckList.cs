using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaizen.Migrations
{
    /// <inheritdoc />
    public partial class added_InfoCheckList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "InfoCheckList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DateAnswered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredPronoun = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AgeNow = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UniversityName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NameOfQualification = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CourseStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentFromUkOrOverseas = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PreferredLanguage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DateOfLastEyeTest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WearGlassesLenses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrescribedTintedLens = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfLastHearingTest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnyHearingDifficulties = table.Column<bool>(type: "bit", nullable: false),
                    AnySeriousMedicalConditionsInThePast = table.Column<bool>(type: "bit", nullable: false),
                    AreYouCurrentlyHealthy = table.Column<bool>(type: "bit", nullable: false),
                    AnyMentalHealtDifficulties = table.Column<bool>(type: "bit", nullable: false),
                    WereYouLateLearningToTalk = table.Column<bool>(type: "bit", nullable: false),
                    HadSpeechTherapies = table.Column<bool>(type: "bit", nullable: false),
                    DifficultToSayAnySounds = table.Column<bool>(type: "bit", nullable: false),
                    DoYouHardPronounceWordsNow = table.Column<bool>(type: "bit", nullable: false),
                    CanExpressIdeasFluently = table.Column<bool>(type: "bit", nullable: false),
                    LearnSkillsWalkingRunning = table.Column<bool>(type: "bit", nullable: false),
                    CanSwimAndRideBicycle = table.Column<bool>(type: "bit", nullable: false),
                    AnySportActivitiesNow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClumsyAsAChild = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillsUsingYourHands = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FindEasyToDevelopNeatHandwriting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedAnyPreviousAssesments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloseFamilyMembersWithLearningDifferences = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloseFamilyMembersWithDifficultyDevelopingLiteracy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaveAlwaysLivedInUK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnglishEverydayLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegularySpeakAnyOtherLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WereSchoolLessionsInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FindEasyToReadWriteOtherLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnjoyGoingToSchool = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FavouriteSubjects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HowDidYouFindLearningToRead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HowDidYouGetRememberingWhatRead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HowDidFindLearningToSpell = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HowDidFindWritingEssays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HowDidOnDevelopingNumeracySkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HardToCompleteExaminationsInTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DidYouHaveSSEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendSchoolRegularly = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnyProlongedPeriodsOfAbsence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DidChangedSchoolsOften = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultsOfALevelExaminations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobsDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatEnjoyedOnJobsDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WHayHaveRequestedAssesment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YourStrengthsAsPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStrengthsWithReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStrengthsWithTasksAsPlanning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStrengthsWithMemory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStrengthsWithSocialInteraction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStrengthsWithNumeracy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriveACar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FreeTimeActivities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadachesReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadingSoreEyes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadingTired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistractedWhenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessComfortableLongerReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrefferDimLights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadingFromWhitePaperBRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadingFromWhitePageFormsPatterns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoesBackgorundShimmer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoesPrintApoearJitter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScrewEyesWhenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RubEyesWhenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseMarkerOrFingerWhenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverOneEyeWhenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LosePlaceWHenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RereadOrSkipWhenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextBlurredWhenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistanceObjectsBlurred = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoubleWordsWhenReading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoCheckList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoCheckList_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

          
            migrationBuilder.CreateIndex(
                name: "IX_InfoCheckList_UserId",
                table: "InfoCheckList",
                column: "UserId");

       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoCheckList");

         
        }
    }
}
