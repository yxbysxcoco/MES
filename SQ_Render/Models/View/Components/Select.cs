using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Select : AbstractElement
    {
        public Select() : base("select")
        {
        }

        public Dictionary<string, string> Options { get; set; }
        public override TagBuilder InitTag(TagBuilder inputField)
        {
            base.InitTag(inputField);

            inputField.AddCssClass("input-field");

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

            TagBuilder label = new TagBuilder("label");
            label.InnerHtml = "请选择..";

            inputField.InnerHtml = select.ToString();
            inputField.InnerHtml += label.ToString();
            return inputField;
        }
    }
}