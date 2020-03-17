using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebVuiVN.Infrastructure.SharedKernel;

namespace WebVuiVN.Data.Entities
{
    public class AppUserChat :DomainEntity<int>
    {
        public AppUserChat(Guid appUserId, int roomChatId)
        {
            AppUserId = appUserId;
            RoomChatId = roomChatId;
        }
        public int RoomChatId { set; get; }
        public RoomChat RoomChat { set; get; }
        public Guid AppUserId { set; get; }
        public AppUser AppUser { set; get; }
    }
}
