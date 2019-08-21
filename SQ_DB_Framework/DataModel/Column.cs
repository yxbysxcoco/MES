using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

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
        public bool IsWritable { get; set; }
        public bool HasQRCode { get; set; }

        public int Colspan { get; set; }
        public int Rowspan { get; set; } 

        public Column() { }
        public Column(MemberInfo member)
        {
            Alais = member.GetCustomAttribute<DisplayAttribute>().Name;
            Name = $"{member.ReflectedType.Name}_{member.Name}";
            Type = member.GetColumnType();
            Fixed = member.IsDefined(typeof(FixedAttribute), false) ? member.GetCustomAttribute<FixedAttribute>().Fixed : null;
            SetWidth(member);
        }
        public Column SetWidth(MemberInfo member)
        {
            var charWidth = member.GetCustomAttribute<DisplayAttribute>().CharWidth;
            var chineseWidth = member.GetCustomAttribute<DisplayAttribute>().ChineseWidth;

            if (charWidth * 10 + chineseWidth * 16 > Alais.Length * 16  )
            {
                Width= charWidth * 10 + chineseWidth * 16 + 32;//表格两侧留空32像素
            }
            else
            {
                Width= Alais.Length * 16  + 32;//可排序列排序符号10像素
            }
            return this;
        }

        public Column SetWidth(MemberInfo sourceMember, MemberInfo aimMember)
        {
            var charWidth = aimMember.GetCustomAttribute<DisplayAttribute>().CharWidth;
            var chineseWidth = aimMember.GetCustomAttribute<DisplayAttribute>().ChineseWidth;
            var sourceWidth = sourceMember.GetCustomAttribute<DisplayAttribute>().Name.Length;
            var aimWidth = aimMember.GetCustomAttribute<DisplayAttribute>().Name.Length;

            if (charWidth * 10 + chineseWidth * 16 > (sourceWidth+ aimWidth) * 16 )
            {
                Width = charWidth * 10 + chineseWidth * 16 + 32;
            }
            else
            {
                Width = Alais.Length * 16 + 32;
            }
            return this;
        }

        public Column(MemberInfo member,int colspan,string alais) :this(member)
        {
            Alais = member.GetCustomAttribute<DisplayAttribute>().Name+ alais;
            Colspan = colspan;
           
        }

        public Column(MemberInfo member, int rowspan) : this(member)
        {
            Rowspan = rowspan;
        }

        public Column(MemberInfo sourceMember, MemberInfo aimMember) : this(aimMember)
        {
            Alais = sourceMember.GetCustomAttribute<DisplayAttribute>().Name+
                aimMember.GetCustomAttribute<DisplayAttribute>().Name;
            Name =$"{aimMember.ReflectedType.Name}_{aimMember.Name}" ;
            SetWidth(sourceMember,aimMember);
        }
        public Column(MemberInfo member, string reduceMethodName) : this(member)
        {
            Alais = $"{Alais}({ReduceColumnAlais(reduceMethodName)})";
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

        public Column SetHasQRCode(bool hasQRCode)
        {
            HasQRCode = hasQRCode;
            return this;
        }
        public Column SetIsSortable(bool isSortable)
        {
            IsSortable = isSortable;
            Width += (IsSortable ? 10 : 0);
            return this;
        }
    }
}
