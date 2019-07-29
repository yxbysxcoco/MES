using SQ_DB_Framework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace SQ_Render.Models.Common
{
    public class TableHeader
    {
        public string GetDataUrl { get; set; }

        public List<Field> Fields { get; set; }

        public  TableHeader(IEnumerable<PropertyInfo> propertyInfos)
        {
            Fields = new List<Field>();
            foreach (var property in propertyInfos)
            {
                Field field = new Field()
                {
                    FiledName = property.Name,
                    Alias = property.GetCustomAttribute<DisplayAttribute>().Name,
                    Length = property.Width()
                };
                Fields.Add(field);
            }
        }
        public DataTable GetSum<T>(IQueryable<T> collection, Expression<Func<T, String>> groupby, params Expression<Func<T, double>>[] expressions)
        {
            DataTable table = new DataTable();

            //  Message：利用表达式设置列名称
            MemberExpression memberExpression = groupby.Body as MemberExpression;

            var displayName = (memberExpression.Member.GetCustomAttributes(false)[0] as DescriptionAttribute).Description;

            table.Columns.Add(new DataColumn(displayName));

            foreach (var expression in expressions)
            {
                memberExpression = expression.Body as MemberExpression;

                displayName = (memberExpression.Member.GetCustomAttributes(false)[0] as DescriptionAttribute).Description;

                table.Columns.Add(new DataColumn(displayName));

            }

            //  Message：通过表达式设置数据体 
            var groups = collection.GroupBy(groupby);

            foreach (var group in groups)
            {
                //  Message：设置分组列头
                DataRow dataRow = table.NewRow();
                dataRow[0] = group.Key;

                //  Message：设置分组汇总数据
                for (int i = 0; i < expressions.Length; i++)
                {
                    var expression = expressions[i];

                    Func<T, double> fun = expression.Compile();

                    dataRow[i + 1] = group.Sum(fun);
                }

                table.Rows.Add(dataRow);
            }

            return table;

        }
    }
}