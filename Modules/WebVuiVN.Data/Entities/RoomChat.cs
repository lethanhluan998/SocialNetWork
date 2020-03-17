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
    [Table("RoomChats")]
    public class RoomChat :DomainEntity<int> ,IDateTracking
    {
        public RoomChat()
        {
            Paticipants = new List<AppUserChat>();
             Code = "";
            Messages = new List<Message>();
        }

    

        public string Code { set; get; }
        public virtual ICollection<Message> Messages { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        
        public virtual ICollection<AppUserChat> Paticipants { set; get; }
    }
}
