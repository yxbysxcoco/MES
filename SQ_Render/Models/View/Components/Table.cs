using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Table<T> : AbstractElement
    {
        public Table( String id)
        {
            Id = id;
        }
        public override string TagName => "table";

        public IEnumerable<T> Thead { get; set; }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            TagBuilder thead = new TagBuilder("thead");
            TagBuilder tr = new TagBuilder("tr");
            foreach(T obj in Thead)
            {
                TagBuilder th = new TagBuilder("th");
                th.InnerHtml = "";
                tr.InnerHtml += th.ToString();
            }
            thead.InnerHtml = tr.ToString();
            tag.InnerHtml = thead.ToString();

            TagBuilder tbody = new TagBuilder("tbody");
            tbody.MergeAttribute("id", Id);

            tag.InnerHtml += tbody;
        }
    }
}