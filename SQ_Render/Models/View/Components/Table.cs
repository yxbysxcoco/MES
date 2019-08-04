﻿using SQ_DB_Framework.DataModel;
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
        public TableHandle tableHandle { get; set; }
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
            if (tableHandle != null) AddChildElement(tableHandle);
            tag.MergeAttribute("id", Id);
            tag.MergeAttribute("lay-filter", "table");
            tag.AddCssClass("layui-hide");
        }
    }
    public class TableHandle: AbstractElement
    {
        public List<HandleItem> HandleItems { get; set; }
        public override string TagName => "script";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("id", Id);
            tag.MergeAttribute("type", "text/html");
            foreach(var handleItem in HandleItems)
            {
                AddChildElement(handleItem);
            }
        }

    }
    public class HandleItem: AbstractElement
    {
        public string EventName { get; set; }
        public string Alias { get; set; }
        public string Url { get; set; }
        public override string TagName => "a";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.AddCssClass("layui-btn layui-btn-default");
            tag.MergeAttribute("onclick", $"{EventName}(this, '{Url}')");
            tag.InnerHtml = Alias;
        }
    }
}