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
            AddChildElement(new IFrame($""));
            base.InitTag(htmlHelper, tag);
            //tag.MergeAttribute("id", Id);
            //tag.AddCssClass("highlight");

            //TagBuilder thead = new TagBuilder("thead");
            //TagBuilder tr_head = new TagBuilder("tr");
            //foreach (var head in DataTable.Columns)
            //{
            //    TagBuilder th = new TagBuilder("th");
            //    th.InnerHtml = head.Alais;
            //    th.MergeAttribute("name", head.Name);
            //    tr_head.InnerHtml += th.ToString();
            //}
            //thead.InnerHtml = tr_head.ToString();
            //TagBuilder tbody = new TagBuilder("tbody");
            //tbody.MergeAttribute("id", "tbody");
            //foreach (var row in DataTable)
            //{
            //    TagBuilder tr = new TagBuilder("tr");
            //    tr.MergeAttribute("id", Current.ToString());

            //    tr.MergeAttribute("hidden", "");
            //    Current = Current + 1;
            //    foreach(var body in row)
            //    {
            //        TagBuilder td = new TagBuilder("td");
            //        td.InnerHtml = body.ToString();
            //        tr.InnerHtml += td.ToString();
            //    }
            //    tbody.InnerHtml += tr;
            //}
            //AddChildElement(new PageNav());
            //tag.InnerHtml = thead.ToString();
            //tag.InnerHtml += tbody.ToString();
        }
    }
    public class PageNav: AbstractElement
    {
        public override string TagName => "ul";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.AddCssClass("pagination");
            tag.MergeAttribute("id", "page");
            TagBuilder toLeft = new TagBuilder("li");
            toLeft.InnerHtml = @"<a href='#!'><i class='material-icons'>chevron_left</i></a>";
            TagBuilder toRight = new TagBuilder("li");
            toRight.InnerHtml = @"<a href='#!'><i class='material-icons'>chevron_right</i></a>";
            tag.InnerHtml += toLeft;
            tag.InnerHtml += toRight;
        }
    }
}