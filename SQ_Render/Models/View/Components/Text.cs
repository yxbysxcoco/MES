using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Text : AbstractElement
    {
        public String InnerText { get; set; }
        public override string TagName => "p";
        public Text(string innerText)
        {
            InnerText = innerText;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.InnerHtml = InnerText;
        }
    }
}