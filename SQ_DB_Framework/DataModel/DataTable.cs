﻿using SQ_DB_Framework.Attributes;
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
            Columns = Columns ?? new List<Column>();
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
                memberExpressions = GetAllMemberExpressionsOfEntity<TEntity>();
            }

            var dt = new DataTable();
            var dbSet = new SQDbSet<TEntity>();

            var entities = dbSet.GetAllEntities();
            foreach (var expression in memberExpressions)
            {
                var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
                dt.AddColumn(new Column(member));
            }

            foreach (var entity in entities)
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
        private static List<Expression<Func<TEntity, object>>> GetAllMemberExpressionsOfEntity<TEntity>()where TEntity : EntityBase
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

        public static DataTable BuildReduceDataTable<TEntity>(Expression<Func<TEntity, object>> groupByItems,
            params Expression<Func<IQueryable<TEntity>, double>>[] reduceExpressions) where TEntity : EntityBase
        {
            var dt = new DataTable();
            var groupByMemberExpressions = new List<Expression<Func<TEntity, object>>>();

            if(groupByItems.Body is NewExpression newExpression)
            {
                var param = Expression.Parameter(typeof(TEntity));
                foreach(var member in newExpression.Members)
                {
                    //由于此member是匿名类中的属性，没有特性等信息，直接使用member初始化Column不可行
                    dt.AddColumn(new Column(typeof(TEntity).GetProperty(member.Name)));

                    var conversion = Expression.Convert(Expression.Property(param, member.Name), typeof(object));
                    groupByMemberExpressions.Add(Expression.Lambda<Func<TEntity, object>>(conversion, param));
                }
            }
            else
            {
                var member = (groupByItems.Body as MemberExpression).Member;
                dt.AddColumn(new Column(member));

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

                dt.AddColumn(new Column(memberExpression.Member, groupName));
            }

            var dbSet = new SQDbSet<TEntity>();

            var groups = dbSet.GetAllEntities().GroupBy(groupByItems);
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

                dt.Add(row);
            }
            return dt;
        }
    }

}
