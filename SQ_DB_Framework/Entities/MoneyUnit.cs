using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MoneyUnit : EntityBase
{
    [Key, Increment, Column()]
    public int MoneyUnitId { get; set; }
    [Display(Name = "名称"), Column()]
    [MaxLength(10)]
    public string Name { get; set; }
}
