using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public partial class Customer
    {
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        public string TotalAmount
        {
            get
            {
                return TotalAmountdecimal.ToString("N0");
            }
        }

        public decimal TotalAmountdecimal
        {
            get
            {
                return new ClockStoreContext().Basket.ToList().Where(c => c.CustomerId == this.CustomerId).Sum(c => c.Product.PriceWithOff);
            }
        }
    }
}
