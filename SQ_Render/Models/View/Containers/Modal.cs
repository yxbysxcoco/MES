using SQ_Render.Models.View.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Modal : Container
    {
        public string TableId { get; set; }
        public override string TagName => "div";
        public Modal(string id)
        {
            Id = id;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("id", Id);
            tag.MergeAttribute("name", "modal");
        }
    }
}