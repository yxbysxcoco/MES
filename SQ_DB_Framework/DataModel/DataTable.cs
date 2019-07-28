using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    public class DataTable : List<Row>
    {
        public List<Column> Columns { get; private set; }
        //不合法行集合
        public List<Row> ErrorDataList { get; set; }
        //合法行集合
        public List<Row> LegalDataList { get; set; }

        public DataTable()
        {
            ErrorDataList = new List<Row>();
            LegalDataList = new List<Row>();
        }
        public List<Column> AddColumn(Column column)
        {
            if(Columns == null)
            {
                Columns = new List<Column>();
            }
            Columns.Add(column);
            return Columns;
        }
        public List<Column> AddColumns(IEnumerable<Column> columns)
        {
            foreach(var column in columns)
            {
                AddColumn(column);
            }
            return Columns;
        }
        //将数据转换成对象集合
        public IEnumerable<TEntity> DecodeResult<TEntity>() where TEntity : EntityBase
        {
            var result = new List<TEntity>();
            foreach (var row in this.LegalDataList)
            {
                var entity = Activator.CreateInstance(typeof(TEntity));
                int i = 0;
                foreach (var prop in entity.GetType().GetProperties().GetPropertysWhereAttr<ColumnAttribute>())
                {   
                    prop.SetValue(entity, prop.Convert(row[i].ToString()));
                    i++;
                }
                result.Add(entity as TEntity);
            }
            return result;
        }
        public static DataTable BuildDataTable<TEntity>(params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            var memberExpressions = expressions.ToList();
            if (memberExpressions.Count == 0)
            {
                var param = Expression.Parameter(typeof(TEntity));
                foreach (var prop in typeof(TEntity).GetProperties().Where(p => p.IsDefined(typeof(ColumnAttribute), false)))
                {
                    //协变——提供来源和目标泛型类型的类型实参必须是引用类型，不能是值类型，需要把基础类型装箱
                    Expression conversion = Expression.Convert(Expression.Property(param, prop), typeof(object));
                    memberExpressions.Add(
                        Expression.Lambda<Func<TEntity, object>>(
                            conversion,
                            param
                        )
                    );
                }
            }

            var dt = new DataTable();

            var dbSet = new SQDbSet<TEntity>();

            var entities = dbSet.GetAllEntities();
            foreach (var expression in memberExpressions)
            {
                var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
                dt.AddColumn(
                    new Column
                    {
                        Alais = (member.GetCustomAttributes(typeof(DisplayAttribute), false)[0] as DisplayAttribute).Name,
                        Name = member.Name,
                        Width = member.Width(),
                        Type = member.GetColumnType(),
                        IsSortable = member.IsDefined(typeof(SortableAttribute), false)
                    }
                );

            }

            foreach(var entity in entities)
            {
                var row = new Row();

                foreach (var expression in memberExpressions)
                {
                    row.Add(expression.Compile()(entity));
                }
                dt.Add(row);
            }
            return dt;
        }
        public static DataTable BuildReduceDataTable<TEntity>(Expression<Func<TEntity, object>> groupBy, params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            return null;
        }
    }

}
