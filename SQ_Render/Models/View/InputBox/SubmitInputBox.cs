using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.InputBox
{
    public class SubmitInputBox : AbstractInputBox
    {
        public SubmitInputBox(string value):base(value, "", true) { }
        public override MvcHtmlString Render()
        {
            return new MvcHtmlString($"<input type = 'submit' value = '{Alias}'>\n");
        }
    }
}