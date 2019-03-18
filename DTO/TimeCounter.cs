using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class TimeCounter
    {
        [Key]
        public Guid TimeCounterId { get; set; }
        public string TimeRange { get; set; }
    }
}
