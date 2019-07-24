﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Row : AbstractElement
    {
        public Boolean IsContainer { get; set; } = false;
        public override MvcHtmlString Render()
        {
            TagBuilder row = new TagBuilder("div");
            if(IsContainer){ row.AddCssClass("container"); }
            row.AddCssClass("row");
            return new MvcHtmlString(row.ToString());
        }
    }
}