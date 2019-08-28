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
        public bool IsInline { get; set; } = true;
        public string Rules { get; set; }
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

            if (IsInline)
            {
                tag.AddCssClass("layui-inline");
            }

            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("layui-form-label");
            label.InnerHtml = Text;

            TagBuilder div = new TagBuilder("div");
            if (IsInline)
            {
                div.AddCssClass("layui-input-inline");
            } else
            {
                div.AddCssClass("layui-input-block");
            }

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", Type);
            if(Rules != null)
            {
                input.MergeAttribute("lay-verify", Rules);
                var text = label.InnerHtml;
                label.InnerHtml = "<span style='color: red; font-size: 22px;'>*</span>" + "<span>"+text+"</span>";
            }
            input.MergeAttribute("autocomplete", "off");
            input.AddCssClass("layui-input");
            input.MergeAttribute("id", Id);

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

    public class CheckBoxInput : AbstractInput
    {

        public CheckBoxInput(string id, string text) : base(id, text)
        {
        }
        public override string Type => "checkbox";

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