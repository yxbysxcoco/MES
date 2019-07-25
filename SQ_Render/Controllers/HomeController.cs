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
    }
}