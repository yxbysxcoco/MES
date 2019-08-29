
using SQ_DB_Framework.Entities;
using SQ_DB_Framework.Entities.PlanManagement;
using SQ_DB_Framework.SQDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.DataModel
{
    //[DataContract]
    public class DataTable
    {
        //[DataMember]
        public List<List<Column>> Columns { get; private set; }
        // [DataMember]
        public List<Row> Rows { get; private set; }
        public string TableName { get; set; }
        public int PageIndex { get; set; }

        public int PageSize { get; set; } = 10;

        public int TotalCount { get => Rows.Count;}

        public int[] Limits = new int[3] { 10, 15, 20 };

        public DataTable()
        {
            Rows = new List<Row>();
        }

        public List<List<Column>> AddColumn(List<Column> column)
        {
            Columns = Columns ?? new List<List<Column>>();
            Columns.Add(column);
            return Columns;
        }

        public void BuildRepalceDataTable<TEntity>(IQueryable<TEntity> entities, params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            var newMemberExpressions= AddLColumnsLayerReplace("",expressions);
            AddRow(entities,"", newMemberExpressions);
        }
        public void BuildSubmitDataTable<TEntity>(IQueryable<TEntity> entities, params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            var newMemberExpressions = AddLColumnsLayerReplace("submit", expressions);
            AddRow(entities, "submit", newMemberExpressions);
        }
        public Expression<Func<TEntity, object>>[] AddLColumnsLayerReplace<TEntity>(string type, params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            var withoutPropertities = new List<PropertyInfo>();
            var expressionRemoveWithouExp = new List<Expression<Func<TEntity, object>>>();

            foreach (var exp in expressions)
            {
                if (!(exp.Body is MethodCallExpression methodCallExp))
                    continue;

                if (methodCallExp.Method.Name.Equals("Without")) {
                    var sourceMember = (methodCallExp.Arguments[0] as MemberExpression)?.Member ?? ((methodCallExp.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                    withoutPropertities.Add((PropertyInfo)sourceMember);
                }
                else
                {
                    expressionRemoveWithouExp.Add(exp);
                }
            }
            var memberExpressions = GetAllMemberExpressionsOfEntity<TEntity>(withoutPropertities.ToArray());
            var newMemberExpressions = new List<Expression<Func<TEntity, object>>>();

            expressions = expressionRemoveWithouExp.ToArray();

            var listColumn = new List<Column>();
            foreach (var memberexpression in memberExpressions)
            {
                var column = new Column();

                var member = (memberexpression.Body as MemberExpression)?.Member ?? ((memberexpression.Body as UnaryExpression).Operand as MemberExpression).Member;
                bool IsReplace = false;
                foreach (var expression in expressions)
                {
                    var methodCall = expression.Body as MethodCallExpression;
                    if (methodCall.Method.Name.Equals("AppointPro"))
                    {
                        continue;
                    }
                    var sourceMember = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                    var aimMember = (methodCall.Arguments[1] as MemberExpression)?.Member ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as MemberExpression).Member;

                    if (sourceMember.Name.Equals(member.Name))
                    {
                        newMemberExpressions.Add(expression);
                        column.SetValues(sourceMember, aimMember);
                        if (type.Equals("submit"))
                        {
                            column.Name = aimMember.Name;
                        }
                        listColumn.Add(column);
                        IsReplace = true;
                    }   
                }
                if (!IsReplace)
                {
                    newMemberExpressions.Add(memberexpression);
                    column.DefaultValues(member);
                    if (type.Equals("submit"))
                    {
                        column.Name = member.Name;
                    }
                    listColumn.Add(column);
                }
            }
            foreach (var expression in expressions)
            {
                var methodCall = expression.Body as MethodCallExpression;
                if (methodCall.Method.Name.Equals("AppointPro"))
                {
                    var Appointmember = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;

                    var column = new Column();
                    column.DefaultValues(Appointmember);
                    if (type.Equals("submit"))
                    {
                        column.Name = Appointmember.Name;
                    }
                    listColumn.Add(column);
                    newMemberExpressions.Add(expression);
                    continue;
                }
            }
            AddColumn(listColumn);
            return newMemberExpressions.ToArray();
        }

        public DataTable AddColumnsLayer<TEntity>(params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {
            var listColumn = new List<Column>();

            foreach (var expression in expressions)
            {
                var column = new Column();

                if (expression.Body is MethodCallExpression methodCall)
                {
                    switch (methodCall.Method.Name)
                    {
                        case "Repalce":

                            var sourceMember = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                            var aimMember = (methodCall.Arguments[1] as MemberExpression)?.Member ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as MemberExpression).Member;
                            column.SetValues(sourceMember, aimMember);
                            listColumn.Add(column);
                            break;
                        case "AppointPro":

                            var appointMember = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                            column.DefaultValues(appointMember);
                            listColumn.Add(column);

                            break;

                        case "Multistage":
                            if (methodCall.Arguments.Count == 3)
                            {
                                var name = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                                var colspan = (methodCall.Arguments[1] as ConstantExpression)?.Value ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as ConstantExpression).Value;
                                var alais = (methodCall.Arguments[2] as ConstantExpression)?.Value ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as ConstantExpression).Value;
                                column.SetValues(name, int.Parse(colspan.ToString()), alais.ToString());
                                listColumn.Add(column);

                                break;
                            }
                            var colName = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                            var rowspan = (methodCall.Arguments[1] as ConstantExpression)?.Value ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as ConstantExpression).Value;
                            column.SetValues(colName, int.Parse(rowspan.ToString()));
                            listColumn.Add(column);

                            break;

                        case "NewOperation":
                            var oprationId = (methodCall.Arguments[0] as ConstantExpression)?.Value ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as ConstantExpression).Value;
                            var alaisName = (methodCall.Arguments[1] as ConstantExpression)?.Value ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as ConstantExpression).Value;
                            var oprationColspan = (methodCall.Arguments[2] as ConstantExpression)?.Value ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as ConstantExpression).Value;
                            column.SetValues(oprationId.ToString(), alaisName.ToString(), int.Parse(oprationColspan.ToString()));
                            listColumn.Add(column);

                            break;
                        default:
                            break;
                    }
                    continue;
                }
                if (expression.Body is NewExpression newExpression)
                {
                    var param = Expression.Parameter(typeof(TEntity));

                    foreach (var proMember in newExpression.Members)
                    {
                        //由于此member是匿名类中的属性，没有特性等信息，直接使用member初始化Column不可行
                        column.DefaultValues(typeof(TEntity).GetProperty(proMember.Name));
                        listColumn.Add(column);
                        var conversion = Expression.Convert(Expression.Property(param, proMember.Name), typeof(object));
                    }
                }
                var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
                column.DefaultValues(member);
                listColumn.Add(column);
            }
            AddColumn(listColumn);
            return this;
        }

        public DataTable AddRow<TEntity>(IQueryable<TEntity> entities,string type, params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
        {

            if (entities == null)
                return this;
            foreach (var entity in entities)
            {
                var row = new Row();
                foreach (var expression in expressions)
                {
                    if (expression.Body is MethodCallExpression methodCall)
                    {
                        foreach (var property in entity.GetType().GetProperties())
                        {
                            var aimMember = (methodCall.Arguments[0] as MemberExpression)?.Member ?? ((methodCall.Arguments[0] as UnaryExpression).Operand as MemberExpression).Member;
                            if (methodCall.Method.Name.Equals("Repalce"))
                            {
                                aimMember = (methodCall.Arguments[1] as MemberExpression)?.Member ?? ((methodCall.Arguments[1] as UnaryExpression).Operand as MemberExpression).Member;
                            }
                            if (property.Name.Equals(aimMember.ReflectedType.Name))
                            {
                                var value = property.GetValue(entity);

                                var ob = Activator.CreateInstance(value.GetType());
                                foreach (var propertyInfo in ob.GetType().GetProperties())
                                {
                                    if (propertyInfo.Name.Equals(aimMember.Name))
                                    {
                                        row.Add($"{aimMember.ReflectedType.Name}_{propertyInfo.Name}" , propertyInfo.GetValue(value));
                                    }
                                }
                            }
                        }
                        continue;
                    }
                    var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
                    if (type.Equals("submit"))
                    {
                        row.Add($"{member.Name}", expression.Compile()(entity));
                    }
                    row.Add($"{member.ReflectedType.Name}_{member.Name}", expression.Compile()(entity));
                }
                Rows.Add(row);
            }

            return this;
        }

        public void BuildDataTable<TEntity>(IQueryable<TEntity> entities, params Expression<Func<TEntity, object>>[] expressions) where TEntity : EntityBase
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

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    var row = new Row();

                    foreach (var expression in memberExpressions)
                    {
                        var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
                        row.Add(member.Name, expression.Compile()(entity));
                    }
                    Rows.Add(row);
                }
            }
        }

        public List<Expression<Func<TEntity, object>>> GetAllMemberExpressionsOfEntity<TEntity>(params PropertyInfo[] withoutProperties) where TEntity : EntityBase
        {
            var param = Expression.Parameter(typeof(TEntity));
            var memberExpressions = new List<Expression<Func<TEntity, object>>>();

            foreach (var prop in typeof(TEntity).GetProperties().Where(p => p.IsDefined(typeof(DisplayAttribute), false)))
            {
                if (withoutProperties?.Any(p => p.Equals(prop)) ?? false)
                    continue;
                //协变——提供来源和目标泛型类型的类型实参必须是引用类型，不能是值类型，需要把基础类型装箱
                var conversion = Expression.Convert(Expression.Property(param, prop), typeof(object));
                var lambda = Expression.Lambda<Func<TEntity, object>>(conversion, param);

                memberExpressions.Add(lambda);
            }
            return memberExpressions;
        }

        public void BuildReduceDataTable<TEntity>(IQueryable<TEntity> entities, Expression<Func<TEntity, object>> groupByItems,
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

            foreach (var expression in reduceExpressions)
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

            AddRow(entities, groupByItems, reduceExpressions);

        }

        public void AddRow<TEntity>(IQueryable<TEntity> entities, Expression<Func<TEntity, object>> groupByItems, Expression<Func<IQueryable<TEntity>, double>>[] reduceExpressions) where TEntity : EntityBase
        {
            if (entities == null)
                return;
            var groups = entities.GroupBy(groupByItems);
            foreach (var group in groups)
            {
                var row = new Row();
                var anonymousObj = group.Key;

                foreach (var prop in anonymousObj.GetType().GetProperties())
                {
                    row.Add(prop.Name, prop.GetValue(anonymousObj));
                }

                foreach (var expression in reduceExpressions)
                {
                    var member = (expression.Body as MemberExpression)?.Member ?? ((expression.Body as UnaryExpression).Operand as MemberExpression).Member;
                    row.Add(member.Name, expression.Compile()(group.AsQueryable()));

                }
                Rows.Add(row);
            } 
        }

        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : EntityBase
        {
            var sQDbSet = new SQDbSet<TEntity>();

            var entities = sQDbSet.GetAllEntities();

            return entities;
        }

        public IQueryable<ProductionDemandScheme> GetAll<TEntity>() where TEntity : EntityBase
        {
            var sQDbSet = new SQDbSet<ProductionDemandScheme>();

            var entities = sQDbSet.GetAll();

            return entities;
        }

        public List<TEntity> GetEntities<TEntity>(int? pageIndex, int? pageSize, Dictionary<string, string> entityInfoDic) where TEntity : EntityBase
        {
            var sQDbSet = new SQDbSet<TEntity>();
            var entities = sQDbSet.GetEntitiesByCondition(pageIndex ?? 1, pageSize ?? 10, entityInfoDic, "");
            return entities.List;
        }

        public List<TEntity> GetEntities<TEntity>( Dictionary<string, string> entityInfoDic) where TEntity : EntityBase
        {
            var sQDbSet = new SQDbSet<TEntity>();
            var entities = sQDbSet.GetEntitiesByContion(entityInfoDic);
            return entities;
        }

        public DataTable ChangeIndexOfFixedColumns()
        {
            if (Columns.Count == 0)
                return this;
            var columns = Columns[0];
            int lastFixedLeftPoint = 0;

            for (int i = 0; i < columns.Count; i++)
            {
                if (!(columns[i].Fixed?.Equals("left") ?? false))
                    continue;

                for (int j = lastFixedLeftPoint; j < i; j++)
                {
                    if (!(columns[j].Fixed?.Equals("left") ?? false))
                    {
                        columns.Insert(j, columns[i]);
                        columns.RemoveAt(i + 1);
                        lastFixedLeftPoint = j;
                        break;
                    }
                }

            }

            int lastFixedRightPoint = columns.Count - 1;

            for (int i = columns.Count - 1; i >= 0; i--)
            {
                if (!(columns[i].Fixed?.Equals("right") ?? false))
                    continue;
                for (int j = lastFixedRightPoint; j > i; j--)
                {
                    if (!(columns[j].Fixed?.Equals("right") ?? false))
                    {
                        columns.Insert(j + 1, columns[i]);
                        columns.RemoveAt(i);
                        lastFixedRightPoint = j;
                        break;
                    }
                }
            }
            return this;
        }

        public static object Repalce(object source, object aim) => null;


        public static object Multistage(object name, int colspan, string alais) => null;

        public static object Multistage(object name, int rowspan) => null;

        public static object NewOperation(string id, string name, int rowspan) => null;

        public static object AppointPro(object pro ) => null;

        public static object Without(object propertity) => null;


    }

}

