using ClockStore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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


    }
}