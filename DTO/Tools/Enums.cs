using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class Enums
    {
        public enum VideoPosition : byte
        {
            [Description("")]
            None = 0,
            [Description("تاره ها")]
            NewSeason = 1,
            [Description("حراجی")]
            Deals = 2,
            [Description("لاکچری")]
            Luxtury = 3
        }
    }
}
