using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class News
    {
        [Key]
        public Guid NewsId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public Guid ImageId { get; set; }
        public virtual File Image { get; set; }

        public Guid VideoId { get; set; }
        public virtual File Video { get; set; }

        [Required]
        [MaxLength(5)]
        public string LangId { get; set; }

    }
}
