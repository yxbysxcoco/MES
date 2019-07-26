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
            var grid = new Grid()
            {
                Id = "grid",
                ConfigurableStyle = new ConfigurableStyle()
                {
                    MarginTop = 200
                },
                ChildElements = new List<AbstractElement>
                {
                    new Card()
                    {
                        Col = new Col(4, 4),
                        ConfigurableStyle = new ConfigurableStyle()
                        {
                            Height = 400,
                        },
                        ChildElements = new List<AbstractElement>
                        {
                            new Context()
                            {
                                ChildElements = new List<AbstractElement>
                                {
                                    new TextInput("test", "测试"),
                                }
                            }
                        }
                    },
                    new Sider()
                    {
                        Lis = new Dictionary<string, string>{{"#", "菜单1"}}
                    },
                    new TextInput("userName", "账号"){
                        Col = new Col(4, 4)
                    },
                    new PasswordInput("password", "密码"){
                        Col = new Col(4, 4),
                    },
                    new Grid()
                    {
                        ChildElements = new List<AbstractElement>
                        {
                            new Button("登录")
                            {
                                Col = new Col(6),
                                Styles = new List<String>{ Style.BtnClick },
                                ConfigurableStyle = new ConfigurableStyle()
                                {
                                    Left = -25
                                },
                                EventType = "click",
                                EventMethod = "test()"
                            }
                        },
                    },
                    new DatePicker()
                    {
                        Col = new Col(4, 4),
                    },
                    new Collection()
                    {
                        Lis = new Dictionary<string, string>{{"#", "1"}},
                        Col = new Col(4, 4)
                    }
                }
            };
            return View(grid);
        }
    }
}