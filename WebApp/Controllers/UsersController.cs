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
    public class UsersController : Controller
    {
        ClockStoreContext db = new ClockStoreContext();
        // GET: Users
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                return View("Login");
            if (SessionParameters.User.UserName != "host")
                return Redirect("/Products/Index");
            var list = new UserBO().GetAll().Where(c => c.UserName != "host");
            return list.Any() ? View(list) : View("Create");
        }

        // GET: Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = new UserBO().Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,UserName,PassWord")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = Guid.NewGuid();
                user.PassWord.ToLower();
                user.UserName.ToLower();
                if (new UserBO().Insert(user))
                    return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = new UserBO().Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,UserName,PassWord")] User user)
        {
            if (ModelState.IsValid)
            {
                if (new UserBO().Update(user))
                    return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = new UserBO().Get(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (new UserBO().Delete(id))
                return RedirectToAction("Index");
            return View(new UserBO().Get(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                new UserBO().Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string PassWord)
        {
            var user = db.User.FirstOrDefault(c => c.UserName == UserName.ToLower() && c.PassWord == PassWord.ToLower());
            if (user != null)
            {
                ViewBag.Alert = "";
                SessionParameters.User = user;
                return Redirect("/Products/Index");
            }
            ViewBag.Alert = "Invalid UserName or PassWord!";
            return View();
        }

        public ActionResult Logout()
        {
            SessionParameters.User = null;
            return Redirect("Login");
        }
    }
}
