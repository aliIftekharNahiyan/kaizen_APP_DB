
using AutoMapper;
using Kaizen.Entities.AcademicTerms;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.AcademicYears.Dto;
using Kaizen.Entities.InfoChecklists;
using Kaizen.Entities.InfoCheckLists.Dto;


namespace Kaizen.InfoCheckLists.Dto
{
    public class InfoCheckListMapProfile : Profile
    {
        public InfoCheckListMapProfile()
        {
            CreateMap<InfoCheckList, InfoCheckListDto>();
            CreateMap<InfoCheckList, CreateInfoCheckListDto>();

            CreateMap<CreateInfoCheckListDto, InfoCheckList>();
        }
    }
}