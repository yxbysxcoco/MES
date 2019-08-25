using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SQ_DB_Framework.Attributes;
using System.Text;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQ_DB_Framework.Entities
{
    public class Department : EntityBase 
    {
        [Key, Display("部门")]
        public int Id { get; set; }
        [Display("部门名称")]
        public string Name { get; set; }
        [Display("上级部门")]
        public int? SuperiorDepartmentId { get; set; }

        [ForeignKey("SuperiorDepartmentId")]
        public  Department SuperiorDepartment { get; set; }

        [Include]
        public List<Department> SubsidiaryDepartments { get; set; }
        [Include]
        public  List<Employee> Employees { get; set; }
    }

   
}
