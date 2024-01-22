using System.Threading.Tasks;
using Kaizen.Configuration.Dto;

namespace Kaizen.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
