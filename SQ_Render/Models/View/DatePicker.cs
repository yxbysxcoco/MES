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
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            input.AddCssClass("datepicker");

            return new MvcHtmlString(input.ToString());
        }
    }
}