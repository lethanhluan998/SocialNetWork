using System;
using System.Collections.Generic;
using System.Text;

namespace WebVuiVN.Application.ViewModels
{
    public class AppUserChatViewModel
    {
        public AppUserChatViewModel(Guid appUserId, int roomChatId)
        {
            AppUserId = appUserId;
            RoomChatId = roomChatId;
        }
        public int Id { get; set; }
        public int RoomChatId { set; get; }
        public Guid AppUserId { set; get; }
    }
}
