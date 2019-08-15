using SQ_Render.Models.View.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Modal : Container
    {
        public string TableId { get; set; }
        public string Text { get; set; } = "按钮";
        public override string TagName => "div";
        public Modal(string id)
        {
            Id = id;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("hidden", "");
            tag.MergeAttribute("id", Id);

            AddChildElement(new IFrame($@"initApp(() => lemon.initShowModalBtn('{Id}', '{Text}'))"));
        }
    }
}