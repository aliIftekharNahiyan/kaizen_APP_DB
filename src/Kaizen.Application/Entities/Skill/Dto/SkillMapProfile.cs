using AutoMapper;
using Kaizen.Entities.Regions.Dto;

namespace Kaizen.Entities.Skills.Dto
{
    public class SkillMapProfile : Profile
    {
        public SkillMapProfile()
        {
            CreateMap<SkillDto, Skill>().ReverseMap();

            CreateMap<Skill, CreateSkillDto>().ReverseMap();

        

        }
    }
}