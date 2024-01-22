
using Kaizen.Entities.ContactMethods.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.ContactMethods
{
    public class ContactMethodListViewModel
    {
        public IReadOnlyList<ContactMethodDto> Entities { get; set; }
    }
}