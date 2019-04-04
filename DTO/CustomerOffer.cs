using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClockStore.DTO
{
    public class CustomerOffer
    {
        [Key, Column(Order = 0)]
        public Guid CustomerId { get; set; }

        [Key, Column(Order = 1)]
        public int OfferCardId { get; set; }
    }
}
