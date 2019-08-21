using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Models.View.Containers
{
    public class Div : AbstractElement
    {
        public override string TagName => "div";

        public override void InitTag(HtmlHelper htmlHelper, TagBuilder tag)
        {
            base.InitTag(htmlHelper, tag);
        }
        public Div()
        {
            ConfigurableStyle = new ConfigurableStyle();
        }
        public void Width(int width)
        {
            ConfigurableStyle.Width = width;
        }
        public void Height(int height)
        {
            ConfigurableStyle.Height = height;
        }
        public void MinWidth(int width)
        {
            ConfigurableStyle.MinWidth = width;
        }
        public void ClassName(string className)
        {

        }
    }
}