using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Application.ViewModels.System;

namespace WebVuiVN.Application.ViewModels
{
    public class RoomChatViewModel
    {

        public int Id { get; set; }
        public  List<MessageViewModel> Messages { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public List<AppUserChatViewModel> Paticipants { set; get; }
    }
}
