using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    public class Column
    {
        public string Id { get; set; }
        public string Alais { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public bool IsSortable { get; set; }
        public ColumnType Type { get; set; }
        public string Fixed { get; set; }

        public int Colspan { get; set; }
        public int Rowspan { get; set; }

        public Column() { }
        public Column(MemberInfo member)
        {
            Alais = (member.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name;
            Name = member.Name;
            Width = GetWidth(member);
            Type = member.GetColumnType();
            IsSortable = member.IsDefined(typeof(SortableAttribute), false);
            Fixed = member.IsDefined(typeof(FixedAttribute), false) ? member.GetCustomAttribute<FixedAttribute>().Fixed : null;
        }
        private int GetWidth(MemberInfo member)
        {
            var charWidth = member.GetCustomAttribute<DisplayWidthAttribute>().CharWidth;
            var chineseWidth = member.GetCustomAttribute<DisplayWidthAttribute>().ChineseWidth;

            if (charWidth * 10 + chineseWidth * 16 > Alais.Length * 16)
            {
                return charWidth * 10 + chineseWidth * 16;
            }
            else
            {
                return Alais.Length * 16;
            }
        }
        private int GetWidth(MemberInfo sourceMember, MemberInfo aimMember)
        {
            var charWidth = (aimMember.GetCustomAttributes(typeof(DisplayWidthAttribute), false)[0] as DisplayWidthAttribute).CharWidth;
            var chineseWidth = (aimMember.GetCustomAttributes(typeof(DisplayWidthAttribute), false)[0] as DisplayWidthAttribute).ChineseWidth;
            var sourceWidth = (sourceMember.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name.Length;
            var aimWidth = aimMember.GetCustomAttribute<DisplayAttribute>().Name.Length;

            if (charWidth * 10 + chineseWidth * 16 > (sourceWidth+ aimWidth) * 16)
            {
                return charWidth * 10 + chineseWidth * 16;
            }
            else
            {
                return Alais.Length * 16;
            }
        }


        public Column(MemberInfo member,int colspan,string alais)
        {
            Alais = (member.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name+ alais;
            Name = member.Name;
            Width = GetWidth(member);
            Type = member.GetColumnType();
            IsSortable = member.IsDefined(typeof(SortableAttribute), false);
            Colspan = colspan;
           
        }

        public Column(MemberInfo member, int rowspan) : this(member)
        {
            Alais = (member.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name;
            Name = member.Name;
            Width = GetWidth(member);
            Type = member.GetColumnType();
            IsSortable = member.IsDefined(typeof(SortableAttribute), false);
            Rowspan = rowspan;

        }

        public Column(MemberInfo sourceMember, MemberInfo aimMember)
        {
            Alais = (sourceMember.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name+
                (aimMember.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name;
            Name = aimMember.ReflectedType.Name+"_"+ aimMember.Name; ;
            Width = GetWidth(sourceMember,aimMember);
            Type = aimMember.GetColumnType();
            IsSortable = aimMember.IsDefined(typeof(SortableAttribute), false);
        }
        public Column(MemberInfo member, string reduceMethodName) : this(member)
        {
            Alais = $"{Alais}({ReduceColumnAlais(reduceMethodName)})";
            Width = GetWidth(member);
        }

        public Column(string name, string alais, int rowspan)
        {
            Alais = alais;
            Id = name;
            Rowspan = rowspan;
        }



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
