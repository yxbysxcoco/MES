using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQ_Render.Models.Common
{
    public class Field
    {
        //字段名
        public string   FiledName { get; set; }
        //字段别名
        public string Alias { get; set; }
        //长度
        public int Length { get; set; }
        //是否可排序
        public bool IsSort { get; set; }

    }
}