using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;
using Kaizen.Entities.AcademicYears;
using Kaizen.Entities.Companies;
using Kaizen.Enums;

namespace Kaizen.Entities.InfoChecklists
{
    [Audited]
    public class InfoCheckList : Entity<long>
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public DateTime DateAnswered { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(256)]
        public string Surname { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [StringLength(256)]
        public string PreferredPronoun { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public int AgeNow { get; set; }

        [Required]
        [StringLength(256)]
        public string Address { get; set; }

        [Required]
        [StringLength(256)]
        public string TelephoneNumber { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        public string UniversityName { get; set; }
        [Required]
        [StringLength(256)]
        public string NameOfQualification { get; set; }

        [Required]
        public DateTime CourseStartDate { get; set; }

        [Required]
        public DateTime CourseEndDate { get; set; }
        [Required]
        [StringLength(256)]
        public string StudentFromUkOrOverseas { get; set; }
        [Required]
        [StringLength(256)]
        public string PreferredLanguage { get; set; }
        [Required]
        public DateTime DateOfLastEyeTest { get; set; }
        public string WearGlassesLenses { get; set; }
        public string PrescribedTintedLens { get; set; }

        [Required]
        public DateTime DateOfLastHearingTest { get; set; }
        [Required]
        public string AnyHearingDifficulties { get; set; }
        [Required]
        public string AnySeriousMedicalConditionsInThePast { get; set; }
        [Required]

        public string AreYouCurrentlyHealthy { get; set; }
        [Required]
        public string AnyMentalHealtDifficulties { get; set; }
        [Required]
        public string WereYouLateLearningToTalk { get; set; }
        [Required]
        public string HadSpeechTherapies { get; set; }
        [Required]
        public string DifficultToSayAnySounds { get; set; }
        [Required]
        public string DoYouHardPronounceWordsNow { get; set; }
        [Required]
        public string CanExpressIdeasFluently { get; set; }
        [Required]
        public string LearnSkillsWalkingRunning { get; set; }
        [Required]
        public string CanSwimAndRideBicycle { get; set; }
        [Required]
 
        public string AnySportActivitiesNow { get; set; }
        [Required]

        public string ClumsyAsAChild { get; set; }
        [Required]
    
        public string SkillsUsingYourHands { get; set; }
        [Required]
      
        public string FindEasyToDevelopNeatHandwriting { get; set; }
        [Required]
        public string ReceivedAnyPreviousAssesments { get; set; }
        [Required]

        public string CloseFamilyMembersWithLearningDifferences { get; set; }
        [Required]
      
        public string CloseFamilyMembersWithDifficultyDevelopingLiteracy { get; set; }
        [Required]
       
        public string HaveAlwaysLivedInUK { get; set; }
        [Required]

        public string IsEnglishEverydayLanguage { get; set; }
        [Required]

        public string RegularySpeakAnyOtherLanguage { get; set; }
        [Required]

        public string WereSchoolLessionsInEnglish { get; set; }
        [Required]

        public string FindEasyToReadWriteOtherLanguages { get; set; }
        [Required]
 
        public string EnjoyGoingToSchool { get; set; }
        [Required]

        public string FavouriteSubjects { get; set; }
        [Required]

        public string HowDidYouFindLearningToRead { get; set; }
        [Required]

        public string HowDidYouGetRememberingWhatRead { get; set; }
        [Required]
     
        public string HowDidFindLearningToSpell { get; set; }
        [Required]
      
        public string HowDidFindWritingEssays { get; set; }
        [Required]
   
        public string HowDidOnDevelopingNumeracySkills { get; set; }
        [Required]
       
        public string HardToCompleteExaminationsInTime { get; set; }
        [Required]

        public string DidYouHaveSSEN { get; set; }
        [Required]
      
        public string AttendSchoolRegularly { get; set; }
        [Required]
     
        public string AnyProlongedPeriodsOfAbsence { get; set; }
        [Required]
   
        public string DidChangedSchoolsOften { get; set; }
        [Required]

        public string ResultsOfALevelExaminations { get; set; }
        [Required]

        public string JobsDone { get; set; }
        [Required]

        public string WhatEnjoyedOnJobsDone { get; set; }
        [Required]

        public string WhyHaveRequestedAssesment { get; set; }
        [Required]

        public string YourStrengthsAsPerson { get; set; }
        [Required]

        public string CurrentStrengthsWithReading { get; set; }
        [Required]

        public string CurrentStrengthsWithTasksAsPlanning { get; set; }
        [Required]

        public string CurrentStrengthsWithMemory { get; set; }
        [Required]

        public string CurrentStrengthsWithSocialInteraction { get; set; }
        [Required]
        public string CurrentStrengthsWithNumeracy { get; set; }
        [Required]
        public string DriveACar { get; set; }
        [Required]

        public string FreeTimeActivities { get; set; }

        [Required]
        public string HeadachesReading { get; set; }
        [Required]
        public string ReadingSoreEyes { get; set; }
        [Required]
        public string ReadingTired { get; set; }
        [Required]
        public string DistractedWhenReading { get; set; }
        [Required]
        public string LessComfortableLongerReading { get; set; }
        [Required]
        public string PrefferDimLights { get; set; }
        [Required]
        public string ReadingFromWhitePaperBright { get; set; }
        [Required]
        public string ReadingFromWhitePageFormsPatterns { get; set; }
        [Required]
        public string DoesBackgorundShimmer { get; set; }
        [Required]
        public string DoesPrintApoearJitter { get; set; }
        [Required]
        public string ScrewEyesWhenReading { get; set; }
        [Required]
        public string RubEyesWhenReading { get; set; }
        [Required]
        public string MoveEyesAndBlinkWhenReading { get; set; }
        [Required]
        public string UseMarkerOrFingerWhenReading { get; set; }
        [Required]
        public string CoverOneEyeWhenReading { get; set; }
        [Required]
        public string LosePlaceWHenReading { get; set; }
        [Required]
        public string RereadOrSkipWhenReading { get; set; }
        [Required]
        public string TextBlurredWhenReading { get; set; }
        [Required]
        public string DistanceObjectsBlurred { get; set; }
        [Required]
        public string DoubleWordsWhenReading { get; set; }
        [Required]
        public string AdditionalInformation { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}
