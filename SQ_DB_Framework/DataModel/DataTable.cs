using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.EntityConfigures;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    //[DataContract]
    public class DataTable 
    {
        //[DataMember]
        public List<List<Column>> Columns { get; private set; }
        // [DataMember]

        public List<Row> Rows { get; private set; }


        public DataTable()
        {
            Rows= new List<Row>();
        }

        public List<List<Column>> AddColumn(List<Column> column)
        {
            Columns = Columns ?? new List<List<Column>>();
            Columns.Add(column);
            return Columns;
        }

      /*  public List<Column> AddColumns(IEnumerable<Column> columns)
        {
            foreach(var column in columns)
            {
                AddColumn(column);
            }
            return Columns;
        }*/
            
        public void BuildRepalceDataTable<TEntity>(List<TEntity> entities, params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            SetColumn(expressions);
            SetRow(entities,expressions);

        }

        public DataTable SetColumn<TEntity>(params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            var listColumn = new List<Column>();
            foreach (var expression in expressions)
            {
                if (expression.Body is MethodCallExpression methodCall)
                {
                    if (methodCall.Arguments.Count==2)
                    {
                        var sourceMember = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                        var aimMember = (methodCall.Arguments[1] as MemberExpression)?.Member ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as MemberExpression).Member;
                        listColumn.Add(new Column(sourceMember, aimMember));
                        continue;
                    }
                    var pro = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                    var colspan = (methodCall.Arguments[1] as ConstantExpression)?.Value ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as ConstantExpression).Value;
                    var alais = (methodCall.Arguments[2] as ConstantExpression)?.Value ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as ConstantExpression).Value;

                    listColumn.Add(new Column(pro, int.Parse(colspan.ToString()), alais.ToString()));
                    continue;
                }
              
                var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
                listColumn.Add(new Column(member));
            }
            AddColumn(listColumn);
            return this;
        }

        public  DataTable SetRow<TEntity>(List<TEntity> entities,params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            foreach (var entity in entities)
            {
                var row = new Row();

                foreach (var expression in expressions)
                {
                    if (expression.Body is MethodCallExpression methodCall)
                    {
                        foreach (var property in entity.GetType().GetProperties())
                        {
                            var aimMember = (methodCall.Arguments[1] as MemberExpression)?.Member ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as MemberExpression).Member;

                            if (property.Name.Equals(aimMember.ReflectedType.Name))
                            {
                                var value = property.GetValue(entity);

                                var ob = Activator.CreateInstance(value.GetType());
                                foreach (var propertyInfo in ob.GetType().GetProperties())
                                {
                                    if (propertyInfo.Name.Equals(aimMember.Name))
                                    {
                                        row.Add(propertyInfo.GetValue(value));
                                    }
                                }
                            }
                        }
                        continue;
                    }
                    row.Add(expression.Compile()(entity));
                }
                Rows.Add(row);
            }
            return this;
        }

        public  void BuildDataTable<TEntity>(IQueryable<TEntity> entities, params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
          
            var memberExpressions = expressions.ToList();
            if (memberExpressions.Count == 0)
            {
                memberExpressions = GetAllMemberExpressionsOfEntity<TEntity>();
            }
            
            var listColumn = new List<Column>();
            foreach (var expression in memberExpressions)
            {
                var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
                listColumn.Add(new Column(member));
            }
            AddColumn(listColumn);

            foreach (var entity in entities)
            {
                var row = new Row();

                foreach (var expression in memberExpressions)
                {
                    row.Add(expression.Compile()(entity));
                }
                Rows.Add(row);
            }

        }

        public  List<Expression<Func<TEntity, object>>> GetAllMemberExpressionsOfEntity<TEntity>()where TEntity : EntityBase
        {
            var param = Expression.Parameter(typeof(TEntity));
            var memberExpressions = new List<Expression<Func<TEntity, object>>>();

            foreach (var prop in typeof(TEntity).GetProperties().Where(p => p.IsDefined(typeof(ColumnAttribute), false)))
            {
                //协变——提供来源和目标泛型类型的类型实参必须是引用类型，不能是值类型，需要把基础类型装箱
                var conversion = Expression.Convert(Expression.Property(param, prop), typeof(object));
                var lambda = Expression.Lambda<Func<TEntity, object>>(conversion, param);

                memberExpressions.Add(lambda);
            }
            return memberExpressions;
        }

        public  void BuildReduceDataTable<TEntity>(IQueryable<TEntity> entities,Expression<Func<TEntity, object>> groupByItems,
            params Expression<Func<IQueryable<TEntity>, double>>[] reduceExpressions) where TEntity : EntityBase
        {
            var groupByMemberExpressions = new List<Expression<Func<TEntity, object>>>();
            var listColumn = new List<Column>();
            if (groupByItems.Body is NewExpression newExpression)
            {
                var param = Expression.Parameter(typeof(TEntity));
                
                foreach (var member in newExpression.Members)
                {
                    //由于此member是匿名类中的属性，没有特性等信息，直接使用member初始化Column不可行
                    listColumn.Add(new Column(typeof(TEntity).GetProperty(member.Name)));

                    var conversion = Expression.Convert(Expression.Property(param, member.Name), typeof(object));
                    groupByMemberExpressions.Add(Expression.Lambda<Func<TEntity, object>>(conversion, param));

                }
                
            }
            else
            {
                var member = (groupByItems.Body as MemberExpression).Member;
                listColumn.Add(new Column(member));

                groupByMemberExpressions.Add(groupByItems);
            }

            foreach(var expression in reduceExpressions)
            {
                if (!(expression.Body is MethodCallExpression dynamicExpression))
                    continue;

                string groupName = dynamicExpression.Method.Name;

                UnaryExpression unaryexpression = dynamicExpression.Arguments[1] as UnaryExpression;

                LambdaExpression LambdaExpression = unaryexpression.Operand as LambdaExpression;

                var memberExpression = LambdaExpression.Body as MemberExpression;

                listColumn.Add(new Column(memberExpression.Member, groupName));
            }

            AddColumn(listColumn);

            var groups = entities.GroupBy(groupByItems);
            foreach(var group in groups)
            {
                var row = new Row();

                var anonymousObj = group.Key;
                foreach(var prop in anonymousObj.GetType().GetProperties())
                {
                    row.Add(prop.GetValue(anonymousObj));
                }

                foreach(var expression in reduceExpressions)
                {
                    row.Add(expression.Compile()(group.AsQueryable()));
                }

                Rows.Add(row);
            }
        }

        public static object Repalce(object source,object aim) => null;

        public static object Multistage(string name, int v, string v1) => null;
      
    }

}

