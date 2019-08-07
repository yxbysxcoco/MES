using System;
using System.Collections.Generic;
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
    public class DisplayWidthAttribute : Attribute
    {
        public int CharWidth { get; }
        public int ChineseWidth { get; }
        public DisplayWidthAttribute(int chineseWidth,int charWidth)
        {
            CharWidth = charWidth;
            ChineseWidth = chineseWidth;
        }
    }
    public class SortableAttribute : Attribute
    {

    }
}
