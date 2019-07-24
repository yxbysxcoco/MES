using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Select : AbstractElement
    {
        public Dictionary<string, string> Options { get; set; }
        public override MvcHtmlString Render()
        {
            TagBuilder inputField = new TagBuilder("div");
            inputField.AddCssClass("input-field");

            TagBuilder select = new TagBuilder("select");
            TagBuilder firstOption = new TagBuilder("option");
            select.InnerHtml = firstOption.ToString();

            foreach(string key in Options.Keys)
            {
                TagBuilder option = new TagBuilder("option");
                option.MergeAttribute("value", key);
                option.InnerHtml = Options[key];
                select.InnerHtml += option;
            }
            inputField.InnerHtml = select.ToString();
            return new MvcHtmlString(inputField.ToString());
        }
    }
}