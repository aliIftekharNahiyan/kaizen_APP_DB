using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Kaizen.EntityFrameworkCore;
using Kaizen.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Kaizen.Web.Tests
{
    [DependsOn(
        typeof(KaizenWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class KaizenWebTestModule : AbpModule
    {
        public KaizenWebTestModule(KaizenEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KaizenWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(KaizenWebMvcModule).Assembly);
        }
    }
}