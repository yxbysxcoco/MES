using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Card : AbstractElement
    {
        public override MvcHtmlString Render()
        {
            TagBuilder card = new TagBuilder("div");
            card.AddCssClass("card blue-grey darken-1");

            TagBuilder context = new TagBuilder("div");
            context.AddCssClass("card-content white-text");

            card.InnerHtml = context.ToString();

            return new MvcHtmlString(card.ToString());
        }
    }
}