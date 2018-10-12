using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

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
        [MaxLength(5)]
        public string LangId { get; set; }
    }
}
