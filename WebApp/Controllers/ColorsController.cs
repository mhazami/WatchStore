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
    public class ColorsController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: Colors
        public ActionResult Index()
        {
            return View(db.Color.ToList());
        }

        // GET: Colors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // GET: Colors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Value,ProductId,Enable,Name")] Color color)
        {
            if (ModelState.IsValid)
            {
                color.Id = Guid.NewGuid();
                db.Color.Add(color);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(color);
        }

        // GET: Colors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value,ProductId,Enable,Name")] Color color)
        {
            if (ModelState.IsValid)
            {
                db.Entry(color).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(color);
        }

        // GET: Colors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color color = db.Color.Find(id);
            if (color == null)
            {
                return HttpNotFound();
            }
            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Color color = db.Color.Find(id);
            db.Color.Remove(color);
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
