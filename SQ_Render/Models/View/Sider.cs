using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQ_Render.Controllers;

namespace SQ_Render.Models.View.Components
{
    public class Sider : AbstractElement
    {
        public Dictionary<string, Tuple<string, string>> Lis { get; set; }
        public IFrame iframe { get; set; }
        public override string TagName => "ul";
        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            UrlHelper url = new UrlHelper();

            base.InitTag(htmlHelper, tag);
            foreach(var kv in Lis)
            {
                //string link = url.Action(kv.Value.Item1, kv.Value.Item2);
                //string link = url.Action("Tem", "Home");
                string link = $"/{kv.Value.Item2}/{kv.Value.Item1}";
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                li.AddCssClass("red lighten-3 white-text");

                a.InnerHtml = kv.Key;
                a.MergeAttribute("href", link);
                a.MergeAttribute("style", "height: 65px;");
                a.AddCssClass("class='waves-effect'");

                li.InnerHtml = a.ToString();
                tag.InnerHtml += li;
            }
            if(InnerChildElements == null)
            {
                InnerChildElements = new List<AbstractElement>();
            }
            InnerChildElements.Add(new IFrame(@"$('.sidenav').sidenav();"));

            tag.MergeAttribute("id", "side-out");
            tag.AddCssClass("sidenav sidenav-fixed");
            //tag.AddCssClass("sidenav");
        }

    }
}