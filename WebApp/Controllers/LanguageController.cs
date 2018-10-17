using ClockStore.DTO;
using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class LanguageController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();
        public ActionResult Loadlanguages()
        {
            var list = db.Language.ToList();
            var res = new List<Language>();
            if (CultureInfo.CurrentCulture.Name != null)
            {
                var current = list.First();
                res.Insert(0, list.FirstOrDefault(c => c.LangId == CultureInfo.CurrentCulture.Name));
                res.Insert(1, list.FirstOrDefault(c => c.LangId != CultureInfo.CurrentCulture.Name));

            }
            return PartialView("PVLoadLanguages", res);
        }

        public ActionResult ChangeLanguage(string langId, string url)
        {
            var currentUrl = string.Empty;
            //var serverurl = "http://www.kookwatch.com/";
            //var x = serverurl.Length;
            if (url.Contains("localhost"))
                currentUrl = url.Remove(0, 22);
            else
                currentUrl = url.Remove(0, 24);
            if (string.IsNullOrEmpty(currentUrl) || currentUrl == "/")
                currentUrl = "/Home/Index";
            if (!string.IsNullOrEmpty(langId))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(langId);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langId);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = langId;
            Response.Cookies.Add(cookie);
            ViewBag.LangId = CultureInfo.CurrentCulture.Name;

            return Content("<script>window.location = '" + url + "';</script>");
        }


        public ActionResult GenerateLanguageDropDownList()
        {
            var list = db.Language.ToList();
            ViewBag.List = new SelectList(list, "LangId", "Title");
            return PartialView("PVLanguageDropDownList");
        }


    }
}