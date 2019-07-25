using SQ_Render.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
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
            var properties = configurableStyle.GetType().GetProperties();
            StringBuilder str = new StringBuilder();
            foreach (var prop in properties)
            {
                string memberName = prop.Name;
                var value = prop.GetValue(configurableStyle);

                if(value == null)
                {
                    continue;
                }

                string val = value.ToString();
                if(memberName == "Center" && val == "true")
                {
                    str.Append("text-align: center;");
                    continue;
                } 

                if(memberName == "Float")
                {
                    str.Append("float: " + val + ";");
                    continue;
                }
                str.Append(memberName + ":" + val + "px;");
            }
            tb.MergeAttribute("style", str.ToString());
            return tb;
        }
    }
}