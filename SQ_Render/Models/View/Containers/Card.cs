using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Card : Container
    {
        public override TagBuilder InitTag(TagBuilder card)
        {
            card.AddCssClass("card blue-grey darken-1");

            TagBuilder context = new TagBuilder("div");
            context.AddCssClass("card-content white-text");

            card.InnerHtml += context;

            return card;
        }
    }
}