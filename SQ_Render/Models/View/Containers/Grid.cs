using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Grid : Container
    {
        public Grid():base("div")
        {
        }
        public override TagBuilder InitTag(TagBuilder row)
        {
            base.InitTag(row);
            row.AddCssClass("row");

            return row;
        }
    }
}