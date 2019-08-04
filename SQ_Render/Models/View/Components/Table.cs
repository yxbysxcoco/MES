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
        public int Current { get; set; } = 1;
        public DataTable DataTable { get; set; }
        public Table(String id, DataTable dataTable)
        {
            Id = id;
            DataTable = dataTable;
        }
        public override string TagName => "table";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            AddChildElement(new IFrame($"initTable('{Id}', '{DataTable.ToJSON()}')"));
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("id", Id);
            tag.MergeAttribute("lay-filter", "table");
            tag.AddCssClass("layui-hide");
        }
    }
}