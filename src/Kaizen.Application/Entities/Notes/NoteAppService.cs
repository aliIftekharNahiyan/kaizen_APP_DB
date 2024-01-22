using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq.Dynamic.Core;
using Kaizen.Entities.Notes.Dto;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Application.Services.Dto;
using Kaizen.Entities.SessionGroups.Dto;
using System.Collections.Generic;
using Kaizen.Authorization.Users;
using Kaizen.Users.Dto;

namespace Kaizen.Entities.Notes
{
    public class NoteAppService : AsyncCrudAppService<Note, NoteDto, long, PagedNoteResultRequestDto, CreateNoteDto, NoteDto>, INoteAppService
    {
        public NoteAppService(
            IRepository<Note, long> repository)
            : base(repository)
        {
        }

        public async Task<PagedResultDto<IGrouping<string, NoteDto>>> GetGroupedAsync(PagedNoteResultRequestDto input)
        {
            try
            {
                var query = Repository.GetAllIncluding(a => a.CreatedByUser).Where(a => a.EntityId == input.EntityId && a.EntityType == input.EntityType);

                var totalCount = await AsyncQueryableExecuter.CountAsync(query);

                query = ApplySorting(query, input);
                query = ApplyPaging(query, input);

                var notes = ObjectMapper.Map<List<NoteDto>>(query.ToList().Select(a => new NoteDto
                {
                    Content = a.Content,
                    CreatedBy = a.CreatedByUser.FullName,
                    IsImportant = a.IsImportant,
                    CreatedDate = a.CreatedDate
                }).ToList());


                IReadOnlyList<IGrouping<string, NoteDto>> notesGrouped = notes.GroupBy(a => a.Title).ToList();

                return new PagedResultDto<IGrouping<string, NoteDto>> { Items = notesGrouped, TotalCount = totalCount };
            }
            catch(Exception ex)
            {
                
            }

            return new PagedResultDto<IGrouping<string, NoteDto>> { TotalCount = 0 };
        }

        public override async Task<NoteDto> CreateAsync(CreateNoteDto input)
        {
            input.CreatedBy = AbpSession.UserId.Value;
            input.CreatedDate = DateTime.UtcNow;
            input.NoteType = input.IsInternal ? (int)NoteType.Internal : (int)NoteType.Public;

            return await base.CreateAsync(input);
        }


        protected override IQueryable<Note> ApplySorting(IQueryable<Note> query, PagedNoteResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }
            else
            {
                return query.OrderBy("CreatedBy asc");
            }
        }

        protected override IQueryable<Note> CreateFilteredQuery(PagedNoteResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return Repository.GetAllIncluding()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Content.ToLower().Contains(keywordLower));
        }
    }
}