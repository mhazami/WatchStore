using ClockStore.BLL.Base;
using ClockStore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockStore.BLL
{
    public class BasketBO : BaseBO<Basket>
    {
        public override bool Insert(Basket obj)
        {
            obj.IsArchive = false;
            return base.Insert(obj);
        }
    }
}
