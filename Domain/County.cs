using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum County
    {
        Unknown = 0,
        Antrim,
        Armagh,
        Carlow,
        Cavan,
        Clare,
        Cork,
        Derry,
        Donegal,
        Down,
        Dublin,
        Fermanagh,
        Galway,
        Kerry,
        Kildare,
        Kilkenny,
        Laois,
        Leitrim,
        Limerick,
        Longford,
        Louth,
        Mayo,
        Meath,
        Monaghan,
        Offaly,
        Roscommon,
        Sligo,
        Tipperary,
        Tyrone,
        Waterford,
        Westmeath,
        Wexford,
        Wicklow
    }

    public static class EnumHelpers
    {
        public static County CountyFromString(string name)
        {
            County result;
            if (!Enum.TryParse<County>(name, true, out result))
            {
                result = County.Unknown;
            }
            return result;
        }
    }
}
