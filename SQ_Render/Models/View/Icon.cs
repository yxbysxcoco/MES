using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Icon : AbstractElement
    {
        public String Text { get; set; }
        public String Location { get; set; }
        public Icon(String text)
        {
            Text = text;
        }
        private Icon() { }
        public override MvcHtmlString Render()
        {
            String icon = $"<i class='material-icons' class='{Location}' >{Text}</i>";
            return new MvcHtmlString(icon);
        }
    }
}