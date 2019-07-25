using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Collection : AbstractElement
    {
        public Dictionary<String, String> Lis { get; set; }
        public override TagBuilder Render()
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("collection");

            foreach(String key in Lis.Keys)
            {
                TagBuilder li = new TagBuilder("li");
                li.AddCssClass("collection-item");
                li.MergeAttribute("href", Lis[key]);

                ul.InnerHtml += li.ToString();
            }
            return ul;
        }
    }
}