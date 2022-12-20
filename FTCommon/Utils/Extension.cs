using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTCommon.Utils
{
    public static class Extension
    {
        public static IList<SelectListItem> SelectListFor(this Type enumType)
        {
            if (enumType.IsEnum)
            {
                return System.Enum.GetValues(enumType)
                    .Cast<int>()
                    .Select(e => new SelectListItem()
                    {
                        Value = e.ToString(),
                        Text = System.Enum.GetName(enumType, e)
                    })
                    .ToList();
            }
            return null;
        }
    }
}
