using Kaizen.Entities.Addresses.Dto;
using Kaizen.Entities.UserKins.Dto;
using Kaizen.Roles.Dto;
using Kaizen.Users.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Kaizen.Web.Models.Users
{
    public class UserEditViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public List<AddressDto> Addresses { get; set; }
        public List<UserKinDto> UserKins { get; set; }
        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
        public int IsSuccess { get; set; } = 0;
    }
}
