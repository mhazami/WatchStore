using ClockStore.DTO;
using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class UserCommentsController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();
        // GET: UserComments
        public ActionResult Index()
        {
            var list = db.UserComments.ToList();
            return View();
        }

        public ActionResult Create()
        {
            return PartialView("PVModify");
        }

        public string AddComment(Guid id,string name, string title, string message)
        {
            if (id == Guid.Empty)
                return "خطا در ارسال نظر";
            if (string.IsNullOrEmpty(name))
                return "لطفا نام خود را وارد کنید";
            if (string.IsNullOrEmpty(title))
                return "لطفا عنوان را وارد کنید";
            if (string.IsNullOrEmpty(message))
                return "نظر خود را بنویسید";
            var obj = new UserComments()
            {
                ProductId = id,
                FullName = name,
                Title = title,
                Message = message,
                IsAccept = false
            };
            db.UserComments.Add(obj);
            db.SaveChanges();
            return "نطر شما با موفقیت ثبت شد";

        }
    }
}