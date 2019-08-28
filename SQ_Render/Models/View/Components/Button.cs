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
        protected Dictionary<string, string> EventsAndMethods { get; set; }

        public override string TagName => "button";

        public Button(String text = "Button")
        {
            Text = text;
        }
        public Button AddEventMethod(string frontEvent, string methodName)
        {
            if(EventsAndMethods == null)
            {
                EventsAndMethods = new Dictionary<string, string>();
            }
            EventsAndMethods.Add(frontEvent, methodName);
            return this;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.AddCssClass("layui-btn");
            tag.MergeAttribute("type", "button");
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
    public class SubmitBtn : Button
    {
        public SubmitBtn(string formId)
        {
            Id = formId;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.AddCssClass("layui-btn");
            tag.MergeAttribute("type", "button");
            tag.InnerHtml = htmlHelper.Encode("查找");
            //tag.MergeAttribute("onclick", "lemon.fliterTable()");
            tag.MergeAttribute("lay-filter", Id + "Btn");
            tag.MergeAttribute("lay-submit", "");

            AddChildElement(new IFrame("initApp(() => {layui.form.on('submit(" + Id + "Btn" + ")', function() {lemon.fliterTable('" + Id + "')})})"));
        }

    }
    public class FormButton : Button
    {
        string Url { get; set; }
        private AbstractElement _formElement;
        public FormButton(string name, string url):base(name)
        {
            Url = url;
        }
        public override void PrepareRender(HtmlHelper htmlHelper)
        {
            base.PrepareRender(htmlHelper);
            _formElement = FindFirstParent<Form>();

            if (_formElement != null)
            {
                AddEventMethod("click", @"lemon.pushData({method: 'POST', data: {dic: lemon.getFormData('" + _formElement.Id + "')}, url: '" + Url + "'})");
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