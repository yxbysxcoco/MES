using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Card : Container
    {
        public override void InitTag(HtmlHelper htmlHelper)
        {
            base.InitTag(htmlHelper);

            tag.AddCssClass("card blue-grey darken-1");

            TagBuilder context = new TagBuilder("div");
            context.AddCssClass("card-content white-text");

            tag.InnerHtml += context;
        }
    }
}