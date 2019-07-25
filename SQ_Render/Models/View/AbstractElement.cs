using SQ_Render.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public abstract class AbstractElement 
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public bool? IsHidden { get; set; }
        public List<String> Styles { get; set; } = new List<String>();
        public Col Col { get; set; }
        public ConfigurableStyle ConfigurableStyle { get; set; } = new ConfigurableStyle();
        public abstract string TagName { get;}
        public List<AbstractElement> ChildElements { get; set; }


        public virtual void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            if(Id != null)
            {
                tag.MergeAttribute("id", Id);
            }
            if(Name != null)
            {
                tag.MergeAttribute("name", Name);
            }
            if(IsHidden != null)
            {
                tag.MergeAttribute("hidden", "");
            }

            tag.setStyles(Styles, Col, ConfigurableStyle);
        }

        public virtual TagBuilder BuildTag(HtmlHelper htmlHelper)
        {
            var tag = new TagBuilder(TagName);
            InitTag(htmlHelper, tag);

            if(ChildElements != null)
            {
                foreach (var childElement in ChildElements)
                {
                    tag.InnerHtml += childElement.BuildTag(htmlHelper);
                }
            }
            return tag;
        }

        public MvcHtmlString Render(HtmlHelper html)
        {
            return new MvcHtmlString(BuildTag(html).ToString());
        }
    }
}