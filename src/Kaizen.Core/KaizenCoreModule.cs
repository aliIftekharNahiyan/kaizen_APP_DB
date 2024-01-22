using Abp.Localization;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Security;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Kaizen.Authorization.Roles;
using Kaizen.Authorization.Users;
using Kaizen.Configuration;
using Kaizen.Localization;
using Kaizen.MultiTenancy;
using Kaizen.Timing;

namespace Kaizen
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class KaizenCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            KaizenLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = KaizenConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
            
            Configuration.Localization.Languages.Add(new LanguageInfo("fa", "فارسی", "famfamfam-flags ir"));
            
            Configuration.Settings.SettingEncryptionConfiguration.DefaultPassPhrase = KaizenConsts.DefaultPassPhrase;
            SimpleStringCipher.DefaultPassPhrase = KaizenConsts.DefaultPassPhrase;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(KaizenCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
