﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClockStore.BLL;
using ClockStore.DTO;
using ClockStore.DTO.DBContext;


namespace WebApp.Controllers
{
    public class BasketsController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: Baskets
        public ActionResult Index()
        {
            var basket = db.Basket.Include(b => b.Customer).Include(b => b.Product);
            return View(basket.ToList());
        }

        // GET: Baskets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = db.Basket.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // GET: Baskets/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "FirstName");
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName");
            return View();
        }

        // POST: Baskets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BasketId,ProductId,CustomerId,Quantity,SaveDate,Status")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                basket.BasketId = Guid.NewGuid();
                db.Basket.Add(basket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "FirstName", basket.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", basket.ProductId);
            return View(basket);
        }

        // GET: Baskets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = db.Basket.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "FirstName", basket.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", basket.ProductId);
            return View(basket);
        }

        // POST: Baskets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BasketId,ProductId,CustomerId,Quantity,SaveDate,Status")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "FirstName", basket.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", basket.ProductId);
            return View(basket);
        }

        // GET: Baskets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = db.Basket.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Basket basket = new BasketBO().Get(id);
            db.Basket.Remove(basket);
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

        public ActionResult CheckOut()
        {
            if (SessionParameters.Customer == null)
                return Content("Empty Card");
            var basket = new BasketBO().GetAll().Where(c => c.CustomerId == SessionParameters.Customer.CustomerId);
            return View(basket.ToList());

        }

        public ActionResult AddToCard(Guid id)
        {
            if (SessionParameters.Customer == null)
            {
                TempData["s"] = "null";
                return Redirect("/Products/ProductDetails?id=" + id + "");

            }


            var basket = new Basket()
            {
                BasketId = Guid.NewGuid(),
                CustomerId = SessionParameters.Customer.CustomerId,
                ProductId = id,
                SaveDate = DateTime.Now,
                Status = 0,
                IsArchive = false
            };

            if (new BasketBO().Insert(basket))
            {
                ViewBag.status = "ok";
                return Redirect("/Products/ProductDetails?id=" + id + "");
            }
            ViewBag.status = "nok";
            return Redirect("/Products/ProductDetails?id=" + id + "");

        }

        public ActionResult RemoveFromCart(Guid id)
        {
            Basket basket = new BasketBO().Get(id);
            db.Basket.Remove(basket);
            db.SaveChanges();
            return RedirectToAction("CheckOut");
        }
    }
}
