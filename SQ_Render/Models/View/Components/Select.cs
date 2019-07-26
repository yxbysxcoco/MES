using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Select : AbstractElement
    {
        public override string TagName => "select";

        public Dictionary<string, string> Options { get; set; }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.AddCssClass("input-field");

            TagBuilder iframe = new TagBuilder("iframe");
            iframe.MergeAttribute("hidden", "");
            iframe.MergeAttribute("onclick", @"$('select').formSelect();");

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

            TagBuilder label = new TagBuilder("label")
            {
                InnerHtml = "请选择.."
            };

            tag.InnerHtml = select.ToString();
            tag.InnerHtml += label.ToString();
        }
    }
}