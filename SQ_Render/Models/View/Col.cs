using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQ_Render.Models.View
{
    public class Col
    {
        public int Offset { get; set; }
        public int Span { get; set; }

        public Col(int offset = 0, int span = 0)
        {
            Offset = offset;
            Span = span;
        }
    }
}