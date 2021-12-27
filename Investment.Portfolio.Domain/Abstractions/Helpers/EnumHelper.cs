using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Investment.Portfolio.Domain.Abstractions.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescriptionName(this Enum val)
            => val.GetType()
                  .GetMember(val.ToString())
                  .FirstOrDefault()
                  ?.GetCustomAttribute<DisplayAttribute>(false)
                  ?.Name
                  ?? val.ToString();
        
    }
}
