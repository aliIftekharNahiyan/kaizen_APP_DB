using System;
using System.Linq;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Kaizen.Authorization;
using Kaizen.Entities.Addresses.Dto;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Kaizen.Entities.LinkTables;

namespace Kaizen.Entities.Addresses
{
    /// <summary>
    /// Address app service
    /// </summary>
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class AddressAppService : AsyncCrudAppService<Address, AddressDto, long, PagedAddressResultRequestDto, CreateAddressDto, AddressDto>, IAddressAppService
    {
        private readonly IRepository<UserAddress, long> _userAddressRepo;

        /// <summary>
        /// Address app service
        /// </summary>
        /// <param name="repository"></param>
        public AddressAppService(
            IRepository<Address, long> repository,
            IRepository<UserAddress, long> userAddressRepo)
            : base(repository)
        {
            _userAddressRepo = userAddressRepo;
        }

        public override async Task<AddressDto> CreateAsync(CreateAddressDto input)
        {
            // We have to create the UserAddress too.
            var address = await base.CreateAsync(input);
            
            _userAddressRepo.Insert(new UserAddress
            {
                UserId = input.UserId,
                AddressId = address.Id
            });

            CurrentUnitOfWork.SaveChanges();

            return address;
        }

        protected override IQueryable<Address> ApplySorting(IQueryable<Address> query, PagedAddressResultRequestDto input)
        {
            if (!input.Sorting.IsNullOrWhiteSpace())
            {
                return query.OrderBy(input.Sorting);
            }

            return base.ApplySorting(query, input);
        }

        protected override IQueryable<Address> CreateFilteredQuery(PagedAddressResultRequestDto input)
        {
            var keywordLower = input.Keyword?.ToLower() ?? string.Empty;

            return _userAddressRepo.GetAllIncluding(a => a.Address)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Address.Name.ToLower().Contains(keywordLower) || x.Address.Line1.ToLower().Contains(keywordLower) || x.Address.Line2.ToLower().Contains(keywordLower)
                || x.Address.Line3.ToLower().Contains(keywordLower)
                || x.Address.City.ToLower().Contains(keywordLower)
                || x.Address.County.ToLower().Contains(keywordLower)
                || x.Address.Postcode.ToLower().Contains(keywordLower))
                .WhereIf(input.IsPrimary.HasValue, x => x.Address.IsPrimary == input.IsPrimary)
                .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId)
                .Select(a => a.Address);
        }


        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

