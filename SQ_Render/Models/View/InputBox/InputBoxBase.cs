using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQ_Render.Models.View
{
    public class InputBoxBase : Element
    {
        public string Alias { get; set; }
        public bool IsRequired { get; set; }
        private InputBoxBase() { }
        public InputBoxBase(string alias, bool isRequired)
        {
            Alias = alias;
            IsRequired = isRequired;
        }
    }
}