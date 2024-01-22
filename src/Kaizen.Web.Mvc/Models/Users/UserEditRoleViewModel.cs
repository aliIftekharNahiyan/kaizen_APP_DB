using Kaizen.Entities.Addresses.Dto;
using Kaizen.Roles.Dto;
using Kaizen.Users.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Kaizen.Web.Models.Users
{
    public class UserEditRoleViewModel
    {
        public int UserId { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
