using Kaizen.Roles.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
