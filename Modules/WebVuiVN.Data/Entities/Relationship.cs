using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebVuiVN.Data.Enums;
using WebVuiVN.Data.Interface;
using WebVuiVN.Infrastructure.SharedKernel;

namespace WebVuiVN.Data.Entities
{
    [Table("Relationships")]
    public class Relationship : DomainEntity<int>, IDateTracking
    {
        public Relationship() : base()
        {
        }
        public Relationship(Guid user_one_id, Guid user_two_id, Guid action_user_id, StatusRS status)
        {
            User_one_id = user_one_id;
            User_two_id = user_two_id;
            Action_user_id = action_user_id;
            Status = status;
        }
        public Relationship(int id, Guid user_one_id, Guid user_two_id, Guid action_user_id, StatusRS status)
        {
            Id = id;
            User_one_id = user_one_id;
            User_two_id = user_two_id;
            Action_user_id = action_user_id;
            Status = status;
        }
        public DateTime DateCreated { get; set; }
        [Required]
        public Guid User_one_id { set; get; }
        [Required]
        public Guid User_two_id { set; get; }
        [Required]
        public Guid Action_user_id { set; get; }
        public StatusRS Status { set; get; }
        public DateTime DateModified { set; get; }
    }
}