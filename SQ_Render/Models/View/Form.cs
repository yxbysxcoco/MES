using SQ_Render.Models.View.InputBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Form : AbstractElement
    {
        public string Action { get; }
        public SubmitInputBox SubmitInputBox { get; }
        public List<AbstractInputBox> InputBoxes { get; set; }

        public Form(string action, SubmitInputBox submitInput)
        {
            Action = action;
            SubmitInputBox = submitInput;
        }
        public override MvcHtmlString Render()
        {
            var tb = new TagBuilder("form");
            tb.MergeAttribute("action", Action);

            foreach(var inputBox in InputBoxes)
            {
                tb.InnerHtml += inputBox.Render();
            }
            tb.InnerHtml += SubmitInputBox.Render();

            return new MvcHtmlString(tb.ToString());
        }
    }
}