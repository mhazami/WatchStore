using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class ExtraImages
    {
        [Key]
        public Guid ExtraImagesId { get; set; }

        [Required]
        public Guid FileId { get; set; }
        

        [Required]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        [MaxLength(5)]
        public string LangId { get; set; }

    }
}
