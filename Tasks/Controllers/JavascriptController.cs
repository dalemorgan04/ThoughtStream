using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.SqlServer.Server;

namespace Tasks.Controllers
{
    public class JavascriptController : Controller
    {
        public JavascriptController()
        {
        }

        public ContentResult ResolveUrl()
        {
            string action = "";
            string controller = "";

            RouteValueDictionary routeParam = new RouteValueDictionary();
            // Enumerate the Querystring and extract the parameters, only 'action' and 'controller' are reserved parameters
            foreach (string key in Request.QueryString.AllKeys)
            {
                string lowerCaseKey = key.ToLower().Trim();
                string queryStringValue = Request.QueryString[key];
                if (lowerCaseKey == "action")
                    action = queryStringValue;
                else if (lowerCaseKey == "controller")
                    controller = queryStringValue;
                else
                {
                    if (!routeParam.ContainsKey(key))
                        routeParam.Add(key, queryStringValue);
                    else
                        routeParam[key] = queryStringValue;
                }
            }

            string resolvedUrl = Url.Action(action, controller, routeParam);
            return Content(resolvedUrl, "text/plain");
        }
    }
}