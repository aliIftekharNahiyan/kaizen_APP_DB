
using AutoMapper;
using Kaizen.Entities.BulkProcess;

namespace Kaizen.Entities.BulkProcessJobs.Dto
{
    public class BulkProcessJobMapProfile : Profile
    {
        public BulkProcessJobMapProfile()
        {
            CreateMap<BulkProcessJob, BulkProcessJobDto>();
            CreateMap<BulkProcessJob, CreateBulkProcessJobDto>().ReverseMap();
        }
    }
}