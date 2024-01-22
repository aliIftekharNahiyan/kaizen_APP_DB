
using Kaizen.Enums;
using System.Collections.Generic;

namespace Kaizen.Web.Services
{
    public interface ICommonDropDownServices
    {
        IEnumerable<CommonDropDownItem> Get(CommonEnum.DropDownList type, int? parentId = 0);
        List<CommonDropDownItem> GetDropdownItems(CommonEnum.DropDownList type, string selectText, int? parentId);
    }
}
