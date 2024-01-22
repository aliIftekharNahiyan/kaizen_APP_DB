using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Kaizen.Entities.Addresses;
using Kaizen.Entities.Notes;

namespace Kaizen.Entities.Notes.Dto
{
    [AutoMapFrom(typeof(Note))]
    public class CreateNoteDto : EntityDto<long>
    {
        public string Content { get; set; }

        public long EntityId { get; set; }
        public string EntityType { get; set; }

        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public bool IsInternal { get; set; }
        public bool IsImportant { get; set; }

        public int NoteType { get; set; }
    }
}

