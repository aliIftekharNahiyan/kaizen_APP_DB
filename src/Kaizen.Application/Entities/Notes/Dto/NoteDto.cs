using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Kaizen.Entities.Notes.Dto
{
    [AutoMapFrom(typeof(Note))]
    public class NoteDto : EntityDto<long>
    {
        public string Content { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        public bool IsImportant { get; set; }

        public NoteType NoteType { get; set; }

        public string Title => CreatedDate.ToString("dd/MM/yyyy");

        public string Time => CreatedDate.ToString("HH:mm");

        public string CreatedBy { get; set; }
        [NotMapped]
        public string Name { get; set; }
    }
}