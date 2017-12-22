using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Tasks.Infrastructure.Helpers
{
    public static class HtmlHelpers
    {
        private const string includeJavascriptFormatString = "\t<script type=\"text/javascript\" src=\"{0}\"></script>\r\n";

        private const string cssIncludeKey = "CSSIncludes";
        
        public static MvcHtmlString ViewJavascriptResource(this HtmlHelper htmlHelper)
        {
            var action = htmlHelper.ViewContext.RouteData.Values["action"];
            var controller = htmlHelper.ViewContext.RouteData.Values["controller"];
            var includeFile = new StringBuilder();

            string targetFile = string.Format("/Javascript/Controllers/{0}/{1}.js", controller, action);
            includeFile.AppendFormat(includeJavascriptFormatString, targetFile);

            return MvcHtmlString.Create(includeFile.ToString());
        }
        public static   MvcHtmlString ViewStylesheetResource(this HtmlHelper htmlHelper)
        {
            string controllerOverride = htmlHelper.ViewBag.CssControllerOverride;
            var httpContext = htmlHelper.ViewContext.HttpContext;
            var action = htmlHelper.ViewContext.RouteData.Values["action"];
            var controller = controllerOverride ?? htmlHelper.ViewContext.RouteData.Values["controller"];

            string targetFile = string.Format("~/CSS/Controllers/{0}.css", controller);

            targetFile = UrlHelper.GenerateContentUrl(targetFile, httpContext);

            string actualFile = htmlHelper.ViewContext.HttpContext.Server.MapPath(targetFile);

            if (!File.Exists(actualFile))
            {
                return MvcHtmlString.Empty;
            }

            var includeFile = new StringBuilder(string.Format("\t<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />\r\n", targetFile));

            // Search for CSS resources for partial controls that might be contained in this page
            List<string> cssResourceFiles = null;

            if (httpContext.Items.Contains(cssIncludeKey))
            {
                cssResourceFiles = (List<string>)httpContext.Items[cssIncludeKey];
            }

            if (cssResourceFiles != null && cssResourceFiles.Count > 0)
            {
                foreach (var file in cssResourceFiles)
                {
                    includeFile.AppendFormat("\t<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />\r\n", file);
                }
            }

            return MvcHtmlString.Create(includeFile.ToString());
        }

    }
}