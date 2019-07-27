using MES.Const;
using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MES.Models
{
    # region 表格字段模型
    public class FieldModel
    {
        //字段名
        public string name { get; set; }
        //字段别名
        public string Alias { get; set; }
        //长度
        public int Length { get; set; }  
        
    }
    #endregion

    #region 表结构模型
    public class SchemaModel: List<FieldModel>
    {
        //字段模型集合
       public string RequestUrl { get; set; }

    }
    #endregion

}