using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Collection : AbstractElement
    {
        public override string TagName => "collection";

        public Dictionary<String, String> Lis { get; set; }
        public override void InitTag(HtmlHelper htmlHelper)
        {
            base.InitTag(htmlHelper);

            tag.AddCssClass("collection");

            foreach(var key in Lis.Keys)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("collection-item");
                li.MergeAttribute("href", Lis[key]);

                tag.InnerHtml += li.ToString();
            }
        }
    }
}