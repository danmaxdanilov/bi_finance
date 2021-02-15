using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;
using System.Reflection;

namespace SL.Models
{
    public static class EnumTypes
    {
        public enum ImportFileType
        {
            [Display(Name = "Employee")]
            Employee = 0,
            [Display(Name = "Distribution")]
            Distribution = 1,
            [Display(Name = "Salary")]
            Salary = 2,
            [Display(Name = "Advance")]
            Advance = 3,
            [Display(Name = "Production")]
            Production = 4,
            [Display(Name = "Revenue")]
            Revenue = 5,
            [Display(Name = "Income")]
            Income = 6
        }

        public enum CostCenterType
        {
            [Display(Name = "profitCenter")]
            ProfitCenter = 0,
            [Display(Name = "lawyer")]
            Lawyer = 1,
            [Display(Name = "indirectionDepartment")]
            IndirectionDepartment = 2,
            [Display(Name = "office")]
            Office = 3,
            [Display(Name = "payment")]
            Payment = 4,
            [Display(Name = "backOffice")]
            BackOffice = 5,
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttr = enumValue.GetAttribute<DisplayAttribute>();
            return displayAttr.Name;
        }

        public static string GetDescription(this Enum enumValue)
        {
            try
            {
                var displayAttr = enumValue.GetAttribute<DisplayAttribute>();
                return displayAttr.Description;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }

        private static TEnum? GetEnumValue<TEnum>(int enumVal) where TEnum : struct, IConvertible
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new Exception("T must be an Enumeration type.");

            if (Enum.IsDefined(typeof(TEnum), enumVal)) return (TEnum)(object)enumVal;
            return null;
        }
    }
}
