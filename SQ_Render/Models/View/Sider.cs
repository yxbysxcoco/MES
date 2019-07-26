using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Components
{
    public class Sider : AbstractElement
    {
        public Dictionary<string, string> Lis { get; set; }
        public IFrame iframe { get; set; }
        public override string TagName => "ul";
        public override void InitTag(HtmlHelper htmlHelper)
        {
            base.InitTag(htmlHelper);
            foreach(var key in Lis.Keys)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.InnerHtml = Lis[key];
                a.AddCssClass("class='waves-effect'");
                li.InnerHtml = a.ToString();
                tag.InnerHtml += li;
            }
            if(ChildElements == null)
            {
                ChildElements = new List<AbstractElement>();
            }
            ChildElements.Add(new IFrame(@"$('.sidenav').sidenav();"));
            tag.MergeAttribute("id", "side-out");
            tag.AddCssClass("sidenav sidenav-fixed");
            //tag.AddCssClass("sidenav");
        }

    }
}