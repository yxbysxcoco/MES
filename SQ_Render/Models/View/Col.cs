using SQ_Render.Models.View.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQ_Render.Models.View
{
    public class Col
    {
        public Position Offset { get; set; }
        public Position Span { get; set; }

        public Col(Position offset = Position.oneTwelfth, Position span = Position.elevenTwelfths)
        {
            Offset = offset;
            Span = span;
        }
    }
}