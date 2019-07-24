using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class DatePicker : AbstractElement
    {
        public override MvcHtmlString Render()
        {
            TagBuilder inputField = new TagBuilder("div");

            TagBuilder iframe = new TagBuilder("iframe");
            iframe.MergeAttribute("hidden", "");
            iframe.MergeAttribute("onload", @"$('.datepicker').datepicker();");

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            input.AddCssClass("datepicker");

            inputField.InnerHtml += input.ToString();
            inputField.InnerHtml += iframe;

            return new MvcHtmlString(inputField.ToString());
        }
    }
}