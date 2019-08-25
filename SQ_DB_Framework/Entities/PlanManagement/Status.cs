using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class Status : EntityBase
    {
        [Key]
        [Display("状态")]
        public int id { get; set; }
        [Display("名称")]
        public string Name { get; set; }
    }
}
