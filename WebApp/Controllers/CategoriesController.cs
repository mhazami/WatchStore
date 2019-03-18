using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClockStore.BLL;
using ClockStore.DTO;
using ClockStore.DTO.DBContext;

namespace WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        ClockStoreContext db = new ClockStoreContext();
        // GET: Categories
        public ActionResult Index()
        {
            var list = new CategoryBO().GetAll();
            return View(list);
        }

        // GET: Categories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = new CategoryBO().Get(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName,LangId")] Category category, HttpPostedFileBase image)
        {
            category.CategoryId = Guid.NewGuid();
            var file = new File();
            file.FileId = Guid.NewGuid();
            if (image != null)
            {
                file.Context = new byte[image.ContentLength];
                image.InputStream.Read(file.Context, 0, image.ContentLength);
                file.ContextType = image.ContentType;
                file.Title = image.FileName;
            }

            db.File.Add(file);


            category.FileId = file.FileId;
            db.Category.Add(category);
            if (db.SaveChanges() > 0)
                return RedirectToAction("Index");



            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = new CategoryBO().Get(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,LangId")] Category category, HttpPostedFileBase image)
        {
            var fileid = db.Category.Find(category.CategoryId).FileId;
            if (image != null)
            {

                new FileBO().UpDate(fileid, image);

            }

            category.FileId = fileid;

            if (new CategoryBO().Update(category))
                return RedirectToAction("Index");

            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = new CategoryBO().Get(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var category = db.Category.Find(id);
            db.Category.Remove(category);
            db.SaveChanges();

            if (new FileBO().Delete(category.FileId))
                return RedirectToAction("Index");
            return View(category);
        }


        public ActionResult ProductsList(Guid id)
        {
            var list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name && c.CategoryId == id).ToList();
            return View(list);
        }

        public ActionResult Brands()
        {
            var list = db.Category.Where(c => c.LangId == CultureInfo.CurrentCulture.Name).ToList();
            return View(list);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                new CategoryBO().Dispose();

            }
            base.Dispose(disposing);
        }
    }
}
