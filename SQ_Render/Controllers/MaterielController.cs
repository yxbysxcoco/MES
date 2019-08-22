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
    public class MaterielController : Controller
    {
        // GET: Materiel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SelectMateriel()
        {

            DataTable dataTable = new DataTable();
            var entities = dataTable.GetEntities<SalesOrder>();
            dataTable.BuildRepalceDataTable(entities, 
                so => DataTable.Repalce(so.SalesPersonId, so.SalesPerson.Name),
                so => DataTable.Repalce(so.DepartmentId, so.Department.Name),
                so => DataTable.Repalce(so.CustomerId, so.Customer.Name));
            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            dataTable.PageIndex = 1;
            dataTable.PageSize = 10;
            dataTable.Limits = new int[3] { 10, 15, 20 };
            dataTable.TableName = "订单";
            var orderNameInput = new TextInput("Order_Name", "订单名称");
            var customerNameInput = new TextInput("Customer_Name", "客户名称");
            var dateDeliverPicker = new DatePicker("Data_Deliver", "生产日期")
            {
                IsRange = true
            };
            var select = new Select("状态")
            {
                Id = "Order_Status",
                Options = entities.Select(so => so.Status).Distinct()
                .ToDictionary(st => st.ToString(), st => st.ToString())
            };

            var button = new SubmitBtn("SearchForm");
            var resetBtn = new Button("重置");
            resetBtn.AddEventMethod("click", "lemon.resetForm('SearchForm')");

            var form = new TableForm("SearchForm", "t1");
            var formRow = new FormRow();
            var hiddenPanel = new HiddenPanel("SearchForm");
            var formRow1 = new FormRow();


            formRow.AddChildElement(orderNameInput);

            formRow1.AddChildElement(customerNameInput).AddChildElement(dateDeliverPicker).AddChildElement(select);

            hiddenPanel.AddChildElement(formRow1);

            formRow.AddChildElement(button).AddChildElement(resetBtn);

            form.AddChildElement(formRow).AddChildElement(hiddenPanel);

            var table = new Table("t1", dataTable) {
                Col = new Col(Position.zero, Position.threeFourths)
            };

            var card = new Card();
            var context = new Context();
            context.AddChildElement(table);
            card.AddChildElement(context);
            card.Col = new Col(Position.quarter, Position.threeFourths);
            var departmentTreeData = TreeNode.GetTreeList(dataTable.GetEntities<Department>(),
                de => de.SuperiorDepartment,
                de => de.SubsidiaryDepartments,
                de => de.Name,
                de => de.Id.ToString());
            var tree = new TableSelectorTree<Department>("tree_table", "t1", departmentTreeData, dep => dep.Name)
            {
                Col = new Col(Position.zero, Position.quarter)
            };
            var grid = new Grid();
            var submitBtn = new Button("批量增加");
            submitBtn.AddEventMethod("click", "saveParamAndCloseLayer()");

            grid.AddChildElement(tree).AddChildElement(card);

            var div = new Container();

            div.AddChildElement(form).AddChildElement(submitBtn).AddChildElement(grid);

            return View(div);
        }
    }
}