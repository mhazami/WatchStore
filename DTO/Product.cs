using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public partial class Product
    {
        [Key]
        public Guid ProductId { get; set; }


        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }


        [Required]
        public Guid FileId { get; set; }
        public virtual File File { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Off { get; set; }


        public int Count { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public bool IsDeals { get; set; }

        public bool IsNewSeason { get; set; }

    }
}
