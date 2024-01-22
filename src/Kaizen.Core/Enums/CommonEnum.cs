using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Enums
{
    public static class CommonEnum
    {
        public enum LookupType
        {
            [Description("User Type")]
            UserType = 1,
            [Description("Customer Type")]
            CustomerType = 2,
            [Description("Employee Type")]
            EmployeeType = 3,
            [Description("Contact Type")]
            ContactType = 4,
            [Description("Relation Type")]
            RelationType = 5,
            [Description("Visibility Permission")]
            VisibilityPermission = 6,
            [Description("Regular Client Support")]
            RegularClientSupport = 7,
            [Description("Pronouns")]
            Pronouns = 8,
        }

        public enum DropDownList
        {
            University = 1,
            Skills = 2,

        }


        }
        public static class EnumHelper
    {
        public static IEnumerable<SelectListItem> GetEnumSelectListWithDescription<TEnum>(string defaultText = " Select ", string defaultValue = "0")
        {
            var enumValues = Enum.GetValues(typeof(TEnum))
                                 .Cast<TEnum>()
                                 .Select(e => new SelectListItem
                                 {
                                     Value = Convert.ToInt32(e).ToString(),
                                     Text = GetEnumDescription(e)
                                 });

            var selectList = new List<SelectListItem>();

            // Add the default SelectListItem if provided
            if (!string.IsNullOrEmpty(defaultText))
            {
                selectList.Add(new SelectListItem { Text = defaultText, Value = defaultValue });
            }

            // Add the enum values to the list
            selectList.AddRange(enumValues);

            return new SelectList(selectList, "Value", "Text");


        }

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) return value.ToString();

            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }

}
