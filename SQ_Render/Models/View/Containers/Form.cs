using SQ_Render.Models.View.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Form : Container
    {
        public override string TagName => "form";
        public Form(string id)
        {
            Id = id;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.AddCssClass("layui-form");
            tag.MergeAttribute("id", Id);

            AddChildElement(new IFrame(@"initApp(() => lemon.form.push({id: '"+Id+"', isHidden: true, datePickerId: ''}))"));
        }
    }
}