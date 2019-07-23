using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MES.Infrastructure
{
    public static class Select
    {
        public static MvcHtmlString ISelect(this HtmlHelper html)
        {
            string select = String.Format("" );
            return new MvcHtmlString(select);
        }
    }
}