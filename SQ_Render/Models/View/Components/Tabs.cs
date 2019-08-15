using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Tabs : AbstractElement
    {
        public override string TagName => "div";

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

        }
    }
}