﻿using SQ_Render.Const;
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
        protected Dictionary<string, string> EventsAndMethods { get; set; }

        public override string TagName => "button";

        public Button(String text = "Button")
        {
            Text = text;
        }
        public void AddEventMethod(string frontEvent, string methodName)
        {
            if(EventsAndMethods == null)
            {
                EventsAndMethods = new Dictionary<string, string>();
            }
            EventsAndMethods.Add(frontEvent, methodName);
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.AddCssClass("btn");
            tag.InnerHtml = htmlHelper.Encode(Text);

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
        private AbstractElement _formElement;
        public FormButton(string url)
        {
            Url = url;
        }
        public override void PrepareRender(HtmlHelper htmlHelper)
        {
            base.PrepareRender(htmlHelper);
            _formElement = FindFirstParent<Form>();

            if (_formElement != null)
            {
                AddEventMethod("onclick", @"getData({method: 'POST', data: getFormData(" + _formElement.Id + "), url: '" + Url + "'})");
            }
        }

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            if (_formElement != null)
            {
                tag.MergeAttribute("form-submmit", _formElement.Id);
            }

        }
    }
}