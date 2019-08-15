using SQ_DB_Framework.DataModel;
using SQ_Render.Const;
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
        public List<TreeNode> Nodes { get; set; }
        public bool ShowCheckBox { get; set; }
        public List<TreeEditItem> TreeEditItems { get; set; }
        public bool Accordion { get; set; }
        public bool OnlyIconControl { get; set; }
        public bool IsJump { get; set; }
        public bool ShowLine { get; set; }
        public string DefaultNodeName { get; set; } = "未命名";
        public string None { get; set; } = "无数据";

        //事件触发时调用的方法，可直接定义obj => {},亦可使用函数变量
        //参数见layui文档 https://www.layui.com/doc/modules/tree.html
        public string ClickMethod { get; set; }
        public string OnCheckMethod { get; set; }
        public string OperateMethod { get; set; }

        public override string TagName => "div";
        public Tree(string id, List<TreeNode> nodes)
        {
            Id = id;
            Nodes = nodes;
        }

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.MergeAttribute("id", Id);
            var script = new TagBuilder("script");
            script.InnerHtml = $@"initApp(() => lemon.initTree(JSON.parse('{this.ToJSON()}')))";
            tag.InnerHtml += script;
        }
    }
}