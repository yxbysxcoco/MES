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

        public override MvcHtmlString Render()
        {
            var htmlStr = new StringBuilder();
            foreach(var element in ChildElements)
            {
                htmlStr.Append(element.Render().ToString());
            }
            return new MvcHtmlString(htmlStr.ToString());
        }
    }
}