using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Table : AbstractElement
    {
        public String Url { get; set; }
        public Table(String id, String url)
        {
            Id = id;
            Url = url;
        }
        public override string TagName => "table";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("id", Id);
            AddChildElement(new IFrame(@"initTable('" + Id + "','" + Url + "')"));
        }
    }
}