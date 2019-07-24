using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public class Table<T> : AbstractElement
    {
        public Table(String id)
        {
            Id = id;
        }
        private Table() { }
        public IEnumerable<T> Thead { get; set; }
        public override MvcHtmlString Render()
        {
            TagBuilder table = new TagBuilder("table");
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
            return new MvcHtmlString(table.ToString());
        }
    }
}