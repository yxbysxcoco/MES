using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Input : AbstractElement
    {
        public String Type { get; set; }
        public Icon Icon { get; set; }
        public String Text { get; set; }
        public Input(String type, String id, String text)
        {
            Id = id;
            Type = type;
            Text = text;
        }
        private Input() { }
        public override TagBuilder Render()
        {
            TagBuilder inputField = new TagBuilder("div");
            inputField.AddCssClass("input-field");
            inputField.AddCol(Col);

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("id", Id);
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