using SQ_DB_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
    public class Employee:EntityBase
    {
        [Key]
        [Display("编号")]
        public int Id { get; set; }
        [Display("姓名")]
        public string Name { get; set; }
        [Display("部门")]
        public int DepartmentId { get; set; }


        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
