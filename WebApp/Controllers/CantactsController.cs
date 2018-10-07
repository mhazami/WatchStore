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
    public class CantactsController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: Cantacts
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            return View(db.Cantact.ToList());
        }

        // GET: Cantacts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cantact cantact = db.Cantact.Find(id);
            if (cantact == null)
            {
                return HttpNotFound();
            }
            return View(cantact);
        }

        // GET: Cantacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cantacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactId,FullName,Email,Phone,Message")] Cantact cantact)
        {
            if (ModelState.IsValid)
            {
                cantact.ContactId = Guid.NewGuid();
                db.Cantact.Add(cantact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cantact);
        }

        
     
        // GET: Cantacts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cantact cantact = db.Cantact.Find(id);
            if (cantact == null)
            {
                return HttpNotFound();
            }
            return View(cantact);
        }

        // POST: Cantacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Cantact cantact = db.Cantact.Find(id);
            db.Cantact.Remove(cantact);
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
