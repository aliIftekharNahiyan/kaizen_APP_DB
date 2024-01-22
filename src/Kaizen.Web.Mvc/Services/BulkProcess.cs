namespace Kaizen.Web.Services
{
    using Abp.Application.Services.Dto;
    using Abp.Domain.Entities;
    using Abp.Domain.Repositories;
    using Abp.EntityFrameworkCore.Repositories;
    using AutoMapper.Internal.Mappers;
    using Kaizen.Entities.Companies;
    using Microsoft.AspNetCore.Razor.Language.Intermediate;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;



    public class BulkProcess<IE,T > where T : EntityDto<long> where IE : class, IEntity<long> 
    {
        

        private IRepository<IE, long> _repository { get; }

        public BulkProcess(IRepository<IE, long> repository)
        {
           
            _repository = repository;
        }
        public async Task Create(List<T> items)
        {

            await _repository.InsertRangeAsync((IEnumerable<IE>)items);

        }
    }
}
