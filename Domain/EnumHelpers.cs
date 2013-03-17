using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class EnumHelpers
    {
        public static T FromString<T>(string name) where T : struct
        {
            T result;
            if (!Enum.TryParse<T>(name, true, out result))
            {
                result = default(T);
            }
            return result;
        }
    }
}
