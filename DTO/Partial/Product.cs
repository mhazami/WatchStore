using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.DTO
{
    public partial class Product
    {
        public List<ExtraImages> ExtraImages { get; set; }
        public decimal PriceWithOff
        {
            get
            {
                if (this.Off > 0)
                    return this.Price - (this.Price * Off.Value / 100);
                return this.Price;
            }
        }

        public string PriceWithOffForFacror
        {
            get
            {
                if (this.PriceWithOff != 0)
                    return this.PriceWithOff.ToString("N0");
                return "0";
            }
        }
    }
}
