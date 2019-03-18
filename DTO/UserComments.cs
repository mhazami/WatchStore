using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
   public class UserComments
    {
        [Key]
        public Int64 UserCommentsId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(400)]
        public string Title { get; set; }

        [Required]
        [MaxLength(4000)]
        public String Message { get; set; }

        public Guid ProductId { get; set; }

        public bool? IsAccept { get; set; }

    }
}
