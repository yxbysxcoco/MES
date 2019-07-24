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
        public TextInputBox(string alias, bool isRequired) : base(alias, isRequired) { }
        public override MvcHtmlString Render()
        {
            var requiredAttr = IsRequired ? "required lay-verify='required'" : "";
            return new MvcHtmlString($"<input type='{"text"}'  placeholder = '{Alias}' id='{Id}' {requiredAttr} class='layui-input'>");
        }
    }
}