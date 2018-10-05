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
    public class ExtraImagesController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: ExtraImages
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            var extraImages = db.ExtraImages.Include(e => e.Product);
            return View(extraImages.ToList());
        }

        // GET: ExtraImages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraImages extraImages = db.ExtraImages.Find(id);
            if (extraImages == null)
            {
                return HttpNotFound();
            }
            return View(extraImages);
        }

        // GET: ExtraImages/Create
        public ActionResult Create()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName");
            return View();
        }

        // POST: ExtraImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExtraImagesId,FileId,ProductId")] ExtraImages extraImages)
        {
            if (ModelState.IsValid)
            {
                extraImages.ExtraImagesId = Guid.NewGuid();
                db.ExtraImages.Add(extraImages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", extraImages.ProductId);
            return View(extraImages);
        }

        // GET: ExtraImages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraImages extraImages = db.ExtraImages.Find(id);
            if (extraImages == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", extraImages.ProductId);
            return View(extraImages);
        }

        // POST: ExtraImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExtraImagesId,FileId,ProductId")] ExtraImages extraImages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", extraImages.ProductId);
            return View(extraImages);
        }

        // GET: ExtraImages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraImages extraImages = db.ExtraImages.Find(id);
            if (extraImages == null)
            {
                return HttpNotFound();
            }
            return View(extraImages);
        }

        // POST: ExtraImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {

            var extra = db.ExtraImages.Find(id);
            
            if (new FileBO().Delete(extra.FileId))
            {
                db.ExtraImages.Remove(extra);
                db.SaveChanges();
                return Redirect("/ExtraImages/ProductImages?id=" + extra.ProductId + "");
            }
                
            return View(extra);
            
      
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ProductImages(Guid id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            var list = new ExtraImagesBO().GetAll().Where(c => c.ProductId == id);
            ViewBag.ProductId = id;
            return View(list);
        }

        [HttpPost]
        public ActionResult ProductImages(Guid id, HttpPostedFileBase file)
        {
            var image = new File();
            image.FileId = Guid.NewGuid();
            if (file != null)
            {
                image.Context = new byte[file.ContentLength];
                file.InputStream.Read(image.Context, 0, file.ContentLength);
                image.ContextType = file.ContentType;
                image.Title = file.FileName;
            }
            
            db.File.Add(image);

            var extera = new ExtraImages()
            {
                ProductId = id,
                FileId = image.FileId,
                ExtraImagesId = Guid.NewGuid()
            };
            db.ExtraImages.Add(extera);
            db.SaveChanges();
            var list = new ExtraImagesBO().GetAll().Where(c => c.ProductId == id);
            ViewBag.ProductId = id;
            return View(list);
        }
    }
}
