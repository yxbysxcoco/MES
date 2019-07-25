using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Sider : AbstractElement
    {
        public override TagBuilder InitTag(TagBuilder tag)
        {

            return new TagBuilder("div");
        }
    }
}