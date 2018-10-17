using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClockStore.BLL;
using ClockStore.DTO;
using ClockStore.DTO.ViewModels;
using ClockStore.DTO.DBContext;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        ClockStoreContext db = new ClockStoreContext();
        // GET: Products
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            var product = db.Product.Where(c => c.Count > 0);
            return View(product.ToList());
        }

        [HttpPost]
        public ActionResult Index(string ProductName, string FromPrice, string ToPrice, string Code, string Count, string FromOff, string ToOff)
        {
            StringBuilder query = new StringBuilder();
            var list = new List<Product>();
            query.Append("select * from Products ");
            if (string.IsNullOrEmpty(ProductName)
                && string.IsNullOrEmpty(Code)
                && string.IsNullOrEmpty(FromOff)
                && string.IsNullOrEmpty(ToPrice)
                && string.IsNullOrEmpty(Count)
                && string.IsNullOrEmpty(ToOff)
                && string.IsNullOrEmpty(FromPrice))
            {
                list = db.Product.SqlQuery(query.ToString()).ToList();
                return View(list.ToList());
            }
            else
            {
                query.Append("Where ");
            }
            if (!string.IsNullOrEmpty(ProductName))
                query.Append($"ProductName LIKE '%{ProductName.Trim()}%' ");



            if (!string.IsNullOrEmpty(Code))
            {
                if (!string.IsNullOrEmpty(ProductName))
                    query.Append($"AND Code = {Code} ");
                else
                    query.Append($"Code = {Code} ");
            }



            if (!string.IsNullOrEmpty(Count))
            {
                if (!string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND Count = {int.Parse(Count)} ");
                else
                    query.Append($"Count = {int.Parse(Count)} ");
            }


            if (!string.IsNullOrEmpty(FromPrice))
            {
                if (!string.IsNullOrEmpty(Count) || !string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND Price > {decimal.Parse(FromPrice.Trim())} ");
                else
                    query.Append($"Price > {decimal.Parse(FromPrice.Trim())} ");
            }


            if (!string.IsNullOrEmpty(ToPrice))
            {
                if (!string.IsNullOrEmpty(FromPrice) || !string.IsNullOrEmpty(Count) || !string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND Price < {decimal.Parse(ToPrice)} ");

                else
                    query.Append($"Price < {decimal.Parse(ToPrice)} ");
            }

            if (!string.IsNullOrEmpty(FromOff))
            {
                if (!string.IsNullOrEmpty(ToPrice) || !string.IsNullOrEmpty(FromPrice) || !string.IsNullOrEmpty(Count) || !string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND [Off] > {int.Parse(FromOff)} ");

                else
                    query.Append($"[Off] > {int.Parse(FromOff)} ");
            }


            if (!string.IsNullOrEmpty(ToOff))
            {
                if (!string.IsNullOrEmpty(FromOff) || !string.IsNullOrEmpty(ToPrice) || !string.IsNullOrEmpty(FromPrice) || !string.IsNullOrEmpty(Count) || !string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND [Off] < {int.Parse(ToOff.Trim())}");

                else
                    query.Append($"[Off] < {int.Parse(ToOff.Trim())}");
            }



            list = db.Product.SqlQuery(query.ToString()).ToList();
            return View(list.ToList());
        }


        public ActionResult ProductListOfEmpty()
        {
            var list = db.Product.Where(c => c.Count <= 0);
            return View(list.ToList());
        }
        // GET: Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = new ProductBO().Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            ViewBag.CategoryId = new SelectList(new CategoryBO().GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,FileId,Price,Off,PriceWithOff,Description,Code,Count,LangId")] Product product, HttpPostedFileBase file)
        {

            product.ProductId = Guid.NewGuid();
            var image = new FileBO().Insert(file);
            product.FileId = image.FileId;
            product.File = image;
            //product.LangId = "en-US";
            db.Product.Add(product);
            if (db.SaveChanges() > 0)
                return RedirectToAction("Index");



            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryId = new SelectList(new CategoryBO().GetAll(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,FileId,Price,Off,PriceWithOff,Description,Code,Count,LangId")] Product product, HttpPostedFileBase file)
        {
            var fileid = db.Product.Find(product.ProductId).FileId;
            if (file != null)
            {

                new FileBO().UpDate(fileid, file);

            }

            product.FileId = fileid;
            product.LangId = "fa-IR";
            if (new ProductBO().Update(product))
                return RedirectToAction("Index");

            //ViewBag.CategoryId = new SelectList(new CategoryBO().GetAll(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = new ProductBO().Get(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();

            if (new FileBO().Delete(product.FileId))
                return RedirectToAction("Index");
            return View(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                new ProductBO().Dispose();
            }
            base.Dispose(disposing);
        }


        #region Method
        public ActionResult ProductsList()
        {
            var list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name);
            return PartialView("PVProductsList", list.ToList());
        }

        public ActionResult ProductDetails(Guid id)
        {
            var product = new ProductBO().Get(id);
            var extraimage = new ExtraImagesBO().GetAll().Where(c => c.ProductId == id).ToList();
            product.ExtraImages = extraimage;

            return PartialView("ProductDetails", product);
        }

        public ActionResult AddCart(Guid? id)
        {
            if (SessionParameters.Customer != null)
                return Redirect("/Baskets/AddToCard?id=" + id + "");
            return Redirect("/Customers/Signin");
        }

        public ActionResult Luxury()
        {
            var list = new VideoProduct()
            {
                ProductsList = db.Product.Where(c => c.Isluxury).ToList(),
                VideoList = db.VideoHandler.Where(c => c.Position == Enums.VideoPosition.Luxtury).ToList()
            };
            return View(list);
        }

        public ActionResult Deals()
        {
            var list = new VideoProduct()
            {
                ProductsList = db.Product.Where(c => c.IsDeals).ToList(),
                VideoList = db.VideoHandler.Where(c => c.Position == Enums.VideoPosition.Deals).ToList()
            };
            return View(list);
        }

        public ActionResult NewSeason()
        {
            var list = new VideoProduct()
            {
                ProductsList = db.Product.Where(c => c.IsNewSeason).ToList(),
                VideoList = db.VideoHandler.Where(c => c.Position == Enums.VideoPosition.NewSeason).ToList()
            };
            return View(list);
        }
        #endregion Method


    }
}
