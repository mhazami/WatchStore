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
    public class NewsController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();

        // GET: News
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            return View(db.News.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsId,Title,Position,Description")] News news, HttpPostedFileBase file)
        {
            news.NewsId = Guid.NewGuid();
            var image = new FileBO().Insert(file);
            news.FileId = image.FileId;
            news.File = image;
            db.News.Add(news);
            if (db.SaveChanges() > 0)
                return RedirectToAction("Index");



            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsId,Title,Position,Description")] News news, HttpPostedFileBase file)
        {
            var fileid = db.Product.Find(news.NewsId).FileId;
            if (file != null)
            {

                new FileBO().UpDate(fileid, file);

            }

            news.FileId = fileid;

            if (new NewsBO().Update(news))
                return RedirectToAction("Index");

            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (SessionParameters.User == null)
                 return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();

            if (new FileBO().Delete(news.FileId))
                return RedirectToAction("Index");
            return View(news);
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
            var count = db.News.Count();
            if (count <= 3)
            {
                var list = db.News.ToList();
                return PartialView("PVWallPaper", list);
            }
            else
            {
                var res =new List<News>();
                var list = db.News.ToList().OrderBy(c => c.Position);
                for (int i = 0; i < 3; i++)
                {
                    res.Add(list.ToList()[i]);
                }
                return PartialView("PVWallPaper", res);
            }
        }
    }
}
