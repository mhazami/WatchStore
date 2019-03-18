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
    public class AboutUsController : Controller
    {
        // GET: AboutUs
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            var list = new AboutUsBO().GetAll();
            return list.Any() ? View(list) : View("Create");
        }

        // GET: AboutUs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = new AboutUsBO().Get(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // GET: AboutUs/Create
        public ActionResult Create()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            return View();
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AboutUsId,Description")] AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                aboutUs.AboutUsId = Guid.NewGuid();
                new AboutUsBO().Insert(aboutUs);
                return RedirectToAction("Index");
            }

            return View(aboutUs);
        }

        // GET: AboutUs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = new AboutUsBO().Get(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AboutUsId,Description")] AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                new AboutUsBO().Update(aboutUs);
                return RedirectToAction("Index");
            }
            return View(aboutUs);
        }

        // GET: AboutUs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = new AboutUsBO().Get(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            new AboutUsBO().Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                new AboutUsBO().Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AboutUS()
        {
            return View();
        }
    }
}
