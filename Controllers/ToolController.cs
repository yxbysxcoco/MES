using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using MES.Models;
using SQ_DB_Framework.Entities;

namespace MES.Controllers
{
    public class ToolController : Controller
    {
        public List<Sider> Menu
        {
            get { return new JsonTool("menu.json").GetValueList<Sider>("Menu").Where(m => m.ControllerName == "Tool").ToList(); }
        }
        public ActionResult Index()
        {
            return View(Menu);
        }
        public ActionResult Excel()
        {
            return View("",  Menu);
        }
    }
}