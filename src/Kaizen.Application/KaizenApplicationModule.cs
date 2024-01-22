using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Kaizen.Authorization;

namespace Kaizen
{
    [DependsOn(
        typeof(KaizenCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class KaizenApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<KaizenAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(KaizenApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
