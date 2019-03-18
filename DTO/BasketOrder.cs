using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class BasketOrder
    {
        [Key, Column(Order = 0)]
        public Guid OrderId { get; set; }

        [Key, Column(Order = 1)]
        public Guid BasketId { get; set; }
    }
}
