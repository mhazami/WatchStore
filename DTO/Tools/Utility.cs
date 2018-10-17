using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public static class Utility
    {
        public static string CurrentDate(this DateTime date, string langId)
        {
            PersianCalendar pc = new PersianCalendar();
            if (langId == "fa-IR")
            {
                var day = pc.GetDayOfMonth(date);
                var month = pc.GetMonth(date);
                var year = pc.GetYear(date);
                return $"{year}/{month}/{day}";
            }
            var dt = date.GetDateTimeFormats()[3];
            return $"{dt.Substring(6, 4)}/{dt.Substring(0,2)}/{dt.Substring(3, 2)}";           
        }

       
    }
}
