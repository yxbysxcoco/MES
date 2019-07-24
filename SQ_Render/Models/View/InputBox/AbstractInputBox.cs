using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public abstract class AbstractInputBox : AbstractElement
    {
        public string Alias { get; set; }
        public bool IsRequired { get; set; }

        private AbstractInputBox() { }
        public AbstractInputBox(string alias, bool isRequired)
        {
            Alias = alias;
            IsRequired = isRequired;
        }

    }
}