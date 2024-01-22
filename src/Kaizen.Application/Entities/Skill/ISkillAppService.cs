using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Kaizen.Entities.Notes.Dto;
using System.Linq;
using System.Threading.Tasks;
using System;
using Kaizen.Entities.Skills.Dto;
using Kaizen.Roles.Dto;
using System.Collections.Generic;

namespace Kaizen.Entities.Skills
{
    public interface ISkillAppService : IAsyncCrudAppService<SkillDto, long, PagedSkillRequestDto, CreateSkillDto, SkillDto>
    {
    }
}