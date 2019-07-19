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
            string input = String.Format("<input class='form-control' type='{0}' id='{1}' />", type, id);
            return new MvcHtmlString(input);
        }
        public static MvcHtmlString Selector(this HtmlHelper html, string id)
        {

            //TagBuilder select = new TagBuilder("select");
            //select.MergeAttribute("id", id);
            string select = String.Format("<select  id='{0}'><option>cnm</option></select>", id);
            //foreach(var i in options)
            //{
            //    TagBuilder option = new TagBuilder("option");
            //    option.SetInnerText(i);
            //    select.InnerHtml += option;
            //}
            return new MvcHtmlString(select);
        }
        public static MvcHtmlString Button(this HtmlHelper html, string id)
        {
            string button = String.Format("<button class='btn btn-primary' id='{0}'>按钮</button>", id);
            return new MvcHtmlString(button);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper html, string id)
        {
            string datepicker = String.Format("<input class='form-control' id='{0}' />", id);
            return new MvcHtmlString(datepicker);
        }
    }
}