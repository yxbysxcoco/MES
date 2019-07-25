using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Input : AbstractElement
    {
        public string Type { get; set; }
        public Icon Icon { get; set; }
        public string Text { get; set; }
        public Input(string type, string id, string text):base("div", id)
        {
            Type = type;
            Text = text;
        }
        public override TagBuilder InitTag(TagBuilder inputField)
        {
            inputField.AddCssClass("input-field");
            inputField.AddCol(Col);

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("id", Id);
            input.MergeAttribute("name", "name");
            input.MergeAttribute("type", Type);
            input.AddCssClass("validate");

            TagBuilder label = new TagBuilder("label");
            label.MergeAttribute("for", Id);
            label.InnerHtml = Text;

            inputField.InnerHtml += input;
            inputField.InnerHtml += label;

            if(Type == "email")
            {
                TagBuilder span = new TagBuilder("span");
                span.AddCssClass("helper-text");
                span.MergeAttribute("data-error", "请输入合法邮箱");
                span.MergeAttribute("data-success", "OK");
                inputField.InnerHtml += span;
            }

            return inputField;
        }
    }
}