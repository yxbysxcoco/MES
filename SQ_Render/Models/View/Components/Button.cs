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
        public Icon Icon { get; set; }
        public String EventType { get; set; }
        public String EventMethod { get; set; }
        public Button(String text)
        {
            Text = text;
        }
        private Button() { }
        public override TagBuilder Render()
        {
            TagBuilder button = new TagBuilder("button");
            button.AddCssClass("btn");
            button.setStyles(Styles, Col, ConfigurableStyle);

            button.InnerHtml = Text;
            if (Icon != null)
            {
                button.InnerHtml += Icon.Render();
            }
            if (EventType != null)
            {
                button.MergeAttribute("on" + EventType, EventMethod);
            }
            return button;
        }
    }
}