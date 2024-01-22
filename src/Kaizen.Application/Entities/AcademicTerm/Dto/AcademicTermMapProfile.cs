
using AutoMapper;
using Kaizen.Entities.AcademicTerms;
using Kaizen.Entities.AcademicTerms.Dto;
using Kaizen.Entities.AcademicYears.Dto;


namespace Kaizen.AcademicTerms.Dto
{
    public class AcademicTermMapProfile : Profile
    {
        public AcademicTermMapProfile()
        {
            CreateMap<AcademicTerm, AcademicTermDto>();
            CreateMap<AcademicTerm, CreateAcademicTermDto>();

            CreateMap<CreateAcademicYearDto, AcademicTerm>();
        }
    }
}