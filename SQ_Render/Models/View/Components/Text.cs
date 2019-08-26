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
        public int? Size { get; set; }
        public bool IsStrong { get; set; }
        public override string TagName => "p";
        public Text(string innerText)
        {
            InnerText = innerText;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.InnerHtml = InnerText;
            Color = Color.blue;
            if (Size != null)
            {
                if(IsStrong)
                {
                    tag.MergeAttribute("style", $"font-size: {Size}px; font-weight: 700;");
                }
                else
                {
                    tag.MergeAttribute("style", $"font-size: {Size}px;");
                }
            }
            if(IsStrong)
            {
                tag.MergeAttribute("style", $"font-weight: 700;");
            }
            
        }
    }
}