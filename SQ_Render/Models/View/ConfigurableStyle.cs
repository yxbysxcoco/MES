using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class ConfigurableStyle
    {
        public string Center { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int MinHeight { get; set; }
        public int MinWidth { get; set; }
        public int MarginTop { get; set; }
        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginBottom { get; set; }
        public int PaddingTop { get; set; }
        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }
        public int PaddingBottom { get; set; }
        public string Float { get; set; }
    }
}