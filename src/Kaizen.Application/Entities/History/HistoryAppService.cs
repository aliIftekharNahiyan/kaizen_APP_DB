using System;
using System.Linq;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Kaizen.Authorization;
using Kaizen.Entities.Addresses.Dto;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Kaizen.Entities.LinkTables;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Kaizen.EntityFrameworkCore;
using Kaizen.Entities.History.Dto;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kaizen.Entities.History
{
    /// <summary>
    /// History app service
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class HistoryAppService : IHistoryAppService
    {
        private readonly IDbContextProvider<KaizenDbContext> _context;
        /// <summary>
        /// History app service
        /// </summary>
        /// <param name="repository"></param>
        public HistoryAppService(
           IDbContextProvider<KaizenDbContext> context)
        {
            _context = context;
        }

        public PagedResultDto<HistoryChangesetDto> GetChangesetsForEntity(PagedHistoryResultRequestDto filter)
        {
            try
            {
                var results = string.Join("", _context.GetDbContext().Database.SqlQueryRaw<string>($"EXECUTE sp_History_GetChangesetsForEntity @EntityType='{filter.EntityType}', @EntityId='{filter.EntityId}'").AsNoTracking().ToList());

                var items = System.Text.Json.JsonSerializer.Deserialize<List<HistoryChangesetDto>>(results);

                return new PagedResultDto<HistoryChangesetDto>()
                {
                    Items = items,
                    TotalCount = items.Count
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PagedResultDto<HistoryDto> GetForChangeset(PagedHistoryResultRequestDto filter)
        {
            try
            {
                var results = string.Join("", _context.GetDbContext().Database.SqlQueryRaw<string>($"EXECUTE sp_History_GetForChangeset @ChangesetId={filter.ChangesetId}").AsNoTracking().ToList());

                var items = System.Text.Json.JsonSerializer.Deserialize<List<HistoryDto>>(results);

                return new PagedResultDto<HistoryDto>()
                {
                    Items = items,
                    TotalCount = items.Count
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

     
    }
}

