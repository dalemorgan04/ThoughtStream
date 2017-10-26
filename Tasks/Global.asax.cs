using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Tasks
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
        //sfgsfdgdfg
        protected void Application_Start()
        {
            ApplicationBootstrap.Start();           
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_End()
        {
            ApplicationBootstrap.End();
        }
    }
}
