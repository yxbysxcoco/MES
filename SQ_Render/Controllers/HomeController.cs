using SQ_Render.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var button = new Button("按钮");
            button.ConfigurableStyle = new ConfigurableStyle()
            {
                Float = "left",
            };
            

            return View();
        }
        public ActionResult Tem()
        {
            return View();
        }


        public ActionResult GatesTest()
        {
            var row = new Grid()
            {

                ChildElements = new List<AbstractElement>
                {
                    new Input("text", "t1", "账号"){
                        Col = new Col()
                        {
                            Offset = 4,
                            Span = 4
                        },
                    },
                    new Input("password", "p1", "密码"){
                        Col = new Col()
                        {
                            Offset = 4,
                            Span = 4
                        }
                    },
                    new Button("登录")
                    {
                        Col = new Col()
                        {
                            Offset = 4
                        }
                    }
                }
            };
            return View(row);
        }
    }
}