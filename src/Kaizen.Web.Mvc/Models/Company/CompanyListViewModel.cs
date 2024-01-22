
using Kaizen.Entities.Companies.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.Companies
{
    public class CompanyListViewModel
    {
        public IReadOnlyList<CompanyDto> Entities { get; set; }
    }
}