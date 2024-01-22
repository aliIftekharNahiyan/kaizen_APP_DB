
            using Abp.Application.Services;
            using Kaizen.Entities.StorageFiles.Dto;

            namespace Kaizen.Entities.StorageFiles
            {
                public interface IStorageFileAppService : IAsyncCrudAppService<StorageFileDto, long, PagedStorageFileResultRequestDto, CreateStorageFileDto, StorageFileDto>
                {

                }
            }