using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ToolEquipmentType : EntityBase
{
    [Key, Increment, Column()]
    public int TypeId { get; set; }
    [Display(Name = "名称"), Column()]
    public string Name { get; set; }
}