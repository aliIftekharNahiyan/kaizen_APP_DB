using Abp.AspNetCore.Mvc.ViewComponents;

namespace Kaizen.Web.Views
{
    public abstract class KaizenViewComponent : AbpViewComponent
    {
        protected KaizenViewComponent()
        {
            LocalizationSourceName = KaizenConsts.LocalizationSourceName;
        }
    }
}
