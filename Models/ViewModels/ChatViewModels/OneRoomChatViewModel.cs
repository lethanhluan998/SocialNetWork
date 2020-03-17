using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels;

namespace WebVuiVN.Models.ViewModels.ChatViewModels
{
    public class OneRoomChatViewModel
    {
        public OneRoomChatViewModel()
        {
            FullNams = new List<string>();
        }
        public string MyName { set; get; }
        public RoomChatViewModel RoomChatViewModel { set; get; }
        public List<string> FullNams { set; get; }
    }
}
