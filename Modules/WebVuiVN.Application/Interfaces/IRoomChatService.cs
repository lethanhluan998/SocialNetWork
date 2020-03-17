using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels;

namespace WebVuiVN.Application.Interfaces
{
    public interface IRoomChatService
    {
        RoomChatViewModel GetData(string userName, Guid idUserLogin);
        void Save();
        void addMess(string userName, string message,int idRoom);
    }
}
