using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_Render.Const;
using SQ_Render.Models.View;
using SQ_Render.Models.View.Components;
using SQ_Render.Models.View.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQ_Render.Controllers
{
    public class SalesOrderController : Controller
    {
        DataTable _dataTable = new DataTable();
        IQueryable<SalesOrder> _entities;
        public SalesOrderController()
        {
            _entities = _dataTable.GetEntities<SalesOrder>();
        }
        public ActionResult OrderMaintenance()
        {

            _dataTable.BuildDataTable(_entities);
            var typeName = new TextInput("OrderCode", "订单编号");

            var material = new TextInput("MaterialId", "材料");
            var datePicker = new DatePicker("DeliverTime", "交付日期");
            var select = new Select("人员")
            {
                Id = "SalesPeople",
                Options = _entities.ToDictionary(so => so.SalesPersonId.ToString(), so => so.SalesPerson.Name.ToString())
            };

            var showBtn = new Button("展示/隐藏更多条件")
            {
                Id = "showBtn"
            };
            showBtn.AddEventMethod("click", "lemon.showHiddenPanel()");
            var button = new Button("查找");
            button.AddEventMethod("click", "lemon.fliterTable()");
            var resetBtn = new Button("重置");
            resetBtn.AddEventMethod("click", "lemon.resetForm()");

            var form = new Form("SearchForm");
            var formRow = new FormRow();
            var formRow1 = new FormRow();



            formRow.AddChildElement(typeName);
            formRow1.AddChildElement(material);
            formRow1.AddChildElement(datePicker);
            formRow1.AddChildElement(select);

            formRow.AddChildElement(showBtn);
            formRow.AddChildElement(button);
            formRow.AddChildElement(resetBtn);

            form.AddChildElement(formRow);
            form.AddChildElement(formRow1);

            var table = new Table("t1", _dataTable);
            var batchHandle = new TableHandle("batchOperation")
            {
                HandleItems = new List<HandleItem>()
                    {
                        new HandleItem(){
                            Alias = "批量删除",
                            Url = @"https://www.baidu.com",
                            EventName = "batchDel",
                            BtnColor = "danger"
                        },
                        new HandleItem(){
                            Alias = "打印",
                            EventName = "lemon.previewPrint(1)",
                            BtnColor = "success"
                        }
                    },
            };
            var tableHandle = new TableHandle("h")
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

            div.AddChildElement(form);
            div.AddChildElement(tableHandle);
            div.AddChildElement(batchHandle);
            div.AddChildElement(table);

            return View(div);
        }
        public ActionResult AddSalesOrder()
        {
            var card = new CardWithContext();
            var form = new Form("addSales");

            var formRow = new FormRow();
            var orderIdInput = new TextInput("orderId", "订单编号") {
                Rules = Rules.NotNull
            };
            var deliverDate = new DatePicker("deliverDate", "交付日期")
            {
                IsRange = false
            };
            var nameInput = new TextInput("name", "订单名称");
            var salesPerson = new Select("销售人员")
            {
                Id = "salesPerson",
                Options = _entities.Select(so => so.SalesPerson).Distinct().ToDictionary(sp => sp.EmployeeId.ToString(), sp => sp.Name)
            };

            var addrInput = new TextInput("addr", "收货地址");
            var departmentSelect = new Select("负责部门")
            {
                Id = "department",
                Options = _entities.Select(so => so.Department).Distinct().ToDictionary(de => de.Id.ToString(), de => de.Name)
            };
            var tree = new TableSelectorTree<Department>("t1", "table_test", GetTreeTest(), dep => dep.Name)
            {
                Col = new Col(Position.zero, Position.quarter)
            };
            var formBox = new Container()
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };
            formRow.AddChildElement(orderIdInput)
                .AddChildElement(deliverDate)
                .AddChildElement(nameInput)
                .AddChildElement(salesPerson)
                .AddChildElement(addrInput)
                .AddChildElement(departmentSelect);
            form.AddChildElement(formRow);

            var selectMaterielBtn = new Button("新增物料");
            selectMaterielBtn.AddEventMethod("click", "selectMaterial()");

            var grid = new Grid();

            formBox.AddChildElement(form);
            grid.AddChildElement(tree).AddChildElement(formBox).AddChildElement(selectMaterielBtn);

            return View(grid);
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