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
            var html = new StringBuilder($"<form action=\"{Action}\">\n");
            foreach(var inputBox in InputBoxes)
            {
                html.Append(inputBox.Render().ToString());
            }
            html.Append(SubmitInputBox.Render().ToString());
            html.Append("</form>\n");

            return new MvcHtmlString(html.ToString());
        }
    }
}