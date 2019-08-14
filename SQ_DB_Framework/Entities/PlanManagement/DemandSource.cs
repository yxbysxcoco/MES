using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    class DemandSource
    {
        [Key]
        [Display("需求来源")]
        public int Id { get; set; }
        [Display("名称")]
        public string Name { get; set; }
    }
}
