using MES.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MES.Models
{
    #region 表格模型
    public class SearchModels: List<SearchModel>
    {
    }
    #endregion
    # region 表格字段模型
    public class FieldModel
    {
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

    #region 查询框模型
    public class SearchModel
    {
        //查询框类型
        public SearchType SearchType { get; set; }
        //查询框id
        public string id { get; set; }
        //参数类型
        public string PropertyType { get; set; }
        //查询框别名
        public string Alias { get; set; }
        //获取参数的Url
        public string ParamUrl { get; set; }
    }
    #endregion
  
}