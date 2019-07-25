using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Table<T> : AbstractElement
    {
        public Table( String id): base("table")
        {
            Id = id;
        }
        public IEnumerable<T> Thead { get; set; }
        public override TagBuilder InitTag(TagBuilder table)
        {
            base.InitTag(table);
            TagBuilder thead = new TagBuilder("thead");
            TagBuilder tr = new TagBuilder("tr");
            foreach(T obj in Thead)
            {
                TagBuilder th = new TagBuilder("th");
                th.InnerHtml = "";
                tr.InnerHtml += th.ToString();
            }
            thead.InnerHtml = tr.ToString();
            table.InnerHtml = thead.ToString();

            TagBuilder tbody = new TagBuilder("tbody");
            tbody.MergeAttribute("id", Id);

            table.InnerHtml += tbody;
            return table;
        }
    }
}