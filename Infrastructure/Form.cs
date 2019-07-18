using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MES.Infrastructure
{
    public static class Form
    {
        public static MvcHtmlString Input(this HtmlHelper html, String id, String type)
        {
            string input = String.Format("<input class='form-control' />");
            return new MvcHtmlString(input);
        }
        public static MvcHtmlString Select(this HtmlHelper html, string id, string[] options)
        {
            TagBuilder select = new TagBuilder("select");
            foreach(var i in options)
            {
                TagBuilder option = new TagBuilder("option");
                option.SetInnerText(i);
                select.InnerHtml += option;
            }
            return new MvcHtmlString(select.ToString());
        }
        public static MvcHtmlString Button(this HtmlHelper html, string url)
        {
            string button = String.Format("<button class='btn btn-primary'>按钮</button>");
            return new MvcHtmlString(button);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper html)
        {
            string datepicker = String.Format("<input class='form-control' id='datepicker' />");
            return new MvcHtmlString(datepicker);
        }
    }
}