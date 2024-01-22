using Abp.Modules;
using Abp.Reflection.Extensions;
using Kaizen.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Kaizen.Web.Startup
{
    [DependsOn(typeof(KaizenWebCoreModule))]
    public class KaizenWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public KaizenWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<KaizenNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KaizenWebMvcModule).GetAssembly());
        }
    }
}
