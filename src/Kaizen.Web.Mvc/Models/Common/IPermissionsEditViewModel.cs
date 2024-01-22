using Kaizen.Roles.Dto;
using System.Collections.Generic;

namespace Kaizen.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}