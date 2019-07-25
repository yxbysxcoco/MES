using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Const
{
    public static class ExtendMethods
    {
        public static MvcHtmlString GetHtmlString(this HtmlHelper html, string str)
        {
            return new MvcHtmlString(str);
        }
    }
}