using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Icon : AbstractElement
    {
        public String Text { get; set; }
        public String Location { get; set; }
        public Icon(String text)
        {
            Text = text;
        }
        public override string TagName => "i";

        public override void InitTag(HtmlHelper htmlHelper)
        {
            base.InitTag(htmlHelper);

            tag.AddCssClass("material-icons");
            if(Location != null) { tag.AddCssClass(Location); }
            tag.InnerHtml = Text;
        }
    }
}