﻿using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Button : AbstractElement
    {
        public String Text { get; set; }
        public Icon Icon { get; set; }
        public Button(String text)
        {
            Text = text;
        }
        private Button() { }
        public override TagBuilder Render()
        {
            TagBuilder button = new TagBuilder("button");
            button.AddStyles(Styles);
            button.InnerHtml = Text;

            if (Icon != null)
            {
                button.InnerHtml += Icon.Render();
            }
            return button;
        }
    }
}