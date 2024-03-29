﻿using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Kaizen.Web.Views
{
    public abstract class KaizenRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected KaizenRazorPage()
        {
            LocalizationSourceName = KaizenConsts.LocalizationSourceName;
        }
    }
}
