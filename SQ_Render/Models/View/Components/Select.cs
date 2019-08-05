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
            tag.MergeAttribute("lay-verify", " ");
            tag.MergeAttribute("lay-search", "");

            TagBuilder firstOption = new TagBuilder("option");
            firstOption.InnerHtml = "请选择";
            tag.InnerHtml = firstOption.ToString();

            foreach(string key in Options.Keys)
            {
                TagBuilder option = new TagBuilder("option");
                option.MergeAttribute("value", key);
                option.InnerHtml = Options[key];
                tag.InnerHtml += option;
            }
        }
    }
}