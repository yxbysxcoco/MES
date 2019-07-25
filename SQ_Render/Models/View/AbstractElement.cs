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
        public bool IsHidden { get; set; }
        public List<String> Styles { get; set; } = new List<String>();
        public Col Col { get; set; }
        public abstract TagBuilder Render();
    }
}