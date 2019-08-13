using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ_DB_Framework.Attributes
{

    public class IncrementAttribute : Attribute
    {
    }
    //唯一特性
    public class UniqueKeyAttribute : Attribute
    {

    }
    public class IndexAttribute : Attribute
    {
    }
    public class DisplayAttribute : Attribute
    {
        public int CharWidth { get; set; }
        public int ChineseWidth { get; set; }

        public string  Name { get; }
        public DisplayAttribute(string name)
        {
            Name = name;
            CharWidth = 5;
            ChineseWidth = 0;
        }
    }
    public class FixedAttribute : Attribute
    {
        public string Fixed { get; set; }
        public FixedAttribute(string _fixed)
        {
            Fixed = _fixed;
        }
    }
    public class SortableAttribute : Attribute
    {

    }
}
