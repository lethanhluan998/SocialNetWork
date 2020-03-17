using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Application.Interfaces;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Application.ViewModels.System;
using WebVuiVN.Data.Entities;
using WebVuiVN.Data.Enums;
using WebVuiVN.Infrastructure.Interfaces;
using WebVuiVN.Utilities;

namespace WebVuiVN.Application.Implementation
{
    public class RelationshipService : IRelationshipService
    {
        private readonly UserManager<AppUser> _userManager;
        private IRepository<Relationship, int> _relationshipRepository;
        private IUnitOfWork _unitOfWork;
        public RelationshipService(IRepository<Relationship, int> relationshipRepository, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _relationshipRepository = relationshipRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
       
        public RelationShipViewModel GetByUser(AppUserViewModel user_one, string user_two_id)
        {
            var user_two =  _userManager.FindByIdAsync(user_two_id).Result;
            var relationship = new Relationship()
            {
                User_one_id = user_one.Id,
                User_two_id = user_two.Id,
                Action_user_id = user_one.Id
            };
            var query = _relationshipRepository.FindSingle(x => x.User_one_id == relationship.User_one_id && x.User_two_id == relationship.User_two_id);
            if (query != null)
            {
                // gui thong tin request

                return Mapper.Map<Relationship, RelationShipViewModel>(query);
            }
            var query2 = _relationshipRepository.FindSingle(x => x.User_one_id == relationship.User_two_id && x.User_two_id == relationship.User_one_id);
            if (query2 != null)
            {
                // gui thong tin request
                return Mapper.Map<Relationship, RelationShipViewModel>(query2);
            }
            return null;
        }
        public async Task<bool> AddFriendAsync(Guid user_one_id, string user_two_id)
        {
            var user_two = await _userManager.FindByIdAsync(user_two_id);
            var relationship = new Relationship()
            {
                User_one_id = user_one_id,
                User_two_id = user_two.Id,
                Action_user_id = user_one_id,
                Status = StatusRS.Pending
            };
            var query = _relationshipRepository.FindSingle(x => x.User_one_id == relationship.User_one_id && x.User_two_id == relationship.User_two_id);
            if (query == null)
            {
                var query2 = _relationshipRepository.FindSingle(x => x.User_one_id == relationship.User_two_id && x.User_two_id == relationship.User_one_id);
                if (query2 == null)
                    _relationshipRepository.Add(relationship);
                // gui thong tin request
                return true;
            }
            return false;
        }
        public async Task AcceptFriendAsync(Guid user_one_id, string user_two_id)
        {
            var user_two = await _userManager.FindByIdAsync(user_two_id);
            var relationship = new Relationship()
            {
                User_one_id = user_two.Id,
                User_two_id = user_one_id,
                Action_user_id = user_two.Id,
                Status = StatusRS.Pending
            };
            var query = _relationshipRepository.FindSingle(x => x.User_one_id == relationship.User_one_id && x.User_two_id == relationship.User_two_id);
            if (query != null )
            {
                query.Status= StatusRS.Accepted;
                _relationshipRepository.Update(query);
                // hien thong bao request
            }
        }
        public async Task CancelRequestAsync(Guid user_one_id, string user_two_id)
        {
            var user_two = await _userManager.FindByIdAsync(user_two_id);
            var relationship = new Relationship()
            {
                User_one_id = user_one_id,
                User_two_id = user_two.Id,
                Action_user_id = user_one_id,
                Status = StatusRS.Pending
            };
            var query = _relationshipRepository.FindSingle(x => x.User_one_id == relationship.User_one_id && x.User_two_id == relationship.User_two_id);
            if (query != null)
            {
                _relationshipRepository.Remove(query);
                // hien thong bao request
            }
        }
        public async Task UnFriendAsync(Guid user_one_id, string user_two_id)
        {
            var user_two = await _userManager.FindByIdAsync(user_two_id);
            var relationship = new Relationship()
            {
                User_one_id = user_two.Id,
                User_two_id = user_one_id,
            };
            var query = _relationshipRepository.FindSingle(x => x.User_one_id == relationship.User_one_id && x.User_two_id == relationship.User_two_id);
            if (query != null)
            {
                _relationshipRepository.Remove(query);
                // hien thong bao request
            }
            var query2 = _relationshipRepository.FindSingle(x => x.User_one_id == relationship.User_two_id && x.User_two_id == relationship.User_one_id);
            if (query2 != null)
            {
                _relationshipRepository.Remove(query2);
                // hien thong bao request
            }
        }
        public async Task<List<AppUserViewModel>> GetlistFriend(string id_user)
        {
            var query = _relationshipRepository.FindAll(x =>( x.User_one_id.ToString() == id_user && x.Status==StatusRS.Accepted)||(x.User_two_id.ToString() == id_user && x.Status == StatusRS.Accepted));
            List<AppUserViewModel> list = new List<AppUserViewModel>();
            foreach (Relationship t in query) 
            {
                if(t.User_one_id.ToString()!=id_user)
                {
                    var user = await _userManager.FindByIdAsync(t.User_one_id.ToString());
                    list.Add(Mapper.Map<AppUser, AppUserViewModel>(user));
                }
                if (t.User_two_id.ToString() != id_user)
                {
                    var user = await _userManager.FindByIdAsync(t.User_two_id.ToString());
                    list.Add(Mapper.Map<AppUser, AppUserViewModel>(user));
                }
            }
            return  list;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
        public void Delete(int id)
        {
            _relationshipRepository.Remove(id);
        }
        
    }
}
