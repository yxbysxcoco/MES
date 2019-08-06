using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQ_Render.Models.View.Containers
{
    public class Form : Container
    {
        public override string TagName => "form";
        public Form(string id)
        {
            Id = id;
        }
    }
}