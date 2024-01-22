using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Domain.Entities;

namespace Kaizen.Entities.StorageFiles
{
    [Audited]
    public class StorageFile : Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(1024)]
        public string FileUrl { get; set; }

        [Required]
        [StringLength(256)]
        public string FileName { get; set; }

        [Required]
        [StringLength(128)]
        public string MimeType { get; set; }

        [Required]
        public bool HasBeenSigned { get; set; } = false;

        [Required]
        public bool Deleted { get; set; } = false;
    }
}
