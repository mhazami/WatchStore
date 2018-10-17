using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClockStore.DTO.Enums;

namespace ClockStore.DTO
{
    public class VideoHandler
    {
        [Key]
        public Guid VideoId { get; set; }

        public VideoPosition Position { get; set; }

        public string Title { get; set; }

    }
}
