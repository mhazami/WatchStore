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

      
    }
}
