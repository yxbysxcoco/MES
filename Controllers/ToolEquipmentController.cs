using MES.Config;
using MES.Table;
using MES.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using WindowsFormsApp1.Modle;
using WindowsFormsApp1.Table;

namespace MES.Controllers
{
    public class ToolEquipmentController : Controller
    {
        private readonly ToolEquipmentRepository toolEquipmentRepository = new ToolEquipmentRepository();
        private  const int pageSize = 10;
        public int SaveData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            MyDataTable dataTables = new MyDataTable(new List<Column>(), new List<Row>(), new List<Row>());
            DBTable dBtable = new ToolEquipment();
            NewDataRows(dataTables);
            dBtable.SaveData(dataTables);
            return 0;
        }
        private static MyDataTable NewDataRows(MyDataTable dataTables)
        {
            for (int i = 0; i < 100; i++)
            {
                Row row = new Row
                {
                    "TG06000" + i,
                    "1.0" ,
                    "刀具"+i,
                    "螺丝刀" + i,
                    "20*30"+i,
                    "钢"+i,
                    "2",
                    "ctoo",
                    "使用",
                    "个",
                    "元",
                    "6",
                    "10",
                    "15",
                    "20",
                    "库房"+i,
                    "制造商"+i,
                    DateTime.Now.ToString(),
                    "A001",
                    "检验单位"+i,
                    "4000",
                    "20",
                    "5",
                    "供应商"+i
                };
                dataTables.Add(row);
            }
            return dataTables;
        }
        public ActionResult Index(int? page)
        {
            DBTable dBtable = new ToolEquipment();
            IEnumerable<ToolEquipment> toolEquipments = dBtable.GetAllData<ToolEquipment>();
            PageHelper<ToolEquipment> pageHelper = new PageHelper<ToolEquipment>(toolEquipments, page - 1 ?? 0, pageSize);
            return Json(pageHelper);
        }
        public ActionResult GetDataByField(int? page, [FromBody] Models.ToolEquipment toolEquipmentParam)
        {
            var toolEquipmentDatas = toolEquipmentRepository.FindUpcomingDinners(toolEquipmentParam);
            var paginatedDinners = new PageHelper<Models.ToolEquipment> (toolEquipmentDatas, page-1 ?? 0, pageSize);
            return Json(paginatedDinners);
        }
    }
    
}