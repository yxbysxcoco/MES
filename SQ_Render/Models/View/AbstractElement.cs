using SQ_DB_Framework.Entities;
using SQ_Render.Const;
using SQ_Render.Models.View.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View
{
    public abstract class AbstractElement 
    {
        public abstract string TagName { get; }
        private TagBuilder tag;
        public Color Color { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }

        public List<string> Styles { get; set; }
        public Col Col { get; set; }
        public ConfigurableStyle ConfigurableStyle { get; set; }
        //abstract string default_css { get; }
        protected List<AbstractElement> childElements;
        protected AbstractElement ParentElement { get; set; }

        public AbstractElement(AbstractElement parent = null)
        {
            ParentElement = parent;
        }
        public void SetId<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : EntityBase
        {
            var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
            Id = member.Name;
        }
        public AbstractElement AddStyleClass(string cssClass)
        {
            Styles.Add(cssClass);
            return this;
        }

        public virtual void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
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
        public virtual void PrepareRender(HtmlHelper htmlHelper)
        {
        }
        public void PrepareRenderAll(HtmlHelper htmlHelper)
        {
            PrepareRender(htmlHelper);

            if (childElements != null)
            {
                foreach (var childElement in childElements)
                {
                    childElement.PrepareRenderAll(htmlHelper);
                }
            }
        }

        public MvcHtmlString Render(HtmlHelper html)
        {
            PrepareRenderAll(html);
            return new MvcHtmlString(BuildTag(html).ToString());
        }

        public virtual AbstractElement AddChildElement(AbstractElement element)
        {
            if (childElements == null)
            {
                childElements = new List<AbstractElement>();
            }

            childElements.Add(element);
            element.ParentElement = this;

            return this;
        }
        public virtual AbstractElement AddChildElements(IEnumerable<AbstractElement> elements)
        {
            foreach(var element in elements)
            {
                AddChildElement(element);
            }
            return this;
        }

        public void MergeAttribute(string key, string value)
        {
            tag.MergeAttribute(key, value);
        }
        public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
        {
            tag.MergeAttributes(attributes);
        }

        protected TElement FindFirstParent<TElement>() where TElement : AbstractElement
        {
            return (TElement)FindFirstParent(typeof(TElement));
        }
        protected AbstractElement FindFirstParent(Type type)
        {
            var parent = ParentElement;
            while (parent != null)
            {
                if (parent.GetType() == type)
                {
                    return parent;
                }
                parent = parent.ParentElement;
            }
            return null;
        }


    }
    public enum Color
    {
        red,
        orange,
        green,
        cyan,
        blue,
        black,
        gray,

    }
}