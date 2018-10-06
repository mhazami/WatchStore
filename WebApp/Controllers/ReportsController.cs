using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ReportsController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();
        // GET: Reports
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            var customers = db.Customer.ToList();
            return View(customers);
        }

        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            return Redirect("/Customers/Details?id=" + id + "");
        }

        public ActionResult ReportAll()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            var customers = db.Customer.ToList();
            ViewBag.TotalAmount = db.Customer.ToList().Sum(c => c.TotalAmountdecimal).ToString("N0");
            return View(customers);
        }
    }
}