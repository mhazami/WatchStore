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
        public long NewsId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public Guid ImageId { get; set; }
        

        public Guid VideoId { get; set; }
        

        [Required]
        [MaxLength(5)]
        public string LangId { get; set; }

        [MaxLength(10)]
        public string SaveDate { get; set; }

    }
}
