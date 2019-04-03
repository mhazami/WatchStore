using System;
using System.ComponentModel.DataAnnotations;

namespace ClockStore.DTO
{
    public class Advertise
    {
        [Key]
        public int Id { get; set; }

        public Guid FileId { get; set; }

        public File File { get; set; }

        public bool Enable { get; set; }
    }
}
