using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    public class Column
    {
        public Column() { }
        public Column(MemberInfo member)
        {
            Alais = (member.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name;
            Name = member.Name;
            Width = member.Width();
            Type = member.GetColumnType();
            IsSortable = member.IsDefined(typeof(SortableAttribute), false);
        }
        public Column(MemberInfo member, string reduceMethodName) : this(member)
        {
            Alais = $"{Alais}({ReduceColumnAlais(reduceMethodName)})";
        }

        public string Alais { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public bool IsSortable{get;set;}
        public ColumnType Type { get; set; }

        private string ReduceColumnAlais(string methodName)
        {
            switch (methodName)
            {
                case "Sum":
                    return "总计";
                case "Average":
                    return "均值";
                case "Max":
                    return "最大值";
                case "Min":
                    return "最小值";
            }
            return "Unknown";
        }
    }
}
