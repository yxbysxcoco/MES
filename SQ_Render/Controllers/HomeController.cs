using SQ_Render.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQ_Render.Const;
using SQ_Render.Models.View.Components;
using SQ_Render.Models.View.Containers;
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;

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
        public ActionResult Login()
        {
            var userName = new TextInput("UserName", "用户名");
            var passWord = new PasswordInput("PassWord","密码");
            var button = new FormButton("https://localhost:44317/User/Login");
            var form = new Form("LoginForm");
            var grid = new Grid()
            {
                HasContainerStyle = true
            }; 
            form.AddChildElement(userName);
            form.AddChildElement(passWord);
            form.AddChildElement(button);
            grid.AddChildElement(form);
            return View(form);
        }


        public ActionResult GatesTest()
        {
            var sider = new Sider() { Lis = new Dictionary<string, Tuple<string, string>>()};
            var textInput = new TextInput("userName", "请输入姓名");
            var card = new Card()
            {
                Col = new Col(Position.oneThird, Position.oneThird)
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
            grid.AddChildElement(sider);

            return View(grid);
        }

        public ActionResult Table()
        {
            var dbSet = new SQDbSet<ToolEquipment>();
            var entities = dbSet.GetAllEntities();

            var dataTable = new DataTable();

            dataTable.BuildReduceDataTable(entities, t => new { t.Code, t.RepairCycle },
                   l => l.Sum(t => t.Weight),
                   l => l.Average(t => t.Univalence),
                   l => l.Max(t => t.Edition));
            var table = new Table("t1", dataTable);

            return View(table);
        }
    }
}