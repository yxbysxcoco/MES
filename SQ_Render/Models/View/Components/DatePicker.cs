using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class DatePicker : AbstractElement
    {
        public override string TagName => "div";


        public override void InitTag(HtmlHelper htmlHelper)
        {
            base.InitTag(htmlHelper);
            TagBuilder iframe = new TagBuilder("iframe");
            iframe.MergeAttribute("hidden", "");
            iframe.MergeAttribute("onload", @"$('.datepicker').datepicker();");

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            input.AddCssClass("datepicker");

            tag.InnerHtml += input.ToString();
            tag.InnerHtml += iframe;
        }
    }
}