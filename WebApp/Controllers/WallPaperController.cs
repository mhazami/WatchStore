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
    public class WallPaperController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: WallPaper
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            return View(db.WallPaper.ToList());
        }

        // GET: WallPaper/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WallPaper WallPaper = db.WallPaper.Find(id);
            if (WallPaper == null)
            {
                return HttpNotFound();
            }
            return View(WallPaper);
        }

        // GET: WallPaper/Create
        public ActionResult Create()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            return View();
        }

        // POST: WallPaper/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WallPaperId,Title,Position,Description")] WallPaper WallPaper, HttpPostedFileBase file)
        {
            WallPaper.WallPaperId = Guid.NewGuid();
            var image = new FileBO().Insert(file);
            WallPaper.FileId = image.FileId;
            WallPaper.File = image;
            db.WallPaper.Add(WallPaper);
            if (db.SaveChanges() > 0)
                return RedirectToAction("Index");



            return View(WallPaper);
        }

        // GET: WallPaper/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WallPaper WallPaper = db.WallPaper.Find(id);
            if (WallPaper == null)
            {
                return HttpNotFound();
            }
            return View(WallPaper);
        }

        // POST: WallPaper/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WallPaperId,Title,Position,Description")] WallPaper WallPaper, HttpPostedFileBase file)
        {
            var fileid = db.Product.Find(WallPaper.WallPaperId).FileId;
            if (file != null)
            {

                new FileBO().UpDate(fileid, file);

            }

            WallPaper.FileId = fileid;

            if (new WallPaperBO().Update(WallPaper))
                return RedirectToAction("Index");

            return View(WallPaper);
        }

        // GET: WallPaper/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WallPaper WallPaper = db.WallPaper.Find(id);
            if (WallPaper == null)
            {
                return HttpNotFound();
            }
            return View(WallPaper);
        }

        // POST: WallPaper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var WallPaper = db.WallPaper.Find(id);
            db.WallPaper.Remove(WallPaper);
            db.SaveChanges();

            if (new FileBO().Delete(WallPaper.FileId))
                return RedirectToAction("Index");
            return View(WallPaper);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult WallPaper()
        {
            var count = db.WallPaper.Count();
            if (count <= 3)
            {
                var list = db.WallPaper.ToList();
                return PartialView("PVWallPaper", list);
            }
            else
            {
                var res =new List<WallPaper>();
                var list = db.WallPaper.ToList().OrderBy(c => c.Position);
                for (int i = 0; i < 3; i++)
                {
                    res.Add(list.ToList()[i]);
                }
                return PartialView("PVWallPaper", res);
            }
        }
    }
}
