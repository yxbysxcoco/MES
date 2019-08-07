using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Storehouse : EntityBase
{
    [Key, Increment, Column(), DisplayWidthAttribute(0, 4)]
    public int StorehouseId { get; set; }
    [Display(Name = "名称"), Column(),DisplayWidthAttribute(16, 0)]
    public string Name { get; set; }
}
