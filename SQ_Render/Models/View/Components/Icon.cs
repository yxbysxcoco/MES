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
        public Icon(String text) : base("")
        {
            Text = text;
        }
        public override TagBuilder InitTag(TagBuilder icon)
        {
            icon.AddCssClass("material-icons");
            if(Location != null) { icon.AddCssClass(Location); }
            icon.InnerHtml = Text;
            return icon;
        }
    }
}