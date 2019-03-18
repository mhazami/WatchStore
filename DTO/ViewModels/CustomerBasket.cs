using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO.ViewModels
{
    public class CustomerBasket
    {
        public Customer Customer { get; set; }

        public List<Basket> Baskets { get; set; }
    }
}
