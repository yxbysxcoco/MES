using SQ_Render.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Const
{
    public static class ExtendMethods
    {
        public static TagBuilder AddStyles(this TagBuilder tb, IEnumerable<string> styles)
        {
            foreach (var cssName in styles)
            {
                tb.AddCssClass(cssName);
            }
            return tb;
        }
        public static TagBuilder AddCol(this TagBuilder tb, Col col)
        {
            if (col != null)
            {
                tb.AddCssClass("col");
                tb.AddCssClass("s" + col.Span.ToString());
                tb.AddCssClass("offset-s" + col.Offset.ToString());
            }
            return tb;
        }
        public static TagBuilder AddConfigurableStyles(this TagBuilder tb, ConfigurableStyle configurableStyle)
        {
            return tb;
        }
    }
}