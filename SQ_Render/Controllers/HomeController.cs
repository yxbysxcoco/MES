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
            var form = new Form("form1");
            var textInput = new TextInput("userName", "请输入姓名");
            var select = new Select("一个下拉")
            {
                Options = new Dictionary<string, string>
                {
                    {"1", "1" }
                }
            };
            form.AddChildElement(textInput);
            form.AddChildElement(select);
            return View(form);
        }

        public ActionResult Table()
        {
            Dictionary<string, string> entityInfoDic = new Dictionary<string, string>();

            DataTable dataTable = new DataTable();
            var entities = dataTable.GetEntities<ToolEquipment>();
            /* dataTable.AddColumnsLayer<ToolEquipment>(
                 t => DataTable.Multistage(t.Code, 2), 
                 t => DataTable.Multistage(t.Name, 2, "1"),
                 t => DataTable.NewOperation(operation, operationName, 2));
             dataTable.AddColumnsLayer<ToolEquipment>(t => t.Weight, t => t.Mark);
             dataTable.AddRow(pageHelper.AllList, t => t.Code, t => t.Weight, t => t.Mark);*/
            dataTable.BuildRepalceDataTable(entities, t => DataTable.Repalce(t.TypeId, t.ToolEquipmentType.Name), t => DataTable.Repalce(t.MoneyUnitId, t.MoneyUnit.Name));

            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            dataTable.PageIndex = 1;
            dataTable.PageSize = 10;
            dataTable.Limits = new int[3] { 10, 15, 20 };
            dataTable.TableName = "工装表";

            var typeName = new TextInput("ToolEquipmentType_Name", "类型名称") {
                Rules = Rules.NotNull
            };
            var material = new TextInput("ToolEquipment_MaterialId", "材料");
            var datePicker = new DatePicker("ToolEquipment_DateAdded", "生产日期") {
                IsRange = true
            };
            var select = new Select("代号")
            {
                Id = "ToolEquipment_Mark",
                Options = new Dictionary<string, string>
                {
                    {"ctoo", "ctoo" },
                    {"YYYY", "YYYY" },
                    {"ABOO", "ABOO" },
                }
            };

            var button = new SubmitBtn("SearchForm");
            var resetBtn = new Button("重置");
            resetBtn.AddEventMethod("click", "lemon.resetForm('SearchForm')");

            var form = new TableForm("SearchForm", "t1");
            var formRow = new FormRow();
            var hiddenPanel = new HiddenPanel("SearchForm");
            var formRow1 = new FormRow();



            formRow.AddChildElement(typeName);

            formRow1.AddChildElement(material).AddChildElement(datePicker).AddChildElement(select);

            hiddenPanel.AddChildElement(formRow1);

            formRow.AddChildElement(button).AddChildElement(resetBtn);

            form.AddChildElement(formRow).AddChildElement(hiddenPanel);

            var table = new Table("t1", dataTable);

            var batchHandle = new BatchHandle("batchOperation")
            {
                HandleItems = new List<HandleItem>()
                    {
                        new HandleItem(){
                            Alias = "批量删除",
                            Url = @"https://www.baidu.com",
                            EventName = "batchDel",
                            BtnColor = "danger"
                        }
                    },
            };
            var tableHandle = new TableHandle(operation)
            {
                HandleItems = new List<HandleItem>()
                    {
                        new HandleItem(){
                            Alias = "编辑",
                            Url = @"https://www.baidu.com",
                            EventName = "handleEdit"
                        },
                        new HandleItem(){
                            Alias = "删除",
                            Url = @"https://www.baidu.com",
                            EventName = "handleDel",
                            BtnColor = "danger"
                        }
                    },
            };
            var div = new Container();

            var modal = new Modal("modalTest")
            {
                Text = "弹出层测试"
            };
            modal.AddChildElement(form);


            var grid = new Grid();
            var tree = new TableSelectorTree<Department>("t13213", "table_test", GetTreeTest(), dep => dep.Name)
            {
                Col = new Col(Position.zero, Position.quarter)
            };
            var card = new Card();
            card.Col = new Col(Position.zero, Position.threeFourths);
            var cardContext = new Context();
            var text = new Text("文字") {
                Size = 44,
                IsStrong = true
            };
            var hr = new Hr();
            cardContext.AddChildElement(text).AddChildElement(hr).AddChildElement(table);
            card.AddChildElement(cardContext);
            grid.AddChildElement(tree).AddChildElement(card);
            div.AddChildElement(form).AddChildElement(tableHandle).AddChildElement(batchHandle).AddChildElement(grid).AddChildElement(modal);

            return View(div);
        }
        public List<TreeNode> GetTreeTest()
        {
            var department = new Department()
            {
                Id = 0,
                Name = "公司",
                SubsidiaryDepartments = new List<Department>()
                {
                    new Department()
                    {
                        Id = 1,
                        Name = "研发部",
                        SubsidiaryDepartments = new List<Department>()
                        {
                            new Department()
                            {
                                Id = 2,
                                Name = "前端"
                            },
                            new Department()
                            {
                                Id = 3,
                                Name = "后端",
                                SubsidiaryDepartments = new List<Department>()
                                {
                                    new Department()
                                    {
                                        Id = 4,
                                        Name = "Web端"
                                    },
                                    new Department()
                                    {
                                        Id = 5,
                                        Name = "大数据"
                                    }
                                }
                            },
                            new Department()
                            {
                                Id = 6,
                                Name = "IOT"
                            }
                        }
                    },
                    new Department()
                    {
                        Id = 7,
                        Name = "财务部",
                        SubsidiaryDepartments = new List<Department>()
                        {
                            new Department()
                            {
                                Id = 8,
                                Name = "回款部门"
                            },
                            new Department()
                            {
                                Id = 9,
                                Name = "会计部门"
                            }
                        }
                    }

                }
            };
            return TreeNode.GetTreeList(new List<Department>() { department }, d => d.SubsidiaryDepartments, d => d.Name, d => d.Id.ToString());
        }
    }
}