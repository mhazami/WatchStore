using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class Color
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Value{ get; set; }

        public Guid ProductId { get; set; }

        public bool? Enable { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
