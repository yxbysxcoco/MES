﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Container : AbstractElement
    {
        public Container(AbstractElement parent = null) : base(parent) { }
        public override string TagName => "div";

        public bool HasContainerStyle { get; set; } = false;

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            if(HasContainerStyle)
            {
                tag.AddCssClass("container");
            }
        }
    }
}