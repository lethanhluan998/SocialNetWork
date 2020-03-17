using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Application.Interfaces;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Data.Entities;
using WebVuiVN.Infrastructure.Interfaces;

namespace WebVuiVN.Application.Implementation
{
    public class RoomChatService : IRoomChatService
    {
        private readonly UserManager<AppUser> _userManager;
        private IRepository<RoomChat, int> _roomChatRepository;
        private IRepository<Message, int> _messageRepository;
        private IRepository<AppUserChat, int> _appUserChatRepository;
        private IUnitOfWork _unitOfWork;
        public RoomChatService(UserManager<AppUser> userManager, IRepository<RoomChat, int> roomChatRepository, IRepository<Message, int> messageRepository, IRepository<AppUserChat, int> appUserChatRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roomChatRepository = roomChatRepository;
            _messageRepository = messageRepository;
            _appUserChatRepository = appUserChatRepository;
            _unitOfWork = unitOfWork;
        }

        public  RoomChatViewModel GetData(string userName, Guid idUserLogin)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var userLogin = _userManager.FindByIdAsync(idUserLogin.ToString()).Result;
            var query = _roomChatRepository.FindSingle(x => (x.Code.Contains(idUserLogin.ToString())) && (x.Code.Contains(user.Id.ToString())));
            var result = Mapper.Map<RoomChat, RoomChatViewModel>(query);
            //
            if (result == null)
            {
                string code = user.Id.ToString() + userLogin.Id.ToString();
                RoomChat room = new RoomChat();
                room.Paticipants.Add(new AppUserChat(user.Id, room.Id));
                room.Paticipants.Add(new AppUserChat(idUserLogin, room.Id));
                room.Code = code;
                _roomChatRepository.Add(room);
                _unitOfWork.Commit();
                result = Mapper.Map<RoomChat, RoomChatViewModel>(room);
            }
            var listMess = _messageRepository.FindAll(x => x.RomChat_Id == result.Id).ProjectTo<MessageViewModel>().ToList();

            foreach (var item in listMess)
                result.Messages.Add(item);

            if (result.Messages.Count() == 0)
                result.Messages.Add(new MessageViewModel(user.FullName, query.Id, user.Id, "text", "Gui loi chao di"));
            return result;
        }
        //public bool GetUserView(string userName)
        //{
        //    var user = _userManager.GetUserAsync(User);
        //}
        public void addMess(string userName, string message,int idRoom)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            var query = _roomChatRepository.FindById(idRoom);
            if(query!=null)
            {
                _messageRepository.Add(new Message(user.FullName,idRoom,user.Id,"test",message));
            }
            
         //   var mess = _roomChatRepository.FindById();
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
