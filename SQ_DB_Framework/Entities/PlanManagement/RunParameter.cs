﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities.PlanManagement
{
    public class RunParameter : EntityBase
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display("运行参数")]
        public int RunParameterId { get; set; }
        [Display("自动运行")]
        public int AutomaticRun { get; set; }
        [Display("运行时间")]
        public string RunTime { get; set; }
        [Display("生成计划")]
        public int CreatePlan { get; set; }
        [Display("计划周期")]
        public int PlanPeriods { get; set; }
        public RunParameter() { }

    }
}
