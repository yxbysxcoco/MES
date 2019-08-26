using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Hr : AbstractElement
    {
       
        public override string TagName => "hr";

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            Color = Color.blue;
            tag.AddCssClass($"layui-bg-{Color.ToString()}");
        }
    }

   
}