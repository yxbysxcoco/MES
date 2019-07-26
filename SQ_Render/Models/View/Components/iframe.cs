using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class IFrame : AbstractElement
    {
        public String Event { get; set; }
        public IFrame(String _event)
        {
            Event = _event;
        }
        public override string TagName => "iframe";

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.MergeAttribute("hidden", "");
            tag.MergeAttribute("onload", Event);
        }
    }
}