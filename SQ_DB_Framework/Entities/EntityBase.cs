using SQ_DB_Framework.DataModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SQ_DB_Framework.Attributes;
using System.ComponentModel;
using System.Linq;

namespace SQ_DB_Framework.Entities
{
   public class EntityBase
    {
        //所有属性集合
        public PropertyInfo[] GetPropertyInfos()
        {
            return this.GetType().GetProperties();
        }
        //属性与数据库字段的类型映射集合
        private readonly Dictionary<PropertyInfo, Type> PropertyAndType;
        public EntityBase()
        {
            PropertyAndType= new Dictionary<PropertyInfo, Type>();
        }
        public void SetPropertyAndType()
        {
            foreach (PropertyInfo property in GetPropertyInfos().GetPropertysWhereAttr<ColumnAttribute>())
            {
                //如果字段自增就忽略
                if (property.HaveAttribute<IncrementAttribute>())
                {
                    continue;
                }
                //构造实体属性与数据库字段类型的集合
                PropertyAndType.Add(property, property.PropertyType);
            }
        }
        public DataTable CheckIllegalData(DataTable dataTable)
        {
            SetPropertyAndType();
            //检查数据与类型是否匹配
            dataTable = CheckTypeAndData(dataTable);
            return dataTable;
        }
        //检查数据与类型是否匹配
        public DataTable CheckTypeAndData(DataTable dataTable)
        {
            foreach (Row row in dataTable)
            {
                if (row.Count != PropertyAndType.Keys.Count)
                {
                    dataTable.ErrorDataList.Add(row);
                    continue;
                }
                if (CheckIsLegal(row))
                {
                    dataTable.LegalDataList.Add(row);
                }
                else
                {
                    dataTable.ErrorDataList.Add(row);
                }
            }
            return dataTable;
        }
        //判断数据是否合法
        public bool CheckIsLegal(Row row)
        {
            object[] cells = row.ToArray();
            int i = 0;
            foreach (PropertyInfo property in PropertyAndType.Keys)
            {
                if (cells[i] == null)
                {
                    if (property.HaveAttribute<RequiredAttribute>())
                    {
                        return false;
                    }
                    if (property.HaveAttribute<DefaultValueAttribute>())
                    {
                        row[i] = (property.GetCustomAttribute<DefaultValueAttribute>()).Value;
                        continue;
                    }
                        row.Add(DBNull.Value);
                }
                else
                {
                    try
                    {
                        //转换数据
                        property.Convert(cells[i].ToString());
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                i++;
            }
            return true;
        }

    }
}
