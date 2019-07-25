﻿using SQ_Render.Models.View;
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
            var grid = new Grid()
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
                    new Grid()
                        {
                            ChildElements = new List<AbstractElement>
                            {
                                new Button("登录")
                                {
                                    Col = new Col()
                                    {
                                        Offset = 6
                                    },
                                    Styles = new List<String>{ Style.BtnClick },
                                    ConfigurableStyle = new ConfigurableStyle()
                                    {
                                        Left = -25
                                    },
                                    EventType = "click",
                                    EventMethod = "test()"
                                }
                            },
                        }
                    }
            };
            return View(grid);
        }
    }
}