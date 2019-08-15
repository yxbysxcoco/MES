using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class HiddenPanel : Container
    {
        public HiddenPanel(string formId)
        {
            Id = formId;
        }
        public override string TagName => "div";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("hidden", "");
            tag.MergeAttribute("name", "hiddenPanel");
            tag.MergeAttribute("id", Id + "hp");
            tag.MergeAttribute("formId", Id);
        }
    }
}