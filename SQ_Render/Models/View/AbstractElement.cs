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
        public String Id { get; set; }
        public String Name { get; set; }
        public Boolean IsHidden { get; set; }
        public List<Style> Stylus { get; set; } = new List<Style>();
        public Col Col { get; set; }
        public abstract TagBuilder Render();
    }
}