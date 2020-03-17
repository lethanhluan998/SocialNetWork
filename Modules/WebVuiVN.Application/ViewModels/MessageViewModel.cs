using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebVuiVN.Data.Entities;

namespace WebVuiVN.Application.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel(string fullName, int romChat_Id, Guid sender_id, string message_type, string message_text)
        {
            RomChat_Id = romChat_Id;
            Sender_id = sender_id;
            FullName = fullName;
            Message_type = message_type;
            Message_text = message_text;

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
  //      public  RoomChatViewModel RoomChat { set; get; }
    }
}
