using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SQ_DB_Framework.Attributes;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
    public class Department
    {
        [Key, Display("部门")]
        public int Id { get; set; }
        [Display("部门名称")]
        public string Name { get; set; }
        [Display("上级部门")]
        public int SuperiorDepartmentId { get; set; }


        public virtual Department SuperiorDepartment { get; set; }
        public virtual List<Department> SubsidiaryDepartments { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}
