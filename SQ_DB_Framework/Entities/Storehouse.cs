using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
public class Storehouse : EntityBase
{
    [Key, Increment,  Display("仓库")]
    public int StorehouseId { get; set; }
    [Display("名称")]
    public string Name { get; set; }
}
