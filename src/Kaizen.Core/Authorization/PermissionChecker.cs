using Abp.Authorization;
using Kaizen.Authorization.Roles;
using Kaizen.Authorization.Users;

namespace Kaizen.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
