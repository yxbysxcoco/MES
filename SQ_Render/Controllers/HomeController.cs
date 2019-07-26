using SQ_Render.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQ_Render.Const;
using SQ_Render.Models.View.Components;
using SQ_Render.Models.View.Containers;

namespace SQ_Render.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var button = new Button("按钮")
            {
                ConfigurableStyle = new ConfigurableStyle()
                {
                    Float = "left",
                }
            };


            return View();
        }
        public ActionResult Tem()
        {
            return View();
        }


        public ActionResult GatesTest()
        {
            var textInput = new TextInput("userName", "请输入姓名");
            var card = new Card()
            {
                Col = new Col(4, 4)
            };
            var grid = new Grid()
            {
                HasContainerStyle = true
            };
            var form = new Form("f1");
            var fb = new FormButton("/Home/Index")
            {
                Text = "提交"
            };
            form.AddChildElement(textInput);
            form.AddChildElement(fb);


            card.AddChildElement(form);
            grid.AddChildElement(card);

            return View(grid);
        }
    }
}