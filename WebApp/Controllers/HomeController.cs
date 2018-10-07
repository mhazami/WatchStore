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
                ViewBag.Cart= db.Customer.ToList().Sum(c => c.TotalAmountdecimal).ToString("N0");
            }
            else
            {
                ViewBag.Cart = "Empty Cart";
            }
            
            return PartialView("PVTopHeader");
        }
    }
}