﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class DatePicker : AbstractElement
    {
        public string Text { get; set; }
        public override string TagName => "div";
        public DatePicker(string id, string text)
        {
            Id = id;
            Text = text;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.AddCssClass("layui-inline");
            AddChildElement(new IFrame(@"var laydate = layui.laydate; laydate.render({elem:'#" + Id + "', type: 'datetime', range: true}); "));

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("datepicker", "true");
            input.MergeAttribute("type", "text");
            input.MergeAttribute("placeholder", "~");
            input.AddCssClass("layui-input");
            input.MergeAttribute("id", Id);

            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("layui-form-label");
            label.InnerHtml = Text;

            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("layui-input-inline");

            tag.InnerHtml = label.ToString();
            div.InnerHtml += input.ToString();

            tag.InnerHtml += div.ToString();
        }
    }
}