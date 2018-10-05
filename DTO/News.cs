﻿using System;
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

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }


        [Required]
        public Int16 Position { get; set; }

        [Required]
        public Guid FileId { get; set; }
        public virtual File File { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
