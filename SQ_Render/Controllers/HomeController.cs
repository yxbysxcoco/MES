using SQ_Render.Models.View;
using SQ_Render.Models.View.InputBox;
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
            ViewBag.textInputBox = new TextInputBox("输入", "text", true);
            var form = new Form("", new SubmitInputBox("提交"))
            {
                InputBoxes = new List<AbstractInputBox>()
                {
                    new TextInputBox("用户名", "userName", true),
                    new PasswordInputBox("密码", "password", true)
                }
            };
            ViewBag.Form = form;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}