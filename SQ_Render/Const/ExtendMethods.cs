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
                tb.AddCssClass("s" + (int)col.Span);
                tb.AddCssClass("offset-s" + (int)col.Offset);
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

                if(memberName == "Center" && (bool)value)
                {
                    str.Append("text-align: center;");
                    continue;
                } 

                if(memberName == "Float")
                {
                    str.Append("float: " + value + ";");
                    continue;
                }

                if(memberName == "MarginTop" || memberName == "MarginLeft" || memberName == "MarginRight" || memberName == "MarginBottom")
                {
                    memberName = memberName.ToLower().Insert(6, "-");
                    str.Append(memberName + ":" + value + "px;");
                    continue;
                }

                if (memberName == "PaddingTop" || memberName == "PaddingLeft" || memberName == "PaddingRight" || memberName == "PaddingBottom")
                {
                    memberName = memberName.ToLower().Insert(7, "-");
                    str.Append(memberName + ":" + value + "px;");
                    continue;
                }

                if (memberName == "MinHeight" || memberName == "MinWidth")
                {
                    memberName = memberName.ToLower().Insert(3, "-");
                    str.Append(memberName + ":" + value + "px;");
                    continue;
                }

                str.Append(memberName.ToLower() + ":" + value + "px;");
            }
            tb.MergeAttribute("style", str.ToString());
            return tb;
        }
        public static TagBuilder setStyles(this TagBuilder tb, IEnumerable<string> styles, Col col, ConfigurableStyle configurableStyle)
        {
            if (styles != null) tb.AddStyles(styles);
            if (col != null) tb.AddCol(col);
            if (configurableStyle != null) tb.AddConfigurableStyles(configurableStyle);

            return tb;
        }

        public static MvcHtmlString Render(this HtmlHelper html, AbstractElement model)
        {
            return model.Render(html);
        }
    }
}