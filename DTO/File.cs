using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class File
    {
        [Key]
        public Guid FileId { get; set; }
        public byte[] Context { get; set; }

        [Required]
        [MaxLength(10)]
        public string ContextType { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
    }
}
