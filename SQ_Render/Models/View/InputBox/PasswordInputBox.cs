using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.InputBox
{
    public class PasswordInputBox : AbstractInputBox
    {
        public PasswordInputBox(string aliaes, string name, bool isRqeuired):base(aliaes, name, isRqeuired) {}
        public override MvcHtmlString Render()
        {
            var requiredAttr = IsRequired ? "required lay-verify='required'" : "";
            var idAttr = Id == null ? $"id='{Id}'" : "";

            return new MvcHtmlString($"<input type='{"password"}'  placeholder = '{Alias}' {idAttr} {requiredAttr} name = '{Name}' class='layui-input'>\n");
        }
    }
}