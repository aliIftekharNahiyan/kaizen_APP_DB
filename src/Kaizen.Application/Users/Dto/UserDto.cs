using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.CustomerTypes.Dto;
using Kaizen.Entities.LinkTables;
using Kaizen.Entities.Lookups;
using Kaizen.Entities.Lookups.Dto;
using Kaizen.Entities.Notes.Dto;
using Kaizen.Entities.PaymentTerms.Dto;
using Kaizen.Entities.Universitys.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDto : EntityDto<long>
    {
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(AbpUserBase.MaxUserNameLength)]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
        [MaxLength(36)]
        public string PreferredCommunicationLanguage { get; set; }
        public bool IsActive { get; set; }
        public bool Archived { get; set; }
        public bool DpStatus { get; set; }
        public bool AgreeTC { get; set; }
        public string FullName { get; set; }



        [Required(ErrorMessage = "Website is required")]
        [StringLength(512)]
        public string Website { get; set; }
        [Required(ErrorMessage = "Medical History is required")]
        public string MedicalHistory { get; set; }
        public DateTime? LastLoginTime { get; set; }


        [Required(ErrorMessage = "User Type is required")]
        public long UserTypeId { get; set; }
        [Required(ErrorMessage = "Payment Type is required")]
        public long? PaymentTermId { get; set; }
        [Required(ErrorMessage = "University is required")]
        public long? UniversityId { get; set; }
        [Required(ErrorMessage = "Customer Type is required")]
        public long? CustomerTypeId { get; set; }
        [Required(ErrorMessage = "Skill is required")]
        public long[] SkillId { get; set; }
        public long[] NoteId { get; set; }
        [Required(ErrorMessage = "RCS is required")]
        public long[] UserClientSupportId { get; set; }


        [Required(ErrorMessage = "Pronoun is required")]
        public long? PronounId { get; set; }
        public long[] ClientSupportId { get; set; }
        [Required(ErrorMessage = "Work Area is required")]
        public long[] WRegionId { get; set; }
        [Required(ErrorMessage = "User Location is required")]
        public long[] LRegionId { get; set; }
        public string[] RoleNames { get; set; }
        public long? VisibilityPermissionId { get; set; }


        public bool MarketingConsent { get; set; }
        public bool StoreDetailsConsent { get; set; }       
        public bool AgreePrivacyPolicy { get; set; }
        public bool DoNotContact { get; set; }
        public UniversityDto University { get; set; }

        [ForeignKey(nameof(NoteId))]
        public NoteDto Note { get; set; }

        public LookupDto ClientSupport { get; set; }

        [ForeignKey(nameof(PronounId))]
        public virtual Lookup Pronoun { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        public virtual Lookup UserType { get; set; }

        [ForeignKey(nameof(VisibilityPermissionId))]
        public virtual Lookup VisibilityPermission { get; set; }
        public virtual List<UserSkillDto> Skills { get; set; }
        public virtual List<UserClientSupportDto> UserClientSupports { get; set; }


        public virtual List<UserLivingRegionLocationDto> LivingRegionLocations { get; set; }
        public virtual List<UserWorkRegionLocationDto> WorkRegionLocations { get; set; }

        [ForeignKey(nameof(PaymentTermId))]
        public virtual PaymentTermDto PaymentTerm { get; set; }

        [ForeignKey(nameof(CustomerTypeId))]
        public virtual CustomerTypeDto CustomerType { get; set; }
    }
}
