using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Card : Container
    {
        public override string TagName => "div";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            if (Styles == null) Styles = new List<string>();
            Styles.Add(Style.CardPadding);
            base.InitTag(htmlHelper, tag);

            tag.AddCssClass("layui-card");
        }
    }
}