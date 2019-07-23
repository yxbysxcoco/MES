using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MES.Infrastructure
{
    public static class Input
    {
        /// <param name="type">输入框类型[area, password, text]</param>
        /// <param name="placeholder">输入框显示的默认值</param>
        /// <param name="id">输入框id</param>
        /// <param name="required">输入框是否要求必填只有一个值required</param>
        /// <param name="style">添加自定义样式</param>
        public static MvcHtmlString IInput(this HtmlHelper html, string type, string placeholder, string id, string required, string style)
        {
            string input = String.Format("<input type='{0}'  placeholder = '{1}' id='{2}' required lay-verify='{3}' class='layui-input' style='{4}'>", type, placeholder, id, required, style);
            return new MvcHtmlString(input);
        }
        public static MvcHtmlString IInput(this HtmlHelper html, string type, string placeholder, string id, string required)
        {
            string input = String.Format("<input type='{0}'  placeholder = '{1}' id='{2}' required lay-verify='{3}' class='layui-input'>", type, placeholder, id, required);
            return new MvcHtmlString(input);
        }
        public static MvcHtmlString IInput(this HtmlHelper html, string type, string placeholder, string id)
        {
            string input = String.Format("<input type='{0}'  placeholder = '{1}' id='{2}' class='layui-input'>", type, placeholder, id);
            return new MvcHtmlString(input);
        }
        public static MvcHtmlString IInput(this HtmlHelper html, string type, string placeholder)
        {
            string input = String.Format("<input type='{0}'  placeholder = '{1}' class='layui-input'>", type, placeholder);
            return new MvcHtmlString(input);
        }
    }
}