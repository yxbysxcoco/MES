using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Material: EntityBase
{
    [Key, Increment, Column(), DisplayWidthAttribute(0, 4)]
    public int MaterialId { get; set; }
    [Display(Name = "名称"), DisplayWidthAttribute(16, 0), Column()]
    public string Name { get; set; }
    
}
