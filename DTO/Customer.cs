using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public partial class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }


        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }


        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }


        [Required]
        [MaxLength(50)]
        public string PassWord { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
