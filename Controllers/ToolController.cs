﻿using System;
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
        public List<Sider> menu
        {
            get { return new JsonTool("menu.json").GetValueList<Sider>("Menu").Where(m => m.ControllerName == "Tool").ToList(); }
        }
        public ActionResult Index()
        {
            return View(menu);
        }
        public ActionResult Template()
        {
            return View(menu);
        }
        public ActionResult Excel()
        {
            return View("",  menu);
        }
        public ActionResult SendMsg()
        {
            return View(menu);
        }
        public ActionResult ReceiveMsg()
        {
            return View(menu);
        }
        public ActionResult Tem()
        {
            return View(new ToolEquipment());
        }
        [ChildActionOnly]
        public ActionResult Test(Object obj)
        {
            return PartialView(obj);
        }
    }
}