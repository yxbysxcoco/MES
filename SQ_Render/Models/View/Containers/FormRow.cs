using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class FormRow : Container
    {
        public bool IsHiddenRow { get; set; } = false;
        public override string TagName => "div";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.AddCssClass("layui-form-item");
            if (IsHiddenRow)
            {
                tag.MergeAttribute("hidden", "");
                tag.MergeAttribute("name", "hiddenPanel");
            }
        }
    }
}