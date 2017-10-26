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

        /// <summary>
        /// Will include the matching javascript file taken from /Javascript/Controllers/{Controller}/{Action}
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString ViewJavascriptResource(this HtmlHelper htmlHelper)
        {
            var action = htmlHelper.ViewContext.RouteData.Values["action"];
            var controller = htmlHelper.ViewContext.RouteData.Values["controller"];
            var includeFile = new StringBuilder();

            string targetFile = string.Format("/Javascript/Controllers/{0}/{1}.js", controller, action);
            includeFile.AppendFormat(includeJavascriptFormatString, targetFile);

            return MvcHtmlString.Create(includeFile.ToString());
        }
    }
}