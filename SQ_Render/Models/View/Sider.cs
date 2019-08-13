using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQ_Render.Controllers;

namespace SQ_Render.Models.View.Components
{
    public class Sider : AbstractElement
    {
        public Dictionary<string, Tuple<string, string>> Lis { get; set; }
        public override string TagName => "ul";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            UrlHelper url = new UrlHelper();

            base.InitTag(htmlHelper, tag);
        }

    }
}