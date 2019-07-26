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
        private TagBuilder tag;

        public String Id { get; set; }
        public String Name { get; set; }
        public bool IsHidden { get; set; }

        protected AbstractElement ParentElement { get; set; }
        public List<String> Styles { get; set; }
        public Col Col { get; set; }
        public ConfigurableStyle ConfigurableStyle { get; set; }

        protected List<AbstractElement> childElements;


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
            if(IsHidden)
            {
                tag.MergeAttribute("hidden", "");
            }

            tag.setStyles(Styles, Col, ConfigurableStyle);
        }

        public TagBuilder BuildTag(HtmlHelper htmlHelper)
        {
            tag = new TagBuilder(TagName);
            InitTag(htmlHelper, tag);

            if(childElements != null)
            {
                foreach (var childElement in childElements)
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

        public void AddChildElement(AbstractElement element)
        {
            if (childElements == null)
            {
                childElements = new List<AbstractElement>();
            }

            childElements.Add(element);
            element.ParentElement = this;
        }
        public void AddChildElements(IEnumerable<AbstractElement> elements)
        {
            foreach(var element in elements)
            {
                AddChildElement(element);
            }
        }

        public void MergeAttribute(string key, string value)
        {
            tag.MergeAttribute(key, value);
        }
        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            tag.MergeAttributes(attributes);
        }

        protected AbstractElement FindFirstParent<TElement>() where TElement : AbstractElement
        {
            while(ParentElement != null)
            {
                if (ParentElement.GetType() == typeof(TElement))
                {
                    return ParentElement;
                }
                ParentElement = ParentElement.ParentElement;       
            }
            return null;
        }

    }
}