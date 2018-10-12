using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
  public  class AboutUs
    {
        [Key]
        public Guid AboutUsId { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(5)]
        public string LangId { get; set; }
    }
}
