
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Authorization.Users;
using Kaizen.Entities.FundingBodys;
using Kaizen.Entities.FundingBodys.Dto;
using Kaizen.Entities.SessionGroups;
using Kaizen.Entities.SessionTypes;
using Kaizen.Entities.SupportTypes;
using Kaizen.Entities.SupportTypes.Dto;
using Kaizen.Enums;
using Kaizen.Users.Dto;
using Newtonsoft.Json;

namespace Kaizen.Entities.SupportSessions.Dto
{
    [AutoMapTo(typeof(SupportSession))]
    public class SupportSessionCalendarDto : EntityDto<long>
    {
        [Required]
        public long UserId { get; set; }

        public long SupportProfessionalUserId { get; set; }

        [Required]
        [JsonProperty("start")]
        public DateTime SessionStartDate { get; set; }

        [Required]
        [JsonProperty("end")]
        public DateTime SessionEndDate { get; set; }

        [JsonProperty("allDay")]
        public bool AllDay { get; set; } = false;

        [JsonProperty("bgcolour")]
        public string BackgroundColour => Status == SupportSessionStatus.Completed ? "#e3e3e3" :
            Status == SupportSessionStatus.Upcoming ? "#ffb347"
            : "";

        [JsonProperty("title")]
        public string Title => SessionStartDate.ToString("dd/MM/yyyy HH:mm") + " - " + SessionEndDate.ToString("dd/MM/yyyy HH:mm") + " with " + SupportProfessionalUser.FullName;

        [JsonIgnore]
        public virtual UserDto SupportProfessionalUser { get; set; }

        public SupportSessionStatus Status { get; set; }

        public string StatusText { get { return this.Status.ToString(); } }
    }
}