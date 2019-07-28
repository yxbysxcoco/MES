using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.DataModel
{
    public class Column
    {
        public string Alais { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public ColumnType Type { get; set; }
    }
}
