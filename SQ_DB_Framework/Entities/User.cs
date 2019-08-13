using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DisplayAttribute = SQ_DB_Framework.Attributes.DisplayAttribute;

namespace SQ_DB_Framework.Entities
{
    [DataContract]
    public class Users : EntityBase
    {
        [Key,Display("用户")]
        [DataMember]
        public int UserId { get; set; }
        [Display("用户名")]
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        [Display("密码")]
        public string PassWord { get; set; }
    }
}
