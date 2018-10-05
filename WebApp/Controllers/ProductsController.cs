using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClockStore.BLL;
using ClockStore.DTO;
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
            var product = new ProductBO().GetAll();
            return View(product);
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
        public ActionResult Create([Bind(Include = "ProductId,ProductName,FileId,Price,Off,PriceWithOff,Description,Code")] Product product, HttpPostedFileBase file)
        {

            product.ProductId = Guid.NewGuid();
            var image = new FileBO().Insert(file);
            product.FileId = image.FileId;
            product.File = image;
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
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,FileId,Price,Off,PriceWithOff,Description,Code")] Product product, HttpPostedFileBase file)
        {
            var fileid = db.Product.Find(product.ProductId).FileId;
            if (file != null)
            {

                new FileBO().UpDate(fileid, file);

            }

            product.FileId = fileid;

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
            var list = new ProductBO().GetAll();
            return PartialView("PVProductsList", list);
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
        #endregion Method
    }
}
