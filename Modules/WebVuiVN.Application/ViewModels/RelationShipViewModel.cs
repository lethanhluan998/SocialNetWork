using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebVuiVN.Data.Enums;

namespace WebVuiVN.Application.ViewModels
{
    public class RelationShipViewModel
    {
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
