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
        public abstract string TagName { get; }
        protected TagBuilder tag;

        public String Id { get; set; }
        public String Name { get; set; }
        public bool IsHidden { get; set; }

        public List<String> Styles { get; set; }
        public Col Col { get; set; }
        public ConfigurableStyle ConfigurableStyle { get; set; }

        public List<AbstractElement> ChildElements { get; set; }

        //定义包含子元素的组件时使用，避免放入ChildElements被覆盖。
        protected List<AbstractElement> InnerChildElements { get; set; }


        public virtual void InitTag(HtmlHelper htmlHelper)
        {
            if(Id != null)
            {
                tag.MergeAttribute("id", Id);
            }
            if(Name != null)
            {
                tag.MergeAttribute("name", Name);
            }
            if(IsHidden)
            {
                tag.MergeAttribute("hidden", "");
            }

            tag.setStyles(Styles, Col, ConfigurableStyle);
        }

        public TagBuilder BuildTag(HtmlHelper htmlHelper)
        {
            tag = new TagBuilder(TagName);
            InitTag(htmlHelper);

            if(InnerChildElements != null)
            {
                foreach (var childElement in InnerChildElements)
                {
                    tag.InnerHtml += childElement.BuildTag(htmlHelper);
                }
            }

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

        protected void AddCssClass(string cssClass)
        {
            tag.AddCssClass(cssClass);
        }
        protected void AddInnerHtml(TagBuilder addingTag)
        {
            tag.InnerHtml += addingTag;
        }
        protected void AddInnerHtml(string addingTag)
        {
            tag.InnerHtml += addingTag;
        }
        protected void SetInnerText(string text)
        {
            tag.SetInnerText(text);
        }


        public void MergeAttribute(string key, string value)
        {
            tag.MergeAttribute(key, value);
        }
        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            tag.MergeAttributes(attributes);
        }
    }
}