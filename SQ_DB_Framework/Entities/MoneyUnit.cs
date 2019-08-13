using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

public class MoneyUnit : EntityBase
{
    [Key, Increment,  Display("货币单位")]
    public int MoneyUnitId { get; set; }
    [Display("名称")]
    [MaxLength(10)]
    public string Name { get; set; }
}
