using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Kaizen.Controllers
{
    public abstract class KaizenControllerBase: AbpController
    {
        protected KaizenControllerBase()
        {
            LocalizationSourceName = KaizenConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
