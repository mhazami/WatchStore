using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class Basket
    {
        [Key]
        public Guid BasketId { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

       

        [Required]
        public DateTime SaveDate { get; set; }

        [Required]
        public Int16 Status { get; set; }

        public bool? IsArchive { get; set; }
        
    }
}
