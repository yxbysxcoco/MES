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
            var entities = dataTable.GetEntities<Material>();
            dataTable.BuildRepalceDataTable(entities, 
                m => DataTable.Repalce(m.MaterialTypeId, m.MaterialType.Name),
                m => DataTable.Repalce(m.MeterageUnitId, m.MeterageUnit.Name));
            dataTable.Columns[0][0].SetHasQRCode(true)
                .SetIsSortable(true);

            dataTable.PageIndex = 1;
            dataTable.PageSize = 10;
            dataTable.Limits = new int[3] { 10, 15, 20 };
            dataTable.TableName = "物料";
            var materialIdInput = new TextInput("Material_Id", "物料编码");
            var materialNameInput = new TextInput("Material_Name", "物料名称");
            var materialNameDatepicker = new DatePicker("materialNameDatepicker", "物料时间测试");
            var SpecificationsInput = new TextInput("Material_Specifications", "规格");

            var select = new Select("单位")
            {
                Id = "Material_MeterageUnit",
                Options = entities.Select(m => m.MeterageUnit).Distinct()
                .ToDictionary(mu => mu.MeterageUnitId.ToString(), mu => mu.Name.ToString())
            };

            var button = new SubmitBtn("SearchForm");
            var resetBtn = new Button("重置");
            resetBtn.AddEventMethod("click", "lemon.resetForm('SearchForm')");

            var form = new TableForm("SearchForm", "t1");
            var formRow = new FormRow();
            var hiddenPanel = new HiddenPanel("SearchForm");
            var formRow1 = new FormRow();


            formRow.AddChildElement(materialIdInput);

            formRow1.AddChildElement(SpecificationsInput).
                AddChildElement(materialNameInput).
                AddChildElement(materialNameDatepicker).
                AddChildElement(select);

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
            var materialTypeTreeData = TreeNode.GetTreeList(dataTable.GetEntities<MaterialType>(),
                mt => mt.ParentMaterialType,
                mt => mt.ChildrenMaterialTypes,
                mt => mt.Name,
                mt => mt.Id.ToString());
            var tree = new TableSelectorTree<MaterialType>("tree_table", "t1", materialTypeTreeData, mt => mt.Name)
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