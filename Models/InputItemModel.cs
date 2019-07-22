using MES.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MES.Models
{
    public class InputItemModel
    {
        //查询框类型
        public SQInputType InputType { get; set; }
        //查询框id
        public string Id { get; set; }
        //参数类型
        public string PropertyType { get; set; }
        //查询框别名
        public string Alias { get; set; }
        //获取参数的Url
        public string ParamUrl { get; set; }

        public Dictionary<string, string> DataDictionary { get; set; }

        public bool IsRequired { get; set; }
        public InputItemModel()
        {
        }
    }
}