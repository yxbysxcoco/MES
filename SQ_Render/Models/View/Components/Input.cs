using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public AbstractInput()
        {
        }
        public  void SetIdAndText<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : EntityBase
        {
            var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
            Id = member.Name;
            Text = (member.GetCustomAttribute<DisplayAttribute>()).Name;
        }
        public void SetIdAndText<TEntity>(Expression<Func<object, object>> expression,string text) where TEntity : EntityBase
        {
            var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
            Id = member.Name;
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
        public TextInput() : base()
        {
        }
        public override string Type => "text";

    }

    public class CheckBoxInput : AbstractInput
    {
        public string value { get; set; }
        public CheckBoxInput(string id, string text) : base(id, text)
        {
            value = "1";
        }
        public CheckBoxInput() : base()
        {
            value = "1";
        }
        public override string Type => "checkbox";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("value", value);
        }

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