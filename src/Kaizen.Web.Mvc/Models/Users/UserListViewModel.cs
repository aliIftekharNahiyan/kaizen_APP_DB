using Kaizen.Roles.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
        public string Signature { get; set; }
    }
}
