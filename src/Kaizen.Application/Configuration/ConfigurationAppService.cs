using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Kaizen.Configuration.Dto;

namespace Kaizen.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : KaizenAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
