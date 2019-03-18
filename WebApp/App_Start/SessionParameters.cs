using ClockStore.DTO;
using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Collections;

namespace WebApp
{
    internal class SessionParameters
    {
        public static User User
        {
            get
            {
                if (HttpContext.Current.Session["User"] != null)
                    return (User)HttpContext.Current.Session["User"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["User"] = value;
            }
        }

        public static List<Basket> Basket
        {
            get
            {
                if (HttpContext.Current.Session["Basket"] != null)
                    return (List<Basket>)HttpContext.Current.Session["Basket"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["Basket"] = value;
            }
        }

        public static Customer Customer
        {
            get
            {
                if (HttpContext.Current.Session["Customer"] != null)
                    return (Customer)HttpContext.Current.Session["Customer"];
                return null;
            }
            set
            {
                HttpContext.Current.Session["Customer"] = value;
            }
        }

        public static void ClearBasket(Guid customerId)
        {

            var db = new ClockStoreContext();
            var list = db.Basket.ToList().Where(c => c.CustomerId == customerId && c.IsArchive == false);
            if (list != null)
            {
                foreach (var item in list)
                {

                    if (item.Product.IsBlocked.HasValue && item.Product.IsBlocked.Value)
                    {
                        item.Product.IsBlocked = false;
                        db.Entry(item.Product).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    db.Basket.Remove(item);
                    db.SaveChanges();
                }
            }

        }




    }
}