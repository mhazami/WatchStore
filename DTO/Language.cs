﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }
        public string LangId { get; set; }
    }
}