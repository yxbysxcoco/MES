using SQ_DB_Framework.Attributes;
using SQ_DB_Framework.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;
public class ToolEquipmentType : EntityBase
{
    [Key, Increment, Display("工装类型")]
    public int TypeId { get; set; }
    [Display("工装类型名称")]
    public string Name { get; set; }
}