using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace SQ_DB_Framework.Entities
{
    [DataContract]
    public class Users : EntityBase
    {
        [Key,Column,Display(Name = "用户id")]
        [DataMember]
        public int UserId { get; set; }
        [Column,Display(Name = "用户名")]
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        [Column, Display(Name = "密码")]
        public string PassWord { get; set; }
    }
}
