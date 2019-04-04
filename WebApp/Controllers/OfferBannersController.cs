using ClockStore.BLL;
using ClockStore.DTO;
using ClockStore.DTO.DBContext;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class OfferBannersController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: OfferBanners
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            IQueryable<OfferBanner> offerBanners = db.OfferBanners.Include(o => o.File);
            return View(offerBanners.ToList());
        }

        // GET: OfferBanners/Details/5
        public ActionResult Details(int? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferBanner offerBanner = db.OfferBanners.Find(id);
            if (offerBanner == null)
            {
                return HttpNotFound();
            }
            return View(offerBanner);
        }

        // GET: OfferBanners/Create
        public ActionResult Create()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            return View();
        }

        // POST: OfferBanners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OfferBanner offerBanner, HttpPostedFileBase file)
        {
           
                File image = new FileBO().Insert(file);
                offerBanner.FileId = image.FileId;
                //offerBanner.File = image;
                db.OfferBanners.Add(offerBanner);
                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
            


            return View(offerBanner);
        }

        // GET: OfferBanners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferBanner offerBanner = db.OfferBanners.Find(id);
            if (offerBanner == null)
            {
                return HttpNotFound();
            }
            return View(offerBanner);
        }

        // POST: OfferBanners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OfferBanner offerBanner, HttpPostedFileBase file)
        {
            var fileid = db.OfferBanners.Find(offerBanner.Id).FileId;
            if (file != null)
            {

                new FileBO().UpDate(fileid, file);

            }

            offerBanner.FileId = fileid;
            if (new OfferBannerBO().Update(offerBanner))
                return RedirectToAction("Index");

            return View(offerBanner);
        }

        // GET: OfferBanners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfferBanner offerBanner = db.OfferBanners.Find(id);
            if (offerBanner == null)
            {
                return HttpNotFound();
            }
            return View(offerBanner);
        }

        // POST: OfferBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OfferBanner offerBanner = db.OfferBanners.Find(id);
            db.OfferBanners.Remove(offerBanner);
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
