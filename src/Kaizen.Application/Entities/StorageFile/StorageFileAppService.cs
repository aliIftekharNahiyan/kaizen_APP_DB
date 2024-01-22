using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.StorageFiles.Dto;

namespace Kaizen.Entities.StorageFiles
            {
                public class StorageFileAppService : AsyncCrudAppService<StorageFile, StorageFileDto, long, PagedStorageFileResultRequestDto, CreateStorageFileDto, StorageFileDto>, IStorageFileAppService
                {
                    public StorageFileAppService(
                        IRepository<StorageFile, long> repository)
                        : base(repository)
                    {
                    }

                    protected override IQueryable<StorageFile> ApplySorting(IQueryable<StorageFile> query, PagedStorageFileResultRequestDto input)
                    {
                        if (!input.Sorting.IsNullOrWhiteSpace())
                        {
                            return query.OrderBy(input.Sorting);
                        }

                        return base.ApplySorting(query, input);
                    }

                    protected override IQueryable<StorageFile> CreateFilteredQuery(PagedStorageFileResultRequestDto input)
                    {
                        var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

                        return Repository.GetAllIncluding();
                    }
                }
            }