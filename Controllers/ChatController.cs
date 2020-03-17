using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVuiVN.Application.Interfaces;
using WebVuiVN.ChatHubs;
using WebVuiVN.Data.Entities;
using WebVuiVN.Models.ViewModels.ChatViewModels;

namespace WebVuiVN.Controllers
{
    
    public class ChatController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IRoomChatService _roomChatService;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(UserManager<AppUser> userManager, IUserService userService, IRoomChatService roomChatService, IHubContext<ChatHub> hubContext) 
        {
            _userService = userService;
            _userManager = userManager;
            _roomChatService = roomChatService;
            _hubContext = hubContext;

        }
        #region AJAX API
        [HttpGet]
        public IActionResult ActionMessage(string userName)
        {
             var user =  _userManager.GetUserAsync(User).Result;
            
            var query = _roomChatService.GetData(userName, user.Id);//
            
            var result = new OneRoomChatViewModel();
            result.RoomChatViewModel = query;
            foreach (var item in query.Paticipants)
            {
                string fullName = _userService.GetById(item.AppUserId.ToString()).Result.FullName;
                result.FullNams.Add(fullName);
            }
            result.MyName = user.FullName;
            
            return new OkObjectResult(result);
        }
        #endregion AJAX API
        [Produces("application/json")]
        [HttpPost]
        public async Task LoadMessage(string userName,string message,string idRoom)
        {
            var idRoomChat = int.Parse(idRoom);
            if (userName == _userManager.GetUserAsync(User).Result.UserName)
            {
                _roomChatService.addMess(userName,message, idRoomChat);
                _roomChatService.Save();
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", userName, message, idRoom);
            }else
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", userName, "User or IdRoom not found", idRoom);
        }
        [HttpPost]
        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await _userManager.GetUserAsync(User);
            string sent = "";
            if (user.UserName == userName)
                sent = "sender";
            else sent = "receiveder";
            return new OkObjectResult(sent);
        }
    }
}
