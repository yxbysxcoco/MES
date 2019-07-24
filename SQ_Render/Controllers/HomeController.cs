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
            var button = new Button("这是一个按钮");
            var input = new Input("email", "myinput", "输入框");
            var datepicker = new DatePicker();
            ViewBag.button = button;
            ViewBag.input = input;
            ViewBag.datepicker = datepicker;
            return View();
        }
        public ActionResult Tem()
        {
            return View();
        }
    }
}