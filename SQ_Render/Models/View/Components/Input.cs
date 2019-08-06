using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public abstract class AbstractInput : AbstractElement
    {
        public abstract string Type { get;}
        public Icon Icon { get; set; }
        public string Text { get; set; }
        public AbstractInput(string id, string text)
        {
            Id = id;
            Text = text;
        }
        public override string TagName => "div";

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.AddCssClass("layui-inline");

            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("layui-form-label");
            label.InnerHtml = Text;

            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("layui-input-inline");

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", Type);
            input.MergeAttribute("required", "");
            input.MergeAttribute("lay-verify", "required");
            input.MergeAttribute("autocomplete", "off");
            input.AddCssClass("layui-input");

            div.InnerHtml = input.ToString();
            tag.InnerHtml = label.ToString();
            tag.InnerHtml += div.ToString();
        }
        
    }
    public class TextInput : AbstractInput
    {
        public TextInput(string id, string text) : base(id, text)
        {
        }
        public override string Type => "text";

    }

    public class PasswordInput : AbstractInput
    {
        public PasswordInput(string id, string text) : base(id, text)
        {
        }
        public override string Type => "password";

    }

    public class EmailInput : AbstractInput
    {
        public EmailInput(string id, string text) : base(id, text)
        {
        }

        public override string Type => "email";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            TagBuilder span = new TagBuilder("span");
            span.AddCssClass("helper-text");
            span.MergeAttribute("data-error", "请输入合法邮箱");
            span.MergeAttribute("data-success", "OK");
            tag.InnerHtml += span;
        }

    }
}