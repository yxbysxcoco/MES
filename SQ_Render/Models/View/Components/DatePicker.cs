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

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            if(InnerChildElements == null)
            {
                InnerChildElements = new List<AbstractElement>();
            }
            InnerChildElements.Add(new IFrame(@"$('.datepicker').datepicker();"));

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            input.AddCssClass("datepicker");
            input.MergeAttribute("placeholder", "请选择时间日期");

            tag.InnerHtml = input.ToString();
        }
    }
}