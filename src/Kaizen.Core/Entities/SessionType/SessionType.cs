using Abp.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Entities.SessionTypes
{
    [Audited]
    public class SessionType:Entity<long>
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }
        
        [Required]
        public bool Deleted { get; set; } =false;
    }
}
