using MES.Attributes;
using MES.Config;
using MES.Table;
using MES.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MES.Table
{
    public abstract class DBTable
    {
        public MyDbConnection myDbConnection = new MyDbConnection();
        //实体属性集合
        public PropertyInfo[] PropertyInfos { get; }
        //表名
        public string TableName { get; }
        //属性与数据库字段的类型映射集合
        public Dictionary<PropertyInfo, string> PropertyAndType;
        //字段与数据库字段的类型映射集合
        public Dictionary<string, string> FiledAndType;
        public DBTable()
        {
            PropertyInfos = this.GetType().GetProperties();
            TableName = this.GetType().Name;
            PropertyAndType = new Dictionary<PropertyInfo, string>();
            FiledAndType = new Dictionary<string, string>();
        }
        //保存数据
        public  MyDataTable SaveData(MyDataTable dataTable)
        {
            //设置实体属性/字段对应的数据库字段类型集合
            SetPropertyOrFieldAndType();
            //检查不合法数据
            CheckIllegalData(dataTable);
            //检查冲突的数据
            CheckConfilictData(dataTable);
            //将合法数据存入表中
            if (InsertDataToDB(dataTable))
            {
                Debug.WriteLine("成功");
            }
            return dataTable;
        }
        //获取所有数据
        internal MyDataTable GetAllData(MyDataTable dataTable)
        {
            SetPropertyOrFieldAndType();
            var propertyInfos = PropertyInfos.GetPropertysWhereAttr<ForeignKeyAttribute>();
            var PoreignKeyAndFrimaryKey = new Dictionary<string, string>();
            var foreignTableNameAndfieldName = new Dictionary<string, List<string>>();
            PoreignKeyAndFrimaryKey = SetFKAndPKOrFTableAndPk(propertyInfos, PoreignKeyAndFrimaryKey, foreignTableNameAndfieldName);
            dataTable = myDbConnection.SelectAll(PoreignKeyAndFrimaryKey, foreignTableNameAndfieldName, dataTable, FiledAndType, TableName);
            return dataTable;
        }
        private Dictionary<string, string> SetFKAndPKOrFTableAndPk(IEnumerable<PropertyInfo> propertyInfos, Dictionary<string, string> poreignKeyAndFrimaryKey, Dictionary<string, List<string>> foreignTableNameAndfieldName)
        {
            foreach (var property in propertyInfos)
            {
                Type foreignTable = property.GetCustomAttribute<ForeignKeyAttribute>().TableType;
                string foreignPropertyName = property.GetCustomAttribute<ForeignKeyAttribute>().ForigenPropertyName;
                var fieldName = foreignTable.GetProperties().GetPropertysWhereAttr<FieldAttribute>().
                    Where(_ => _.Name.Equals(foreignPropertyName)).
                    Single().
                    GetCustomAttribute<FieldAttribute>().FieldName;
                var fieldForeignPrimaryName = foreignTable.GetProperties().GetPropertysWhereAttr<PrimaryKeyAttribute>().Single().
                    GetCustomAttribute<FieldAttribute>().FieldName;
                poreignKeyAndFrimaryKey.Add(property.GetCustomAttribute<FieldAttribute>().FieldName, fieldForeignPrimaryName);
                List<string> fieldList = new List<string>()
                {
                    fieldForeignPrimaryName,fieldName,
                };
                foreignTableNameAndfieldName.Add(foreignTable.Name, fieldList);
            }
            return poreignKeyAndFrimaryKey;
        }









        //检查数据与字段类型是否匹配
        public MyDataTable CheckIllegalData(MyDataTable dataTable)
        {
            //检查数据与类型是否匹配
            CheckTypeAndData(dataTable);
            return dataTable;
        }
        //检查数据与类型是否匹配
        public MyDataTable CheckTypeAndData(MyDataTable dataTable)
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
        //获取实体属性对应的数据库字段类型集合
        public void SetPropertyOrFieldAndType()
        {
            foreach (PropertyInfo property in PropertyInfos.GetPropertysWhereAttr<FieldAttribute>())
            {
                //如果字段自增就忽略
                if (property.GetCustomAttribute(typeof(IncrementAttribute)) != null)
                {
                    continue;
                }
                string fieldType = "";
                if (property.GetCustomAttribute(typeof(ForeignKeyAttribute)) != null)
                {
                    fieldType = AddFKFieldAndType(property);
                }
                else
                {
                    fieldType = (property.GetCustomAttribute(typeof(FieldAttribute)) as FieldAttribute).FieldType;
                }
                //构造实体属性与数据库字段类型的集合
                PropertyAndType.Add(property, fieldType);
                //构造数据库中字段与类型的集合
                FiledAndType.Add((property.GetCustomAttribute(typeof(FieldAttribute)) as FieldAttribute).FieldName,
                    (property.GetCustomAttribute(typeof(FieldAttribute)) as FieldAttribute).FieldType);
            }
        }
        //将外键对应的属性和数据库字段类型存入属性与类型字典中
        public static string AddFKFieldAndType(PropertyInfo propertyFk)
        {
            //外键表实体类名
            Type typeType = (propertyFk.GetCustomAttribute<ForeignKeyAttribute>().TableType);
            //外键字段映射的唯一字段名
            string ForigenPropertyName = (propertyFk.GetCustomAttribute(typeof(ForeignKeyAttribute)) as ForeignKeyAttribute).ForigenPropertyName;
            //根据具有外键特性的属性获取外键表的具有字段特性的属性集合
            return typeType.GetProperties().
                GetPropertysWhereAttr<FieldAttribute>().
                Where(_=>_.Name== ForigenPropertyName).
                Single().
                GetCustomAttribute<FieldAttribute>().FieldType; 
        }
        //判断数据是否合法
        private bool CheckIsLegal( Row row)
        {
            object[] cells = row.ToArray();
            int i = 0;
            foreach (PropertyInfo property in PropertyAndType.Keys)
            {
                if (cells[i] == null)
                {
                    if (property.GetCustomAttribute(typeof(RequiredAttribute)) != null) 
                    {
                        return false;
                    }
                    if (property.GetCustomAttribute(typeof(DefaultValueAttribute)) != null)
                    {
                        row[i] = ((property.GetCustomAttribute<DefaultValueAttribute>()).DefaultValue);
                    }
                    else
                    {
                        row.Add(DBNull.Value);
                    }
                }
                else
                {
                    try
                    {
                        TransforType(PropertyAndType[property], cells[i]);
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
        //将数据转换成对应的类型
        private static void TransforType(string type, object value)
        {
            switch (type)
            {
                case "Int":
                    int.Parse(value.ToString());
                    break;
                case "Varchar2":
                    break;
                case "TIMESTAMP":
                    DateTime.Parse(value.ToString());
                    break;
                case "Duoble":
                    double.Parse(value.ToString());
                    break;
                default:
                    break;
            }
        }
        //将List数据集合转换为字典集合
        public Dictionary<string, Column> DataToDictionary( MyDataTable dataTable)
        {
            var fieldAndValue = new Dictionary<string, Column>();
            int i = 0;
            foreach (var field in FiledAndType.Keys)
            {
                Column column = new Column();
                foreach (Row row in dataTable.LegalDataList)
                {
                    column.Add(row.ToArray()[i]);
                }
                fieldAndValue.Add(field, column);
                i++;
            }
            return fieldAndValue;
        }
        //筛选合法有效和冲突的数据
        public MyDataTable CheckConfilictData(MyDataTable dataTable)
        {
            //处理主键特性
            HandlePkAndUkAttribute(dataTable, PropertyInfos.GetPropertysWhereAttr<PrimaryKeyAttribute>());
            //处理唯一特性
            HandlePkAndUkAttribute(dataTable, PropertyInfos.GetPropertysWhereAttr<UniqueKeyAttribute>());
            //处理外键特性
            HandleFKAttribute(dataTable, PropertyInfos.GetPropertysWhereAttr<ForeignKeyAttribute>());

            return dataTable;
        }
        //处理外键特性
        public MyDataTable HandleFKAttribute(MyDataTable dataTable, IEnumerable<PropertyInfo> propertyInfos)
        {
            if (dataTable.LegalDataList.Count == 0)
            {
                return dataTable;
            }
            foreach (PropertyInfo property in propertyInfos)
            {
                //外键表实体类名
                Type foreignTable = (property.GetCustomAttribute<ForeignKeyAttribute>().TableType);
                //外键字段映射的唯一字段名
                string ForigenPropertyName = property.GetCustomAttribute<ForeignKeyAttribute>().ForigenPropertyName;
                //获取对应的外键表字段名
                var fieldName = foreignTable.GetProperties().GetPropertysWhereAttr<FieldAttribute>().
                    Where(_=>_.Name.Equals(ForigenPropertyName)).
                    Single().
                    GetCustomAttribute<FieldAttribute>().FieldName;

                //向外键表中存入未存在的数据
                IsertFKTable(dataTable,foreignTable,property, fieldName);
                //将值转换为外键表的主键值
                valueTransformPrimary(dataTable, foreignTable, fieldName,property);
            }
            return dataTable;
        }
        //将值转换为外键表的主键值
        public void valueTransformPrimary(MyDataTable dataTable, Type foreignTable, string fieldName, PropertyInfo property)
        {
            //定义外键表的数据集合
            IEnumerable<object> column = GetColumnList(dataTable, property).Distinct();
            Dictionary<object, object> valueAndPrimary = myDbConnection.SelectFKValueByField(foreignTable.Name, fieldName, column);
            if (valueAndPrimary.Keys.Count!=0)
            {
                int index = PropertyAndType.Keys.ToList().MyDictionary()[property];
                foreach (Row row in dataTable.LegalDataList)
                {
                    foreach (object value in valueAndPrimary.Keys)
                    {
                        if (row.Contains(value))
                        {
                            row[index] = valueAndPrimary[value];
                        }
                    }
                }
            }
        }
        //向外键表中存入未存在的数据
        public bool IsertFKTable(MyDataTable dataTable, Type foreignTable, PropertyInfo property, string fieldName)
        {
            //定义需存入的外键表的数据集合
            IEnumerable<object> column = GetColumnList(dataTable, property).Distinct();
            var valueList = myDbConnection.SelectValueByField(foreignTable.Name, fieldName, column);
            //定义插入外键表的列
            Column insertColumn = new Column();
            foreach (var value in column)
            {
                if (valueList.Count != 0)
                {
                    if (!valueList.Contains(value))
                    {
                        insertColumn.Add(value);
                    }
                    continue;
                }
                insertColumn.Add(value);
            }
            if (insertColumn.Count != 0)
            {
                var FieldAndValue = new Dictionary<string, Column>
                {
                    { fieldName, insertColumn }
                };
                var fieldAndType = new Dictionary<string, string>();
                var type = foreignTable.GetProperties().GetPropertysWhereAttr<FieldAttribute>().
                      Where(_ => _.GetCustomAttribute<FieldAttribute>().FieldName.Equals(fieldName)).
                      Single().
                      GetCustomAttribute<FieldAttribute>().FieldType;
                fieldAndType.Add(fieldName, type);
                
                return  myDbConnection.InsertData(foreignTable, FieldAndValue, fieldAndType);
            }
            return false;
        }
        //处理主键/唯一键特性
        public MyDataTable HandlePkAndUkAttribute(MyDataTable dataTable, IEnumerable<PropertyInfo> propertys) 
        {
            if (dataTable.LegalDataList.Count==0)
            {
                return dataTable;
            }
            foreach (PropertyInfo property in propertys)
            {
                string fieldName = (property.GetCustomAttribute(typeof(FieldAttribute)) as FieldAttribute).FieldName;
                IEnumerable<object> column = GetColumnList(dataTable, property).Distinct();
                 var valueList = myDbConnection.SelectValueByField(TableName, fieldName, column);
                if (valueList.Count != 0)
                {
                    //筛选出冲突数据
                    dataTable = GetConflictData(dataTable, valueList, property);
                }
            }
            return dataTable;
        }
        //根据属性获取该列数据
        private Column GetColumnList(MyDataTable dataTable, PropertyInfo property)
        {
            Column column = new Column();
            if (PropertyAndType.Keys.Contains(property))
            {
                int index = PropertyAndType.Keys.ToList().MyDictionary()[property];
                foreach (Row row in dataTable.LegalDataList)
                {
                    column.Add(row[index]);
                }
            }
            return column;
        }
        //筛选出冲突数据
        public  MyDataTable GetConflictData(MyDataTable dataTable,  List<object> valueList, PropertyInfo property)
        {
            List<Row> rowList = new List<Row>();
            int index = PropertyAndType.Keys.ToList().MyDictionary()[property];
            foreach (Row row in dataTable.LegalDataList)
            {
                if (valueList.Contains(row[index]))
                {
                    dataTable.ErrorDataList.Add(row);
                    continue;
                }
                    rowList.Add(row);
            }
            dataTable.LegalDataList = rowList;
            return dataTable;
        }
        //将合法数据存入表中
        public bool InsertDataToDB(MyDataTable dataTable)
        {
            if (dataTable.LegalDataList.Count == 0)
            {
                return false;
            }
            var fieldAndValue = DataToDictionary(dataTable);
            if (fieldAndValue.Keys.Count != 0)
            {
                return myDbConnection.InsertData( this.GetType(), fieldAndValue, FiledAndType);
            }
            return false;
        }
    }
}
