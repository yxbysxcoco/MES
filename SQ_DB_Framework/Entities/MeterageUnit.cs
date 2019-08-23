using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

public class MeterageUnit : EntityBase
{
    [Key, Increment, Display("计量单位")]
    public int MeterageUnitId { get; set; }
    [Display("名称")]
    public string Name { get; set; }
}
