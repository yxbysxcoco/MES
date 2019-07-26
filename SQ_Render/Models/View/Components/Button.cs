using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Button : AbstractElement
    {
        public String Text { get; set; }
        public String EventType { get; set; }
        public String EventMethod { get; set; }

        public override string TagName => "button";

        public Button(String text)
        {
            Text = text;
        }
        public override void InitTag(HtmlHelper htmlHelper)
        {
            base.InitTag(htmlHelper);

            tag.AddCssClass("btn");
            tag.InnerHtml = Text;

            if (EventType != null)
            {
                tag.MergeAttribute("on" + EventType, EventMethod);
            }
        }
    }
}