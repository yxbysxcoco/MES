using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.InputBox
{
    public class TextInputBox : AbstractInputBox
    {
        public TextInputBox(string alias, string name, bool isRequired) : base(alias, name, isRequired) { }
        public override MvcHtmlString Render()
        {
            var requiredAttr = IsRequired ? "required lay-verify='required'" : "";
            var idAttr = Id == null ? $"id='{Id}'" : "";

            return new MvcHtmlString($"<input type='{"text"}'  placeholder = '{Alias}' {idAttr} {requiredAttr} name = '{Name}' class='layui-input'>\n");
        }
    }
}