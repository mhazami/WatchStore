﻿using System;
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
    public class SlidersController : Controller
    {
        // GET: Sliders
        private ClockStoreContext db = new ClockStoreContext();
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            var slider = new SliderBO().GetAll();
            return View(slider);
        }

        // GET: Sliders/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = new SliderBO().Get(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Sliders/Create
        public ActionResult Create()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            return View();
        }

        // POST: Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SliderId,Title,Description,FileId,IsMainSlider,LangId")] Slider slider, HttpPostedFileBase file)
        {
           
                slider.SliderId = Guid.NewGuid();
                var image = new FileBO().Insert(file);
                slider.FileId = image.FileId;
                slider.File = image;
                if (new SliderBO().Insert(slider))
                    return RedirectToAction("Index");
            

            return View(slider);
        }

        // GET: Sliders/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = new SliderBO().Get(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SliderId,Title,Description,FileId,IsMainSlider,LangId")] Slider slider, HttpPostedFileBase file)
        {
            var fileid = db.Slider.Find(slider.SliderId).FileId;
            if (file != null)
            {

                new FileBO().UpDate(fileid, file);

            }

            slider.FileId = fileid;

            if (new SliderBO().Update(slider))
                return RedirectToAction("Index");

            //ViewBag.CategoryId = new SelectList(new CategoryBO().GetAll(), "CategoryId", "CategoryName", product.CategoryId);
            return View(slider);
        }

        // GET: Sliders/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = new SliderBO().Get(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var slider = new SliderBO().Get(id);
            if (new FileBO().Delete(slider.FileId))
                return RedirectToAction("Index");
            return View(slider);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                new SliderBO().Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Sliders()
        {
            var list = new SliderBO().GetAll();
            return PartialView("PVSliders", list);
        }
    }
}
