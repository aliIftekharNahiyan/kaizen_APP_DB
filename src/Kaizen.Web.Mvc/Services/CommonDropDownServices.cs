using Kaizen.EntityFrameworkCore;
using Kaizen.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Kaizen.Web.Services
{
    /// <summary>
    /// Created: Md Maksudur Rahman 13-March-2015
    /// Description: Ge Select List from Enum
    /// </summary>
    /// 
    public class CommonDropDownServices : ICommonDropDownServices, IDisposable
    {
      
        private readonly IConfiguration _configuration;
        public KaizenDbContext _context { get; set; }

        public CommonDropDownServices( IConfiguration configuration,KaizenDbContext context)
        {
            _context = context;           
            _configuration = configuration;
        }


        public IEnumerable<CommonDropDownItem> Get(CommonEnum.DropDownList type, int? parentId = 0)
        {

            IEnumerable<CommonDropDownItem> query;
            parentId = parentId == 0 ? null : parentId;

            switch (type)
            {
                case CommonEnum.DropDownList.University:

                    query = from x in _context.University.AsNoTracking().Where(u => u.Deleted == false).OrderBy(o => o.Name).AsQueryable()
                            select new CommonDropDownItem { Id = x.Id, Name = x.Name };
                    return query;

                case CommonEnum.DropDownList.Skills:

                    query = from x in _context.Skill.AsNoTracking().Where(p => p.IsDeleted == false && p.IsActive == true && (!parentId.HasValue || p.Id == parentId)).OrderBy(o => o.Name).AsQueryable()
                            select new CommonDropDownItem { Id = x.Id, Name = x.Name };
                    return query;                

                default:
                    query = new List<CommonDropDownItem>();
                    return query;
            }
        }


        public List<CommonDropDownItem> GetDropdownItems(CommonEnum.DropDownList type, string selectText, int? parentId)
        {
            List<CommonDropDownItem> items = new List<CommonDropDownItem>();
            items.Add(new CommonDropDownItem { Id = 0, Name = selectText.Trim() == "" ? "-- Select --" : selectText });
            items.AddRange(Get(type, parentId));
            items = items.OrderBy(o => o.Name).ToList();
            //HttpEncodeData.ParseProperties(items);
            //return items;
            return items.OrderBy(o => o.Name).ToList();
        }
        /// <summary>
        /// Created: Md Maksudur Rahman 13-March-2015
        /// Description: Ge Select List from Enum
        /// </summary>
        /// 
        public static IEnumerable<CommonDropDownItem> ToSelectList<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumerationType = typeof(TEnum);
            var items = new List<CommonDropDownItem>();

            foreach (int value in Enum.GetValues(enumerationType))
            {
                var name = Enum.GetName(enumerationType, value);

                var description = GetDisplayValue<TEnum>(name);
                CommonDropDownItem item = new CommonDropDownItem();
                item.Id = value;
                item.Name = string.IsNullOrWhiteSpace(description) ? name : description;
                items.Add(item);
            }

            return items;
        }
 
        private static string GetDisplayValue<TEnum>(string item)
        {
            var enumerationType = typeof(TEnum);
            var value = (TEnum)Enum.Parse(enumerationType, item, true);

            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            string desc = string.Empty;
            if (descriptionAttributes != null && descriptionAttributes.Length > 0) desc = descriptionAttributes[0].Description;
            else desc = fieldInfo.Name;

            return desc;
        }

        private bool disposed = false;

        /// <summary>
        /// Object disposing
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
