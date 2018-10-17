using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (SessionParameters.Customer != null)
            {
                var list = db.Basket.Where(c => c.CustomerId == SessionParameters.Customer.CustomerId).ToList();
                ViewBag.Cart = list.Sum(c => c.Product.PriceWithOff).ToString("N0");
            }
            else
            {
                ViewBag.Cart = Resources.Design.Design.EmptyCart;
            }

            return PartialView("PVTopHeader");
        }

        public ActionResult TestVideo(Guid id)
        {
            var model = db.Product.Find(id);
            return View(model);
        }

        public FileResult DownloadFile(Guid? fileId)
        {
            var file = db.File.FirstOrDefault(x => x.FileId == fileId);
            return File(file.Context, file.ContextType, file.Title);
        }

        public ActionResult BrandsSlider()
        {
            var list = db.Category.Where(c => c.LangId == CultureInfo.CurrentCulture.Name).ToList();
            return PartialView("PVBrandsSlider", list);

        }


    }
}