using SQ_Render.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            var tb1 = new TagBuilder("li");
            tb1.AddConfigurableStyles(new ConfigurableStyle(), con => con.Center, con => con.Height, con => con.Width);
            return tb;
        }
        
        public static TagBuilder AddConfigurableStyles(this TagBuilder tb, ConfigurableStyle configurableStyle, params Expression<Func<ConfigurableStyle, object>>[] expressions)
        {
            foreach(var expression in expressions)
            {
                var member = expression.Body as MemberExpression;
                string memberName = member.Member.Name;
                string value = expression.Compile().Invoke(configurableStyle).ToString();

                tb.MergeAttribute("style", "");
            }
            return tb;
        }
    }
}