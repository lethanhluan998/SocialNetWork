using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebVuiVN.Data.Interface;
using WebVuiVN.Infrastructure.SharedKernel;

namespace WebVuiVN.Data.Entities
{
    [Table("Messages")]
    public class Message: DomainEntity<int> ,IDateTracking
    {
     
        public Message(string fullName, int romChat_Id, Guid sender_id, string message_type, string message_text)
        {
            RomChat_Id = romChat_Id;
            Sender_id = sender_id;
            Message_type = message_type;
            Message_text = message_text;
            FullName = fullName;
        }

        [Required]
        public int RomChat_Id { set; get; }
        [Required]
        public Guid Sender_id { set; get; }
        public string FullName { set; get; }
        public string Message_type { set; get; }
        public string Message_text { set; get; }
        public string Attach_url { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        [ForeignKey("RomChat_Id")]
        public virtual RoomChat RoomChat { set; get; }
    }
}
