using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ_DB_Framework.Attributes
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
    
    public class IncrementAttribute : Attribute
    {

        public IncrementAttribute()
        {

        }
    }
    //外键特性
    /*public class ForeignKeyAttribute : Attribute
    {
        public Type TableType { get; }
        public string ForigenPropertyName{ get; }

        public ForeignKeyAttribute(Type tableType, string fkPropertyName)
        {
            TableType = tableType;
            ForigenPropertyName = fkPropertyName;
        }
    }*/
    //唯一特性
    public class UniqueKeyAttribute : Attribute
    {
      
        public UniqueKeyAttribute()
        {
          
        }
    }
    public class IndexAttribute : Attribute
    {
    }
    public class DefaultValueAttribute : Attribute
    {
        public object DefaultValue { get; }
        public DefaultValueAttribute(object defaultValue)
        {
            DefaultValue = defaultValue;
        }
    }
}
