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
        public override TagBuilder Render()
        {
            TagBuilder row = new TagBuilder("div");
            if (HasContainerStyle) { row.AddCssClass("container"); }
            row.AddCssClass("row");
            row.setStyles(Styles, null, ConfigurableStyle);
            foreach (var element in ChildElements)
            {
                row.InnerHtml += element.Render();
            }
            return row;
        }
    }
}