using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MES.Controllers
{
    public class TemplateController : Controller
    {
        // GET: Template
        public ActionResult Button()
        {
            return View();
        }
        public ActionResult Input()
        {
            return View();
        }
        public ActionResult Tem()
        {
            return View(new ToolEquipment());
        }
    }
}