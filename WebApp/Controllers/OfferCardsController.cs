using ClockStore.DTO;
using ClockStore.DTO.DBContext;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class OfferCardsController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: OfferCards
        public ActionResult Index()
        {
            return View(db.OfferCards.ToList());
        }

        // GET: OfferCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferCard offerCard = db.OfferCards.Find(id);
            if (offerCard == null)
            {
                return HttpNotFound();
            }
            return View(offerCard);
        }

        // GET: OfferCards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfferCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferCard offerCard)
        {
            if (ModelState.IsValid)
            {
                db.OfferCards.Add(offerCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offerCard);
        }

        // GET: OfferCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferCard offerCard = db.OfferCards.Find(id);
            if (offerCard == null)
            {
                return HttpNotFound();
            }
            return View(offerCard);
        }

        // POST: OfferCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OfferCard offerCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offerCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offerCard);
        }

        // GET: OfferCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferCard offerCard = db.OfferCards.Find(id);
            if (offerCard == null)
            {
                return HttpNotFound();
            }
            return View(offerCard);
        }

        // POST: OfferCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferCard offerCard = db.OfferCards.Find(id);
            db.OfferCards.Remove(offerCard);
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
