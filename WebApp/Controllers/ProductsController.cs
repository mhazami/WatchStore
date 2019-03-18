using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClockStore.BLL;
using ClockStore.DTO;
using ClockStore.DTO.ViewModels;
using ClockStore.DTO.DBContext;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        ClockStoreContext db = new ClockStoreContext();
        // GET: Products
        public ActionResult Index()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            var product = db.Product.Where(c => c.Count > 0);
            return View(product.ToList());
        }

        [HttpPost]
        public ActionResult Index(string ProductName, string FromPrice, string ToPrice,
            string Code, string Count, string FromOff, string ToOff, string LangId)
        {
            StringBuilder query = new StringBuilder();
            var list = new List<Product>();
            query.Append("select * from Products ");
            if (string.IsNullOrEmpty(ProductName)
                && string.IsNullOrEmpty(Code)
                && string.IsNullOrEmpty(FromOff)
                && string.IsNullOrEmpty(ToPrice)
                && string.IsNullOrEmpty(Count)
                && string.IsNullOrEmpty(ToOff)
                && string.IsNullOrEmpty(FromPrice))
            {
                list = db.Product.Where(c => c.LangId == LangId).ToList();
                return View(list.ToList());
            }
            else
            {
                query.Append($"Where LangId='{LangId}'");
            }
            if (!string.IsNullOrEmpty(ProductName))
                query.Append($" And ProductName LIKE '%{ProductName.Trim()}%' ");



            if (!string.IsNullOrEmpty(Code))
            {
                if (!string.IsNullOrEmpty(ProductName))
                    query.Append($"AND Code = {Code} ");
                else
                    query.Append($"Code = {Code} ");
            }



            if (!string.IsNullOrEmpty(Count))
            {
                if (!string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND Count = {int.Parse(Count)} ");
                else
                    query.Append($"Count = {int.Parse(Count)} ");
            }


            if (!string.IsNullOrEmpty(FromPrice))
            {
                if (!string.IsNullOrEmpty(Count) || !string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND Price > {decimal.Parse(FromPrice.Trim())} ");
                else
                    query.Append($"Price > {decimal.Parse(FromPrice.Trim())} ");
            }


            if (!string.IsNullOrEmpty(ToPrice))
            {
                if (!string.IsNullOrEmpty(FromPrice) || !string.IsNullOrEmpty(Count) || !string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND Price < {decimal.Parse(ToPrice)} ");

                else
                    query.Append($"Price < {decimal.Parse(ToPrice)} ");
            }

            if (!string.IsNullOrEmpty(FromOff))
            {
                if (!string.IsNullOrEmpty(ToPrice) || !string.IsNullOrEmpty(FromPrice) || !string.IsNullOrEmpty(Count) || !string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND [Off] > {int.Parse(FromOff)} ");

                else
                    query.Append($"[Off] > {int.Parse(FromOff)} ");
            }


            if (!string.IsNullOrEmpty(ToOff))
            {
                if (!string.IsNullOrEmpty(FromOff) || !string.IsNullOrEmpty(ToPrice) || !string.IsNullOrEmpty(FromPrice) || !string.IsNullOrEmpty(Count) || !string.IsNullOrEmpty(ProductName) || !string.IsNullOrEmpty(Code))
                    query.Append($"AND [Off] < {int.Parse(ToOff.Trim())}");

                else
                    query.Append($"[Off] < {int.Parse(ToOff.Trim())}");
            }



            list = db.Product.SqlQuery(query.ToString()).ToList();
            return View(list.ToList());
        }


        public ActionResult ProductListOfEmpty()
        {
            var list = db.Product.Where(c => c.Count <= 0);
            return View(list.ToList());
        }
        // GET: Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = new ProductBO().Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            ViewBag.CategoryId = new SelectList(new CategoryBO().GetAll(), "CategoryId", "CategoryName");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                product.ProductId = Guid.NewGuid();
                var image = new FileBO().Insert(file);
                product.FileId = image.FileId;
                product.File = image;
                db.Product.Add(product);
                if (db.SaveChanges() > 0)
                {
                    if (Session["Color"] != null)
                    {
                        var colorList = (List<Color>)Session["Color"];
                        foreach (var item in colorList)
                        {
                            item.ProductId = product.ProductId;
                            db.Color.Add(item);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }
            }


            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryId = new SelectList(new CategoryBO().GetAll(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            var fileid = db.Product.Find(product.ProductId).FileId;
            if (file != null)
            {

                new FileBO().UpDate(fileid, file);

            }

            product.FileId = fileid;
            product.LangId = "fa-IR";
            if (new ProductBO().Update(product))
                return RedirectToAction("Index");

            //ViewBag.CategoryId = new SelectList(new CategoryBO().GetAll(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (SessionParameters.User == null)
                return Redirect("/Users/Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = new ProductBO().Get(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();

            if (new FileBO().Delete(product.FileId))
                return RedirectToAction("Index");
            return View(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                new ProductBO().Dispose();
            }
            base.Dispose(disposing);
        }


        #region Method
        public ActionResult ProductsList()
        {
            var list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name).ToList();
            return PartialView("PVProductsList", list);
        }

        //نمایش لیست محصولات بر اساس برند
        public ActionResult ProducsList(Guid? id, Enums.ProductSex? cat)
        {
            var list = new List<Product>();
            if (cat != 0 && cat != null)
                list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name && c.CategoryId == id && c.Sex == cat).ToList();
            else
                list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name && c.CategoryId == id).ToList();
            return View(list);
        }

        //نمایش لیست محصولات کلاسیک
        public ActionResult Classic()
        {
            var list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name && c.MasterCategory == Enums.MasterCategory.Classic).ToList();
            return View("ProducsList", list);
        }

        //نمایش محصولات اسپرت
        public ActionResult Sport()
        {
            var list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name && c.MasterCategory == Enums.MasterCategory.Sport).ToList();
            return View("ProducsList", list);
        }

        //نمایش محصولات بچه گانه
        public ActionResult Kids()
        {
            var list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name && c.Sex == Enums.ProductSex.Kids).ToList();
            return View("ProducsList", list);
        }

        //نمایش محصولات زنانه
        public ActionResult Women()
        {
            var list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name && c.Sex == Enums.ProductSex.Women).ToList();
            return View("ProducsList", list);
        }

        //نمایش محصولات مردانه
        public ActionResult Men()
        {
            var list = db.Product.Where(c => c.Count > 0 && c.LangId == CultureInfo.CurrentCulture.Name && c.Sex == Enums.ProductSex.Men).ToList();
            return View("ProducsList", list);
        }


        public ActionResult ProductDetails(Guid id)
        {
            var product = new ProductBO().Get(id);
            var extraimage = new ExtraImagesBO().GetAll().Where(c => c.ProductId == id).ToList();
            product.ExtraImages = extraimage;
            ViewBag.Id = product.ProductId;
            ViewBag.FileId = product.FileId;


            return PartialView("ProductDetails", product);
        }

        public ActionResult AddCart(Guid? id)
        {
            if (SessionParameters.Customer != null)
                return Redirect("/Baskets/AddToCard?id=" + id + "");
            return Redirect("/Customers/Signin");
        }

        public ActionResult Luxury()
        {
            var list = new VideoProduct()
            {
                ProductsList = db.Product.Where(c => c.Isluxury.Value).ToList(),
                VideoList = db.VideoHandler.Where(c => c.Position == Enums.VideoPosition.Luxtury).ToList()
            };
            return View(list);
        }

        public ActionResult Deals()
        {
            var list = new VideoProduct()
            {
                ProductsList = db.Product.Where(c => c.IsDeals.Value).ToList(),
                VideoList = db.VideoHandler.Where(c => c.Position == Enums.VideoPosition.Deals).ToList()
            };
            return View(list);
        }

        public ActionResult NewSeason()
        {
            var list = new VideoProduct()
            {
                ProductsList = db.Product.Where(c => c.IsNewSeason.Value).ToList(),
                VideoList = db.VideoHandler.Where(c => c.Position == Enums.VideoPosition.NewSeason).ToList()
            };
            return View(list);
        }
        #endregion Method


        public ActionResult ProductDetailsBottom()
        {
            var obj = db.Product.Find(new Guid("bbc02562-13c2-472a-b637-08c268de15a1"));
            return PartialView("PVProductDetailsBottom", obj);
        }

        public ActionResult SearchResult(string id)
        {
            var list = db.Product.Where(c => c.ProductName.Contains(id) || c.Description.Contains(id) && c.LangId == CultureInfo.CurrentCulture.Name).ToList();
            if (list.Any())
                ViewBag.Res = string.Empty;
            else
                ViewBag.Res = "نتیجه ای یافت نشد";
            return View(list);

        }

        public ActionResult AddColor(string color, string name, string enable)
        {
            if (!string.IsNullOrEmpty(color))
            {
                var list = new List<Color>();
                if (Session["Color"] != null)
                    list = (List<Color>)Session["Color"];
                var col = new Color()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Value = color,
                    Enable = !string.IsNullOrEmpty(enable) && enable == "on" ? true : false

                };
                list.Add(col);
                Session["Color"] = list;
                return Content("true");
            }
            return Content("false");
        }

        public JsonResult GetColor(Guid id)
        {
            var colors = db.Color.Where(c => c.ProductId == id).ToList();
            var list = new List<object>();
            var obj = new Object();
            foreach (var item in colors)
            {
                obj = new
                {
                    Code = item.Value,
                    ColorId = item.Id
                };
                list.Add(obj);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
