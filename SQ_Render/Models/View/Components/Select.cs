using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Select : AbstractElement
    {
        public override string TagName => "div";
        public string Text { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public Select(string text)
        {
            Text = text;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.AddCssClass("layui-inline");
            AddChildElement(new IFrame(@"layui.use('form', function(){ var form = layui.form; form.render();});"));

            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("layui-form-label");
            label.InnerHtml = Text;

            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("layui-input-inline");

            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("lay-verify", " ");
            select.MergeAttribute("lay-search", "");
            select.MergeAttribute("id", Id);
            select.MergeAttribute("isSelect", "true");

            TagBuilder firstOption = new TagBuilder("option");
            firstOption.InnerHtml = "请选择";
            select.InnerHtml = firstOption.ToString();

            foreach(string key in Options.Keys)
            {
                TagBuilder option = new TagBuilder("option");
                option.MergeAttribute("value", key);
                option.InnerHtml = Options[key];
                select.InnerHtml += option;
            }

            div.InnerHtml = select.ToString();
            tag.InnerHtml = label.ToString();
            tag.InnerHtml += div.ToString();
        }
    }
}