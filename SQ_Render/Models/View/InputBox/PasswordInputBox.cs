﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.InputBox
{
    public class PasswordInputBox : AbstractInputBox
    {
        public PasswordInputBox(string aliaes, bool isRqeuired):base(aliaes, isRqeuired) {}
        public override MvcHtmlString Render()
        {
            return new MvcHtmlString("");
        }
    }
}