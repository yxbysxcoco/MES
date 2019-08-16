using SQ_Render.Models.View.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class TableForm : Form
    {
        public string TableId { get; set; } 
        public override string TagName => "form";
        public TableForm(string id, string tableId): base(id)
        {
            TableId = tableId;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.AddCssClass("layui-form");
            tag.MergeAttribute("id", Id);

            AddChildElement(new IFrame(@"initApp(() => {lemon.form.push({id: '" + Id + "', isHidden: true, datePickerId: ''}); lemon.bindTableIdToForm('"+ TableId + "', '" + Id + "') })"));
        }
    }
}