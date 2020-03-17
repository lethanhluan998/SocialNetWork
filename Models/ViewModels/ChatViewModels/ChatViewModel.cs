using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels;

namespace WebVuiVN.Models.ViewModels.ChatViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel(List<RoomChatViewModel> chatViewModels)
        {
            ChatViewModels = chatViewModels;
        }
        public ChatViewModel()
        {
            
        }
        public List<RoomChatViewModel> ChatViewModels { set; get; }
    }
}
