using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
    public class DateTable : EntityBase
    {
        [Key, Display("时间")]
        public int Id { get; set; }
        [Display("时间名称")]
        public string Name { get; set; }
        [Display("上级时间")]
        public int? SuperiorDateTableId { get; set; }

        [ForeignKey("SuperiorDateTableId")]
        public DateTable SuperiorDateTable { get; set; }

        [Include]
        public List<DateTable> SubsidiaryDateTables { get; set; }
    }
}
