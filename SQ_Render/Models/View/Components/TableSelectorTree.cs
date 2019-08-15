using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using SQ_DB_Framework.DataModel;

namespace SQ_Render.Models.View.Components
{
    public class TableSelectorTree<TEntity> : Tree
    {
        public string _tableId;
        public string _fieldName;
        public TableSelectorTree(string id, string tableId, List<TreeNode> nodes, Expression<Func<TEntity, object>> propExp) : base(id, nodes)
        {
            _tableId = tableId;
            var memberInfo = (propExp.Body as MemberExpression).Member;
            _fieldName = $"{memberInfo.ReflectedType.Name}_{memberInfo.Name}";
            OnlyIconControl = true;
            ClickMethod = "treeFilterTable";
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            var script = new TagBuilder("script");
            script.InnerHtml = $@"initApp(() => lemon.initTableSelectorTree(JSON.parse('{this.ToJSON()}')))";
            tag.InnerHtml = script.ToString();
        }
    }
}