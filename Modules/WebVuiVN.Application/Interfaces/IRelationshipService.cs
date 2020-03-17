using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Application.ViewModels.System;
using WebVuiVN.Data.Entities;

namespace WebVuiVN.Application.Interfaces
{
    public interface IRelationshipService
    {
       Task<bool> AddFriendAsync(Guid user_one_id, string user_two_id);
       Task AcceptFriendAsync(Guid user_one_id, string user_two_id);
       Task UnFriendAsync(Guid user_one_id, string user_two_id);
       Task CancelRequestAsync(Guid user_one_id, string user_two_id);
       void Save();
       void Delete(int id);
       RelationShipViewModel GetByUser(AppUserViewModel user_one, string user_two_id);
       Task<List<AppUserViewModel>> GetlistFriend(string id_user);
    }
}
