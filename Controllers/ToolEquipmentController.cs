using MES.Table;
using MES.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MES.Controllers
{
    public class ToolEquipmentController : Controller
    {
        public int SaveData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            MyDataTable dataTables = new MyDataTable(new List<Column>(), new List<Row>(), new List<Row>());
            DBTable dBtable = new ToolEquipment();
            NewDataRows(dataTables);
            dBtable.SaveData(dataTables);
            //Debug.WriteLine("批量插入所占时间:" + sw.ElapsedMilliseconds.ToString());  sw.Start();
            return 0;
        }
        private static MyDataTable NewDataRows(MyDataTable dataTables)
        {
            for (int i = 0; i < 2; i++)
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
        public string GetData()
        {
            DBTable dBtable = new ToolEquipment();
            MyDataTable dataTables = new MyDataTable();
            dataTables=dBtable.GetAllData(dataTables);
            return Utils.ToJSON(dataTables);
        }
    }
}