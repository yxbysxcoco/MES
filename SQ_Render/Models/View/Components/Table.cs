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
        public Table(String id, DataTable dataTable)
        {
            Id = id;
            DataTable = dataTable;
        }
        public override string TagName => "table";

        public override void PrepareRender(HtmlHelper htmlHelper)
        {
            DataTable.Columns[0] = ChangeIndexOfFixedColumns(DataTable.Columns[0]);
        }

        public List<Column> ChangeIndexOfFixedColumns(List<Column> columns)
        {
            int lastFixedLeftPoint = 0;

            for (int i = 0; i < columns.Count; i++)
            {
                if (!(columns[i].Fixed?.Equals("left") ?? false))
                    continue;

                for (int j = lastFixedLeftPoint; j < columns.Count; j++)
                {
                    if (!(columns[j].Fixed?.Equals("left") ?? false))
                    {
                        columns.Insert(j, columns[i]);
                        columns.RemoveAt(i + 1);
                        lastFixedLeftPoint = j;
                    }
                }

            }

            int lastFixedRightPoint = columns.Count - 1;

            for (int i = columns.Count - 1; i >= 0; i--)
            {
                if (!(columns[i].Fixed?.Equals("right") ?? false))
                    continue;
                for (int j = lastFixedRightPoint; j > lastFixedLeftPoint; j--)
                {
                    if (!(columns[j].Fixed?.Equals("right") ?? false))
                    {
                        columns.Insert(j + 1, columns[i]);
                        columns.RemoveAt(i);
                        lastFixedRightPoint = j;
                        break;
                    }
                }
            }
            return columns;
        }

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            AddChildElement(new IFrame($"initTable('{Id}', '{DataTable.ToJSON()}')"));

            base.InitTag(htmlHelper, tag);

            tag.AddCssClass("layui-hide");

            tag.MergeAttribute("id", Id);
            tag.MergeAttribute("lay-filter", "table");
        }
    }
    public class TableHandle : AbstractElement
    {
        public List<HandleItem> HandleItems { get; set; }
        public override string TagName => "script";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);

            tag.MergeAttribute("id", Id);
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