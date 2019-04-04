using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClockStore.DTO;
using ClockStore.DTO.DBContext;

namespace WebApp.Controllers
{
    public class AdvertisesController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: Advertises
        public ActionResult Index()
        {
            var advertises = db.Advertises.Include(a => a.File);
            return View(advertises.ToList());
        }

        // GET: Advertises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertise advertise = db.Advertises.Find(id);
            if (advertise == null)
            {
                return HttpNotFound();
            }
            return View(advertise);
        }

        // GET: Advertises/Create
        public ActionResult Create()
        {
            ViewBag.FileId = new SelectList(db.File, "FileId", "ContextType");
            return View();
        }

        // POST: Advertises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Advertise advertise,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Advertises.Add(advertise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FileId = new SelectList(db.File, "FileId", "ContextType", advertise.FileId);
            return View(advertise);
        }

        // GET: Advertises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertise advertise = db.Advertises.Find(id);
            if (advertise == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileId = new SelectList(db.File, "FileId", "ContextType", advertise.FileId);
            return View(advertise);
        }

        // POST: Advertises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Advertise advertise, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advertise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FileId = new SelectList(db.File, "FileId", "ContextType", advertise.FileId);
            return View(advertise);
        }

        // GET: Advertises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertise advertise = db.Advertises.Find(id);
            if (advertise == null)
            {
                return HttpNotFound();
            }
            return View(advertise);
        }

        // POST: Advertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertise advertise = db.Advertises.Find(id);
            db.Advertises.Remove(advertise);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
