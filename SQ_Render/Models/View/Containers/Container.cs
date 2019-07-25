using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Container : AbstractElement, IFinalTag
    {
        public List<AbstractElement> ChildElements { get; set; }

        public bool HasContainerStyle { get; set; } = false;

        public override TagBuilder Render()
        {
            TagBuilder htmlStr = new TagBuilder("div");
            foreach(var element in ChildElements)
            {
                htmlStr.InnerHtml += element.Render().ToString();
            }
            return htmlStr;
        }
        public MvcHtmlString FinalRender()
        {
            return new MvcHtmlString(this.Render().ToString());
        }
    }
}