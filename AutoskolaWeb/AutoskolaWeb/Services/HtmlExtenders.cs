using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoskolaWeb.Services
{
    public static class HtmlExtenders
    {
        public static MvcHtmlString GetMomentJsDatePattern(this HtmlHelper html)
        {
            var shortDatePattern = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            return MvcHtmlString.Create(shortDatePattern.ToUpper());
        }

        public static MvcHtmlString GetMomentJsDateTimePattern(this HtmlHelper html)
        {
            var shortDatePattern = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern.ToUpper();
            var shortTimePattern = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern;
            shortTimePattern = shortTimePattern.Replace("tt", "A");
            return MvcHtmlString.Create(shortDatePattern + " " + shortTimePattern);
        }
    }
}