using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public DateTime SaveDate { get; set; }
        public bool IsFinaly { get; set; }
        public long RefID { get; set; }
        public decimal CurrentPrice { get; set; }

        public  int OfferCardId { get; set; }
    }
}
