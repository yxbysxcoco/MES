using SQ_DB_Framework.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Tree : AbstractElement
    {
        public List<TreeNode> Data { get; set; }
        public override string TagName => "div";
        public Tree(string id, List<TreeNode >data)
        {
            Id = id;
            Data = data;
        }

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("id", Id);
            var script = new TagBuilder("script");
            script.InnerHtml = $@"initApp(() => lemon.initTree('{Id}', JSON.parse('{Data.ToJSON()}')))";
            tag.InnerHtml += script;
        }
    }
}