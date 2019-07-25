using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Container : AbstractElement
    {
        public Container(string tagName = "div") : base(tagName)
        {
        }

        public List<AbstractElement> ChildElements { get; set; }

        public bool HasContainerStyle { get; set; } = false;

        public override TagBuilder InitTag(TagBuilder tag)
        {
            tag = base.InitTag(tag);
            if(HasContainerStyle)
            {
                tag.AddCssClass("container");
            }
            return tag;
        }
        public override TagBuilder BuildTag()
        {
            var tag = base.BuildTag();
            foreach (var childElement in ChildElements)
            {
                tag.InnerHtml += childElement.BuildTag();
            }

            return tag;
        }
    }
}