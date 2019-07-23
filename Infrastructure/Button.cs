using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MES.Infrastructure
{
    public static class Button
    {
        /// <param name="id">按钮id</param>
        /// <param name="uiType">UI类型[primary, normal, warm, danger, disabled]</param>
        /// <param name="method">事件触发的方法名，当传入事件类型时必传</param>
        /// <param name="eventType">事件类型[click, change, blur, focus]</param>
        /// <param name="style">传入样式</param>
        /// <param name="type">按钮类型[fluid, radius]</param> 
        /// <param name="slot">插槽[&#xe67b; ...]</param>
        /// <param name="slotText">插槽文本</param>
        /// <param name="text">按钮文本</param>
        /// <param name="size">按钮大小[xs, sm, lg]</param>
        /// <returns>返回一个按钮Element</returns>
        public static MvcHtmlString IButton(this HtmlHelper html, string uiType, string text, string size, string id, string method, string eventType, string style, string slot, string slotText, string type)
        {
            string button = String.Format("<button type='button' class='layui-btn layui-btn-{1} layui-btn-{7} layui-btn-{8}' id='{0}' on{2}='{3}' style='{4}'>" +
                "<i class='layui-icon'>{6}</i>{5}</button>", id, uiType, eventType, method, style, slotText, slot, type, size);
            return new MvcHtmlString(button);
        }
        public static MvcHtmlString IButton(this HtmlHelper html, string uiType, string text, string size, string id, string method, string eventType, string style, string slot, string slotText)
        {
            string button = String.Format("<button type='button' class='layui-btn layui-btn-{1} layui-btn-{7}' id='{0}' on{2}='{3}' style='{4}'>" +
                "<i class='layui-icon'>{6}</i>{5}</button>", id, uiType, eventType, method, style, slotText, slot, size);
            return new MvcHtmlString(button);
        }
        public static MvcHtmlString IButton(this HtmlHelper html, string uiType, string text, string size, string id, string method, string eventType, string style, string type)
        {
            string button = String.Format("<button type='button' class='layui-btn layui-btn-{1} layui-btn-{5} layui-btn-{7}' id='{0}' on{2}='{3}' style='{4}'>{6}</button>", id, uiType, eventType, method, style, type, text, size);
            return new MvcHtmlString(button);
        }
        public static MvcHtmlString IButton(this HtmlHelper html, string uiType, string text, string size, string id, string method, string eventType, string style)
        {
            string button = String.Format("<button type='button' class='layui-btn layui-btn-{1} layui-btn-{6}' id='{0}' on{2}='{3}' style='{4}'>{5}</button>", id, uiType, eventType, method, style, text, size);
            return new MvcHtmlString(button);
        }
        public static MvcHtmlString IButton(this HtmlHelper html, string uiType, string text, string size, string id, string method, string eventType)
        {
            string button = String.Format("<button type='button' class='layui-btn layui-btn-{1} layui-btn-{5}' id='{0}' on{2}='{3}'>{4}</button>", id, uiType, eventType, method, text, size);
            return new MvcHtmlString(button);
        }
        public static MvcHtmlString IButton(this HtmlHelper html, string uiType, string text, string size, string id)
        {
            string button = String.Format("<button type='button' class='layui-btn layui-btn-{1} layui-btn-{3}' id='{0}'>{2}</button>", id, uiType, text, size);
            return new MvcHtmlString(button);
        }
        public static MvcHtmlString IButton(this HtmlHelper html, string uiType, string text, string size)
        {
            string button = String.Format("<button type='button' class='layui-btn layui-btn-{0} layui-btn-{2}'>{1}</button>", uiType, text, size);
            return new MvcHtmlString(button);
        }
    }
}