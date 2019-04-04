using ClockStore.BLL;
using ClockStore.DTO;
using ClockStore.DTO.DBContext;
using ClockStore.DTO.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace WebApp.Controllers
{
    public class BasketsController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: Baskets
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
            {
                return Redirect("/Users/Login");
            }

            IQueryable<Basket> basket = db.Basket.Include(b => b.Customer).Include(b => b.Product);
            return View(basket.ToList());
        }

        // GET: Baskets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
            {
                return Redirect("/Users/Login");
            }

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
            if (SessionParameters.User == null)
            {
                return Redirect("/Users/Login");
            }

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
            if (SessionParameters.User == null)
            {
                return Redirect("/Users/Login");
            }

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
            if (SessionParameters.User == null)
            {
                return Redirect("/Users/Login");
            }

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
            {
                return Redirect("/Customers/Signin");
            }

            IEnumerable<Basket> basket = db.Basket.ToList().Where(c => c.CustomerId == SessionParameters.Customer.CustomerId && c.IsArchive == false);

            return View(basket);



        }

        public ActionResult AddToCard(Guid id)
        {
            List<Basket> list = new List<Basket>();
            if (SessionParameters.Customer == null)
            {
                TempData["s"] = "null";
                return Redirect("/Products/ProductDetails?id=" + id + "");

            }
            Product product = db.Product.Find(id);
            if (product.Count == 1 && product.IsBlocked.HasValue && product.IsBlocked.Value)
            {
                TempData["s"] = "Block";
                return Redirect("/Products/ProductDetails?id=" + id + "");
            }

            Basket basket = new Basket()
            {
                BasketId = Guid.NewGuid(),
                CustomerId = SessionParameters.Customer.CustomerId,
                ProductId = id,
                SaveDate = DateTime.Now,
                Status = 0,
                IsArchive = false,
                LangId = CultureInfo.CurrentCulture.Name
            };
            if (product.Count == 1 && !product.IsBlocked.Value)
            {
                product.IsBlocked = true;

            }

            db.Entry(product).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                db.Basket.Add(basket);
                db.SaveChanges();
                TempData["s"] = "Added";
                if (SessionParameters.Basket != null)
                {
                    list = SessionParameters.Basket;
                }

                list.Add(basket);
                SessionParameters.Basket = list;
                return Redirect("/Products/ProductDetails?id=" + id + "");
            }


            TempData["s"] = "Failed";
            return Redirect("/Products/ProductDetails?id=" + id + "");

        }

        public ActionResult RemoveFromCart(Guid id)
        {
            List<Basket> list = new List<Basket>();
            if (SessionParameters.Basket != null)
            {
                list = SessionParameters.Basket;
            }

            Basket basket = list.Single(c => c.BasketId == id);
            Product product = db.Product.Find(basket.ProductId);
            //product.Count++;
            if (product.IsBlocked.Value)
            {
                product.IsBlocked = false;
            }

            db.Entry(product).State = EntityState.Modified;
            //db.Basket.Remove(basket);
            db.SaveChanges();
            if (SessionParameters.Basket != null)
            {
                list.Remove(list.Single(c => c.BasketId == id));
                SessionParameters.Basket = list;
            }
            return RedirectToAction("CheckOut");
        }

        public ActionResult PrintCustomerApprov(Guid? id)
        {
            if (SessionParameters.Customer == null && SessionParameters.User == null)
            {
                return Redirect("/Customers/Signin");
            }

            if (id.HasValue)
            {
                IEnumerable<Basket> basket = db.Basket.ToList().Where(c => c.CustomerId == id);
                ViewBag.TotalAmount = db.Basket.ToList().Where(c => c.CustomerId == id).Sum(c => c.Product.PriceWithOff).ToString("N0");
                return View(basket);
            }
            else
            {
                IEnumerable<Basket> basket = db.Basket.ToList().Where(c => c.CustomerId == SessionParameters.Customer.CustomerId);
                ViewBag.TotalAmount = db.Basket.ToList().Where(c => c.CustomerId == SessionParameters.Customer.CustomerId).Sum(c => c.Product.PriceWithOff).ToString("N0");
                return View(basket);
            }

        }

        public void ClearCheckout()
        {
            List<Basket> list = SessionParameters.Basket;
            if (list.Any())
            {
                foreach (Basket item in list)
                {
                    db.Basket.Remove(item);
                    db.SaveChanges();
                }
            }
            SessionParameters.Basket = null;
        }

        public ActionResult FinalApproval()
        {
            if (SessionParameters.Customer == null)
            {
                return Redirect("/Customers/Signin");
            }

            CustomerBasket list = new CustomerBasket()
            {
                Customer = SessionParameters.Customer,
                Baskets = db.Basket.Where(c => c.CustomerId == SessionParameters.Customer.CustomerId && c.IsArchive == false).ToList()

            };

            ViewBag.TotalAmount = list.Baskets.Sum(c => c.Product.PriceWithOff);
            return View(list);
        }

        [HttpPost]
        public ActionResult FinalApproval(Guid CustomerId, string FirstName, string LastName, string Email, string Phone, string Address, string offerCode)
        {
            Customer cust = db.Customer.Find(CustomerId);
            OfferCard offer = db.OfferCards.FirstOrDefault(c => c.OfferCode == offerCode);
            if (offer == null)
            {
                ViewBag.ErrorOfferCard = "کد تخفیف اشتباه است";
                return View();
            }
            bool isvalid = db.CustomerOffers.Any(c => c.CustomerId == CustomerId && c.OfferCardId == offer.Id);
            if (isvalid)
            {
                ViewBag.ErrorOfferCard = "شما یکبار از این کد تخفیف استفاده کرده اید لطفا کد تخفیف دیگری را وارد کنید";
                return View();
            }
            cust.FirstName = FirstName;
            cust.LastName = LastName;
            cust.Email = Email;
            cust.Phone = Phone;
            cust.LangId = CultureInfo.CurrentCulture.Name;
            cust.Address = Address;
            db.Entry(cust).State = EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                return Redirect($"/Payment/Index?id={CustomerId}&offerCode={offerCode}");
            }

            return View();
        }


    }
}
