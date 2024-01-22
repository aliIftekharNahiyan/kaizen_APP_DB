using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;
using Kaizen.Entities.CustomerTypes;
using Kaizen.Entities.Feedbacks;
using Kaizen.Entities.LinkTables;
using Kaizen.Entities.Lookups;
using Kaizen.Entities.Notes;
using Kaizen.Entities.PaymentTerms;
using Kaizen.Entities.PhoneNumbers;
using Kaizen.Entities.Universitys;
using Kaizen.Entities.UserClientSupports;
using Kaizen.Entities.UserKins;
using Kaizen.Entities.UserLookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Authorization.Users
{
    [Audited]
    public class User : AbpUser<User>
    {
        public User()
        {
            Addresses = new List<UserAddress>();
            PhoneNumbers = new List<PhoneNumber>();

        }

        public const string DefaultPassword = "123qwe";
        public long? PronounId { get; set; }

        [StringLength(512)]
        public string Website { get; set; }

        public long? PaymentTermId { get; set; }
        public long? UniversityId { get; set; }
        public long? CustomerTypeId { get; set; }


        public long? UserTypeId { get; set; }
        public bool MarketingConsent { get; set; }
        public bool StoreDetailsConsent { get; set; }
       // public bool AgreeTermsAndConditions { get; set; }
        public bool AgreePrivacyPolicy { get; set; }
        public bool DoNotContact { get; set; }


        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string Surname { get; set; }


        public DateTime? DateofBirth { get; set; }

        [MaxLength(36)]
        public string PreferredCommunicationLanguage { get; set; }
        public bool? DpStatus { get; set; }
        public bool? AgreeTC { get; set; }
        [MaxLength(128)]
        public string MedicalHistory { get; set; }

        public long? ParentFamilyMember { get; set; }

        public bool? Archived { get; set; }

        public DateTime? StartDate { get; set; }

        public long? VisibilityPermissionId { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }

        [ForeignKey(nameof(PaymentTermId))]
        public virtual PaymentTerm PaymentTerm { get; set; }
        public virtual List<UserAddress> Addresses { get; set; }
        public virtual List<PhoneNumber> PhoneNumbers { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<UserSkill> Skills { get; set; }
        public virtual List<UserClientSupport> UserClientSupports { get; set; }
        public virtual List<UserDisability> Disabilities { get; set; }
        public virtual List<UserLivingRegionLocation> LivingRegionLocations { get; set; }
        public virtual List<UserWorkRegionLocation> WorkRegionLocations { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public virtual University University { get; set; }
        [ForeignKey(nameof(CustomerTypeId))]
        public virtual CustomerType CustomerType { get; set; }
        [ForeignKey(nameof(PronounId))]
        public virtual Lookup Pronoun { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        public virtual Lookup UserType { get; set; }

        [ForeignKey(nameof(VisibilityPermissionId))]
        public virtual Lookup VisibilityPermission { get; set; }
        public virtual List<UserLookup> UserLookups { get; set; }
        public virtual List<UserKin> UserKins { get; set; }
        public virtual List<Feedback> Feedbacks { get; set; }
    }
}