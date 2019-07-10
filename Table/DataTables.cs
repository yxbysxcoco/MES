
using MES.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace MES.Table
{
    public class MyDataTable : List<Row>
    {
        //列集合
        public List<Column> Columns { get; set; }
        //不合法行集合
        public List<Row> ErrorDataList { get; set; }
        //合法行集合
        public List<Row> LegalDataList { get; set; }
        //合法的数据字段与数据集合
        public MyDataTable(List<Column> columns ,List<Row> errorDataList, List<Row> legalDataList)
        {
            Columns = columns;
            ErrorDataList = errorDataList;
            LegalDataList = legalDataList;
        }
        public MyDataTable(){

        }
    }
        
}
