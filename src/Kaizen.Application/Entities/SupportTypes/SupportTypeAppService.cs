using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Kaizen.Authorization;
using Kaizen.Entities.Addresses.Dto;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Application.Services.Dto;
using Kaizen.Authorization.Roles;
using Kaizen.Authorization.Users;
using Kaizen.Users.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kaizen.Entities.LinkTables;
using Kaizen.Entities.Addresses;
using Kaizen.Entities.SupportTypes.Dto;

namespace Kaizen.Entities.SupportTypes
{
    /// <summary>
    /// Address app service
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class SupportTypeAppService : AsyncCrudAppService<SupportType, SupportTypeDto, long, PagedSupportTypeResultRequestDto, CreateSupportTypeDto, SupportTypeDto>, ISupportTypeAppService
    {
        /// <summary>
        /// Address app service
        /// </summary>
        /// <param name="repository"></param>
        public SupportTypeAppService(
            IRepository<SupportType, long> repository)
            : base(repository)
        {

        }


        protected override IQueryable<SupportType> ApplySorting(IQueryable<SupportType> query, PagedSupportTypeResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }
            else
            {
                return query.OrderBy("name asc");
            }
        }

        protected override IQueryable<SupportType> CreateFilteredQuery(PagedSupportTypeResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(keywordLower) || x.Description.ToLower().Contains(keywordLower) || x.Cost.ToString().Contains(keywordLower)
                || x.Margin.ToString().Contains(keywordLower));
                //.WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId)
        }


        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

