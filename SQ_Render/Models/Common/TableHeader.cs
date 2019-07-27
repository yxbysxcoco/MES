using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQ_Render.Models.Common
{
    public class TableHeader : List<Field>
    {
        public string GetDataUrl { get; set; }
    }
}