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
        public DataTable DataTable { get; set; }
        public Table(string id, DataTable dataTable)
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
            tag.MergeAttribute("lay-filter", "layui-table");

            TagBuilder script = new TagBuilder("script");
            script.InnerHtml = @"initApp(() => {lemon.initTable('" + Id + "',JSON.parse('" + DataTable.ToJSON() + "'));})";
            tag.InnerHtml = script.ToString();
        }
    }
    public class TableHandle : AbstractElement
    {
        public List<HandleItem> HandleItems { get; set; }
        public override string TagName => "script";
        public TableHandle(string id)
        {
            Id = id;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.MergeAttribute("id", Id);
            tag.MergeAttribute("name", "tableHandle");
            tag.MergeAttribute("type", "text/html");

            foreach (HandleItem handleItem in HandleItems) { AddChildElement(handleItem); }
        }

    }
    public class HandleItem : AbstractElement
    {
        public string EventName { get; set; }
        public string Alias { get; set; }
        public string Url { get; set; }
        public string BtnColor { get; set; }
        public override string TagName => "a";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.InnerHtml = Alias;
            tag.AddCssClass("layui-btn layui-btn-xs");
            tag.AddCssClass($"layui-btn-{BtnColor}");
            tag.MergeAttribute("onclick", $@"{EventName}(this, '{Url}')");
        }
    }
}