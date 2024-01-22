
using AutoMapper;
using Kaizen.Entities.AcademicYears;
using Kaizen.Entities.AcademicYears.Dto;

namespace Kaizen.AcademicYears.Dto
{
    public class AcademicYearMapProfile : Profile
    {
        public AcademicYearMapProfile()
        {
            CreateMap<AcademicYear, AcademicYearDto>();
            CreateMap<AcademicYear, CreateAcademicYearDto>();

            CreateMap<CreateAcademicYearDto, AcademicYear>();
        }
    }
}