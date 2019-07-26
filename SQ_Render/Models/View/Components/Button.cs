using SQ_Render.Const;
using SQ_Render.Models.View.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Button : AbstractElement
    {
        public String Text { get; set; }
        public Dictionary<string, string> EventsAndMethods { get; set; }

        public override string TagName => "button";

        public Button(String text = "Button")
        {
            Text = text;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.AddCssClass("btn");
            tag.InnerHtml = Text;

            if (EventsAndMethods!= null)
            {
                foreach (var eventMethod in EventsAndMethods)
                {
                    tag.MergeAttribute("on" + eventMethod.Key, eventMethod.Value);
                }
            }
        }
    }

    public class FormButton : Button
    {
        string Url { get; set; }
        public FormButton(string url)
        {
            Url = url;
        }

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            var formElement = FindFirstParent<Form>();
            if(formElement != null)
            {
                tag.MergeAttribute("form-submmit", formElement.Id);
            }
            if(EventsAndMethods == null)
            {
                tag.MergeAttribute("onclick", @"getData({method: 'POST', data: getFormData('" + formElement.Id + "'), url: '" + Url + "'})");
            }
        }
    }
}