using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Attributes
{
    //表特性
    public class TableAttribute : Attribute
    {
        public Type TypeTable { get; }
        public TableAttribute(Type typeTable)
        {
            TypeTable = typeTable;
        }
    }
    //主键特性
    public class PrimaryKeyAttribute : Attribute
    {
       
        public PrimaryKeyAttribute()
        { 
        }
    }
    public class IncrementAttribute : Attribute
    {

        public IncrementAttribute()
        {
        }
    }
    //外键特性
    public class ForeignKeyAttribute : Attribute
    {
        public Type TableType { get; }
        public string ForigenPropertyName{ get; }
        public ForeignKeyAttribute(Type tableType, string forigenPropertyName)
        {
            TableType = tableType;
            ForigenPropertyName = forigenPropertyName;
        }
    }
    //唯一特性
    public class UniqueKeyAttribute : Attribute
    {
        public UniqueKeyAttribute()
        {
        }
    }
    //字段特性
    public class FieldAttribute : Attribute
    {
        public string FieldName { get; }
        public string FieldType { get; }
        public FieldAttribute(string fieldName, string fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
        }
    }
    public class CommonAttribute : Attribute
    {
        public string Common { get; }
        public CommonAttribute()
        {
            Common = "common";
        }
    }
    public class DefaultValueAttribute : Attribute
    {
        public object DefaultValue { get; }
        public DefaultValueAttribute(object defaultValue)
        {
            DefaultValue = defaultValue;
        }
    }
    public class RequiredAttribute : Attribute
    {
        public string Required { get; }
        public RequiredAttribute()
        {
            Required = "Required";
        }
    }
}
