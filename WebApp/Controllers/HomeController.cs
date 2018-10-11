using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TopHeader()
        {
            if (SessionParameters.Customer!=null)
            {
                var list = db.Basket.Where(c => c.CustomerId == SessionParameters.Customer.CustomerId).ToList();
                ViewBag.Cart = list.Sum(c => c.Product.PriceWithOff).ToString("N0");
            }
            else
            {
                ViewBag.Cart = "Empty Cart";
            }
            
            return PartialView("PVTopHeader");
        }
    }
}