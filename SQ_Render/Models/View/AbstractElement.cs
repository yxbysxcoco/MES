using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public abstract class AbstractElement 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public List<Stylus> Styles { get; set; }
        public SpanOffset Layout { get; set; }

        public AbstractElement()
        {
            Styles = new List<Stylus>();
        }

        public abstract MvcHtmlString Render();
    }
}