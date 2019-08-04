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
        private const string operation = "ToolOperation";
        private const string operationName = "操作";
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
            Dictionary<string, string> entityInfoDic = new Dictionary<string, string>();
            var sQDbSet = new SQDbSet<ToolEquipment>();

            //var entities = sQDbSet.GetEntitiesByContion(entityInfoDic);
            var pageHelper = sQDbSet.GetEntitiesByCondition(1, 10, entityInfoDic, "");
            
            DataTable dataTable = new DataTable();

            
            //dataTable.BuildRepalceDataTable(pageHelper.AllList, t =>t.Name ,t => t.Weight, t => DataTable.Repalce(t.TypeId,t.ToolEquipmentType.Name),t=>DataTable.Repalce(t.MoneyUnitId,t.MoneyUnit.Name));


            dataTable.SetColumn<ToolEquipment>(t => DataTable.Multistage(t.Code, 2), t => DataTable.Multistage(t.Name, 2, "1"),
                t => DataTable.NewOperation(operation, operationName, 2));
            dataTable.SetColumn<ToolEquipment>(t => t.Weight, t => t.Mark);
            dataTable.SetRow(pageHelper.AllList, t => t.Code, t => t.Weight, t => t.Mark);

            dataTable.PageIndex = 1;
            dataTable.PageSize = 10;
            dataTable.TotalCount = pageHelper.TotalCount;
            dataTable.Limits = new int[3] { 10, 15, 20 };
            dataTable.TableName = "工装表";

            var table = new Table("t1", dataTable) {
                tableHandle = new TableHandle()
                {
                    HandleItems = new List<HandleItem>()
                    {
                        new HandleItem(){
                            Alias="编辑",
                            Url = @"https://www.baidu.com",
                            EventName ="www"
                        }
                    },
                    Id= operation,
                    
                }
            };

            return View(table);
        }
    }
}