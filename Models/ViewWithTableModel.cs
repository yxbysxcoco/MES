using MES.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MES.Models
{
    #region 表格模型
    public class ViewWithTableModel
    {
        //查询框集合
        public List<SearchModel>   SearchModels { get;}
        //表格对象
        public SchemaModel SchemaModel { get;}
    }
    #endregion
    # region 表格字段模型
    public class FieldModel
    {
        //字段别名
        public string Alias { get;}
        //长度
        public int Length { get;}
        //标识
        public string Identify { get;}
    }
    #endregion

    #region 表结构模型
    public class SchemaModel
    {
        //字段模型集合
        public List<FieldModel> FiledModels { get;}
    }
    #endregion

    #region 查询框模型
    public class SearchModel
    {
        //查询框类型
        public SearchType Type { get;}
        //参数名称
        public string ParamName { get;}
        //参数默认值
        public object DefaultValue { get;}
        //获取参数的Url
        public string GetParamUrl { get;}
    }
    #endregion
  
}