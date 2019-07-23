using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MES.Infrastructure
{
    public static class DatePicker
    {
        public static MvcHtmlString IDatePicker(this HtmlHelper html, string id)
        {
            string datepicker = String.Format("<input type='text' class='layui-input' id='{0}'>", id);
            return new MvcHtmlString(datepicker);
        }
    }
}