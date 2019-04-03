using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class Cantact
    {
        [Key]
        public Guid ContactId { get; set; }


        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }


        [Required]
        [MaxLength(150)]
        public string Email { get; set; }


        [MaxLength(11)]
        public string Phone { get; set; }


        [Required]
        public string Message { get; set; }

        [MaxLength(5)]
        public string LangId { get; set; }
    }
}
