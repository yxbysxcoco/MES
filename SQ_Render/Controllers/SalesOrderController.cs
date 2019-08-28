using Microsoft.EntityFrameworkCore;
using SQ_DB_Framework.DataModel;
using SQ_DB_Framework.Entities;
using SQ_Render.Const;
using SQ_Render.Models.View;
using SQ_Render.Models.View.Components;
using SQ_Render.Models.View.Containers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

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

            _dataTable.BuildRepalceDataTable(_entities, so => DataTable.Repalce(so.SalesPersonId, so.Employee.Name));
            var typeName = new TextInput("OrderCode", "订单编号");

            var material = new TextInput("MaterialId", "材料");
            var datePicker = new DatePicker("DeliverTime", "交付日期");
            var select = new Select("人员")
            {
                Id = "SalesPeople",
                Options = _entities.ToDictionary(so => so.SalesPersonId.ToString(), so => so.Employee.Name.ToString())
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
                Options = _entities.Select(so => so.Employee).Distinct().ToDictionary(sp => sp.EmployeeId.ToString(), sp => sp.Name)
            };

            var addrInput = new TextInput("addr", "收货地址");
            var departmentSelect = new Select("负责部门")
            {
                Id = "department",
                Options = _entities.Select(so => so.Department).Distinct().ToDictionary(de => de.Id.ToString(), de => de.Name)
            };

            formRow.AddChildElement(orderIdInput)
                .AddChildElement(deliverDate)
                .AddChildElement(nameInput)
                .AddChildElement(salesPerson)
                .AddChildElement(addrInput)
                .AddChildElement(departmentSelect);
            form.AddChildElement(formRow);

            var selectMaterielBtn = new Button("从产品目录中选择+");
            selectMaterielBtn.AddEventMethod("click", "selectMaterial('OrderMaterialMaps')");

            var grid = new Grid();

            var dataTable = new DataTable();
            dataTable.BuildRepalceDataTable<OrderMaterialMap>(null, 
                omm => DataTable.Repalce(omm.MaterialId, omm.Material.Name),
                omm => DataTable.Without(omm.OrderCode)
                );
            dataTable.Columns[0][1].Writable();
            dataTable.Columns[0][2].Writable();
            dataTable.Columns[0][3].Writable();
            dataTable.Columns[0][4].Writable();
            dataTable.Columns[0][5].Writable();
            dataTable.Columns[0][6].Writable();


            var table = new Table("OrderMaterialMaps", dataTable)
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };

            var submitBtn = new FormButton("提交", "https://localhost:44317/SalesOrder/AddSalesOrderImpl");
            grid.AddChildElement(form.AddChildElement(table).AddChildElement(submitBtn)).AddChildElement(selectMaterielBtn);

            return View(grid);
        }
        [HttpPost]
        public string AddSalesOrderImpl([FromBody]SalesOrder dic)
        {
            return "";
        }
        public ActionResult SelectSalesOrder()
        {

            DataTable dataTable = new DataTable();
            var entities = dataTable.GetEntities<SalesOrder>();
            dataTable.BuildRepalceDataTable(entities,
                 omm => DataTable.Repalce(omm.SalesPersonId, omm.Employee.Name),
                 omm => DataTable.Repalce(omm.CustomerId, omm.Customer.Name),
                  omm => DataTable.Repalce(omm.DepartmentId, omm.Department.Name)
                 );
            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            dataTable.PageIndex = 1;
            dataTable.PageSize = 10;
            dataTable.Limits = new int[3] { 10, 15, 20 };
            dataTable.TableName = "销售订单";

            var orderCode = new TextInput("SalesOrder_OrderCode", "订单编号");
            var name = new TextInput("SalesOrder_Name", "订单名称");
            var deliverTime = new DatePicker("SalesOrder_DeliverTime", "交付时间");
            var money = new TextInput("SalesOrder_Money", "订单金额");

            var select = new Select("销售")
            {
                Id = "SalesOrder_Customer",
                Options = dataTable.GetEntities<Employee>()
                .ToDictionary(mu => mu.EmployeeId.ToString(), mu => mu.Name.ToString())
            };

            var button = new SubmitBtn("SearchForm");
            var resetBtn = new Button("重置");
            resetBtn.AddEventMethod("click", "lemon.resetForm('SearchForm')");

            var form = new TableForm("SearchForm", "t1");
            
            
            

            var formRow = new FormRow();
            formRow.AddChildElement(orderCode).AddChildElement(button).AddChildElement(resetBtn);

            var formRow1 = new FormRow();
            formRow1.AddChildElement(name).
                AddChildElement(deliverTime).
                AddChildElement(money).
                AddChildElement(select);

            var hiddenPanel = new HiddenPanel("SearchForm");
            hiddenPanel.AddChildElement(formRow1);

            form.AddChildElement(formRow).AddChildElement(hiddenPanel);

            var table = new Table("t1", dataTable)
            {
                Col = new Col(Position.zero, Position.threeFourths)
            };

            var card = new Card();
            var context = new Context();
            context.AddChildElement(table);
            card.AddChildElement(context);
            card.Col = new Col(Position.quarter, Position.threeFourths);

            var grid = new Grid();
            var submitBtn = new Button("批量增加");
            submitBtn.AddEventMethod("click", "saveParamAndCloseLayer()");

            grid.AddChildElement(card);

            var div = new Container();

            div.AddChildElement(form).AddChildElement(submitBtn).AddChildElement(grid);

            return View(div);
        }

    }
}