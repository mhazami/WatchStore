using ClockStore.DTO;
using ClockStore.DTO.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class PaymentController : Controller
    {
        private ClockStoreContext db = new ClockStoreContext();
        // GET: Payment
        public ActionResult Index(Guid id, string offerCode)
        {
            if (SessionParameters.Customer == null)
            {
                return Redirect("/Customers/Signin");
            }

            List<Basket> baskets = db.Basket.Where(c => c.CustomerId == SessionParameters.Customer.CustomerId && c.IsArchive == false).ToList();

            Order order = new Order
            {
                OrderId = Guid.NewGuid(),
                SaveDate = DateTime.Now,
                IsFinaly = false,
                CurrentPrice = baskets.Sum(c => c.Product.PriceWithOff)
            };

            if (!string.IsNullOrEmpty(offerCode))
            {

                OfferCard offer = db.OfferCards.FirstOrDefault(c => c.OfferCode == offerCode);
                bool isvalid = db.CustomerOffers.Any(c => c.CustomerId == SessionParameters.Customer.CustomerId && c.OfferCardId == offer.Id);
                if (!isvalid)
                {
                    order.OfferCardId = offer.Id;
                    switch (offer.OfferType)
                    {
                        case Enums.OfferType.TwoByOne:
                            break;
                        case Enums.OfferType.percent:
                            order.CurrentPrice = order.CurrentPrice - (order.CurrentPrice * offer.OfferAmount / 100);
                            break;
                        case Enums.OfferType.Price:
                            order.CurrentPrice = order.CurrentPrice - Convert.ToDecimal(offer.OfferCode);
                            break;

                    }
                }

            }
            //order.CurrentPrice = 100;
            db.Order.Add(order);
            db.SaveChanges();

            System.Net.ServicePointManager.Expect100Continue = false;
            ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();
            int Amount = Convert.ToInt32(order.CurrentPrice);
            string url = "http://kookwatch.com/Payment/Verify?cusId=" + SessionParameters.Customer.CustomerId + "&orderid=" + order.OrderId + "";
            //var url = "http://localhost:40412/Payment/Verify?cusId=" + SessionParameters.Customer.CustomerId + "&orderid=" + order.OrderId + "";
            int Status = zp.PaymentRequest("a3078700-d5bd-11e8-81b7-005056a205be", Amount, "درگاه پرداخت Trend Watch", "info@kookwatch.com", "09120332214", url, out string Authority);

            if (Status == 100)
            {

                Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);
                //Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
            }
            else
            {
                ViewBag.Error = "Error : " + Status;
            }
            return View();
        }

        public ActionResult Verify(Guid cusId, Guid orderid)
        {
            Order order = db.Order.Find(orderid);


            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
                    int Amount = Convert.ToInt32(order.CurrentPrice);
                    System.Net.ServicePointManager.Expect100Continue = false;
                    ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();

                    int Status = zp.PaymentVerification("a3078700-d5bd-11e8-81b7-005056a205be", Request.QueryString["Authority"].ToString(), Amount, out long RefID);

                    if (Status == 100)
                    {
                        order.IsFinaly = true;
                        db.SaveChanges();
                        ViewBag.IsSuccess = true;
                        ViewBag.RefId = RefID;
                        List<Basket> baskets = db.Basket.Where(c => c.CustomerId == SessionParameters.Customer.CustomerId && c.IsArchive == false).ToList();
                        ViewBag.Status = "پرداخت شما با موفقیت انجام شد";
                        var customerOffer = new CustomerOffer()
                        {
                            CustomerId = cusId,
                            OfferCardId = order.OfferCardId
                        };
                        db.CustomerOffers.Add(customerOffer);
                        db.SaveChanges();
                        foreach (Basket item in baskets)
                        {
                            BasketOrder basketOrder = new BasketOrder()
                            {
                                BasketId = item.BasketId,
                                OrderId = orderid
                            };
                            db.BasketOrder.Add(basketOrder);
                            db.SaveChanges();
                            item.LangId = CultureInfo.CurrentCulture.Name;
                            item.IsArchive = true;
                            db.Entry(item).State = EntityState.Modified;
                            db.SaveChanges();

                            Product product = item.Product;
                            product.IsBlocked = false;
                            product.Count--;
                            db.Entry(product).State = EntityState.Modified;
                            db.SaveChanges();

                        }
                    }
                    else
                    {
                        ViewBag.IsSuccess = false;

                        ViewBag.Status = "خطا در انجام تراکنش";

                    }

                }
                else
                {
                    return Redirect("/Baskets/FinalApproval");
                    //Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
                }
            }
            else
            {
                Response.Write("Invalid Input");
            }
            return View();
        }
    }
}