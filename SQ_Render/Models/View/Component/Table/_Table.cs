using SQ_DB_Framework.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class _Table : AbstractElement
    {
        public List<List<Object>> Cols { get; set; }
        public string ToolBar { get; set; }
        public string FormId { get; set; }
        public DataTable DataTable { get; set; }
        public _Table(string id, DataTable dataTable)
        {
            Id = id;
            DataTable = dataTable;
        }
        public override string TagName => "table";
        public override void PrepareRender(HtmlHelper htmlHelper)
        {
            base.PrepareRender(htmlHelper);
            DataTable.ChangeIndexOfFixedColumns();

        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {

            base.InitTag(htmlHelper, tag);

            tag.AddCssClass("layui-hide");

            tag.MergeAttribute("id", Id);
            tag.MergeAttribute("lay-filter", "layui-" + Id);

            AddChildElement(new IFrame($@"initApp(() => lemon.initTable('{Id}', JSON.parse('{DataTable.ToJSON()}')))"));
        }
    }
}