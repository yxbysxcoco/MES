using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Container : AbstractElement
    {
        public IEnumerable<AbstractElement> ChildElements { get; set; }

        public override TagBuilder Render()
        {
            TagBuilder htmlStr = new TagBuilder("div");
            foreach(var element in ChildElements)
            {
                htmlStr.InnerHtml += element.Render().ToString();
            }
            return htmlStr;
        }
    }
}