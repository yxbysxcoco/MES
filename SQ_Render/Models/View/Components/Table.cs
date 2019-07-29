using SQ_DB_Framework.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Table : AbstractElement
    {
        public string Url { get; set; }
        public DataTable DataTable { get; set; }
        public Table(String id, DataTable dataTable)
        {
            Id = id;
            DataTable = dataTable;
        }
        public override string TagName => "table";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("id", Id);
            tag.AddCssClass("highlight");

            TagBuilder thead = new TagBuilder("thead");
            TagBuilder tr_head = new TagBuilder("tr");
            foreach (var head in DataTable.Columns)
            {
                TagBuilder th = new TagBuilder("th");
                th.InnerHtml = head.Alais;
                th.MergeAttribute("name", head.Name);
                tr_head.InnerHtml += th.ToString();
            }
            thead.InnerHtml = tr_head.ToString();
            TagBuilder tbody = new TagBuilder("tbody");
            foreach (var row in DataTable)
            {
                TagBuilder tr = new TagBuilder("tr");
                foreach(var body in row)
                {
                    TagBuilder td = new TagBuilder("td");
                    td.InnerHtml = body.ToString();
                    tr.InnerHtml += td.ToString();
                }
                tbody.InnerHtml += tr;
            }
        }
    }
}