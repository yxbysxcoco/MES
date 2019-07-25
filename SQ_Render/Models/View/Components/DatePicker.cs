using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class DatePicker : AbstractElement
    {
        public DatePicker() : base("")
        {
        }

        public override TagBuilder InitTag(TagBuilder inputField)
        {
            base.InitTag(inputField);
            TagBuilder iframe = new TagBuilder("iframe");
            iframe.MergeAttribute("hidden", "");
            iframe.MergeAttribute("onload", @"$('.datepicker').datepicker();");

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "text");
            input.AddCssClass("datepicker");

            inputField.InnerHtml += input.ToString();
            inputField.InnerHtml += iframe;

            return inputField;
        }
    }
}