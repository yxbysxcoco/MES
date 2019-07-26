using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class CardWithContext : Card
    {
        public override void AddChildElement(AbstractElement element)
        {
            if(childElements == null)
            {
                childElements = new List<AbstractElement>()
                {
                    new Context(this)
                };
            }
            childElements[0].AddChildElement(element);
        }
        public override void AddChildElements(IEnumerable<AbstractElement> elements)
        {
            foreach(var element in elements)
            {
                AddChildElement(element);
            }
        }
    }
}