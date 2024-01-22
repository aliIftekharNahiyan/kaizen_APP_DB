
            using AutoMapper;
            using Kaizen.Entities.StorageFiles;
            using Kaizen.Entities.StorageFiles.Dto;

            namespace Kaizen.Users.Dto
            {
                public class StorageFileMapProfile : Profile
                {
                    public StorageFileMapProfile()
                    {
                        CreateMap<StorageFile, StorageFileDto>();
                        CreateMap<StorageFile, CreateStorageFileDto>();

                        CreateMap<CreateStorageFileDto, StorageFile>();
                    }
                }
            }