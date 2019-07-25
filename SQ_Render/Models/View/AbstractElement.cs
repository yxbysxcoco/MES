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
        protected AbstractElement(string tagName = "div", string id = null)
        {
            TagName = tagName;
            Id = id;
        }

        public String Id { get; set; }
        public String Name { get; set; }
        public bool? IsHidden { get; set; }
        public List<String> Styles { get; set; } = new List<String>();
        public Col Col { get; set; }
        public ConfigurableStyle ConfigurableStyle { get; set; } = new ConfigurableStyle();
        public string TagName { get; set; }

        public virtual TagBuilder InitTag(TagBuilder tag)
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
            return tag.setStyles(Styles, Col, ConfigurableStyle);
        }

        public virtual TagBuilder BuildTag()
        {
            var tag = new TagBuilder(TagName);
            tag = InitTag(tag);
            return tag;
        }

        public MvcHtmlString Render()
        {
            return new MvcHtmlString(BuildTag().ToString());
        }
    }
}