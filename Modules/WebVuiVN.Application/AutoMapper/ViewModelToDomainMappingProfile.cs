using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Application.ViewModels.System;
using WebVuiVN.Data.Entities;

namespace WebVuiVN.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PostCategoryViewModel, PostCategory>()
                .ConstructUsing(c => new PostCategory(c.Name, c.Description,c.ParentId ,c.Image,c.Status));
            CreateMap<PostViewModel,Post>()
                .ConstructUsing(c => new Post(c.Name, c.Text, c.Tags, c.Image, c.CategoryId, c.Status,c.UserId));
            CreateMap<CommentViewModel, Comment>()
                .ConstructUsing(c => new Comment(c.Content, c.Images, c.Sticker, c.Reply, c.PostId));
            CreateMap<AppUserViewModel, AppUser>()
            .ConstructUsing(c => new AppUser(c.Id, c.FullName, c.UserName,
            c.Email, c.PhoneNumber, c.Avatar, c.Status));
            CreateMap<MessageViewModel, Message>()
            .ConstructUsing(c => new Message(c.FullName,c.RomChat_Id,c.Sender_id,c.Message_type,c.Message_text));
            CreateMap<RelationShipViewModel, Relationship>()
            .ConstructUsing(c => new Relationship(c.User_one_id, c.User_two_id, c.Action_user_id,c.Status));
            CreateMap<AppUserChatViewModel, AppUserChat>()
            .ConstructUsing(c => new AppUserChat(c.AppUserId, c.RoomChatId));
            CreateMap<RoomChatViewModel, RoomChat>()
            .ConstructUsing(c => new RoomChat());
        }
    }
}
