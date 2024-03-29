﻿

using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Element 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Stylus> Styluss { get; set; }
        public Layout Layout { get; set; }
        public IEnumerable<Element> ChildElements { get; set; }

        public virtual MvcHtmlString Render()
        {
            StringBuilder htmlCode = new StringBuilder();

            foreach (var item in ChildElements)
            {
                htmlCode.Append(item.Render());
            }
            return new MvcHtmlString(htmlCode.ToString());
        }
    }
}