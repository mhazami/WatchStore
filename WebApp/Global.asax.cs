using ClockStore.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Application_BeginRequest()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("fa-IR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-IR");

                //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
        }


        void Session_End(object sender, EventArgs e)
        {
            Guid customerId = Guid.Empty;
            if (Session["Customer"] != null)
                customerId = ((Customer)Session["Customer"]).CustomerId;
            SessionParameters.ClearBasket(customerId);

        }
        public void Session_OnEnd()
        {

        }
    }
}
