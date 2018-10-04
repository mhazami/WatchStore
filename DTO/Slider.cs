using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class Slider
    {
        [Key]
        public Guid SliderId { get; set; }


        [Required]
        [MaxLength(50)]
        public string Title { get; set; }


        public string Description { get; set; }

        [Required]
        public Guid FileId { get; set; }
        public virtual File File { get; set; }

        public bool IsMainSlider { get; set; }
    }
}
