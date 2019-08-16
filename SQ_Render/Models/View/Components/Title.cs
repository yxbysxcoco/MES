using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Title : AbstractElement
    {
        public int TitleLevel { get; set; }
        public string TitleContent { get; set; }
        public override string TagName => $"h{TitleLevel}";

        public Title(int level, string content)
        {
            TitleLevel = level;
            TitleContent = content;
        }
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
            tag.InnerHtml += new TagBuilder("br");
            tag.InnerHtml = TitleContent;
        }
    }
}