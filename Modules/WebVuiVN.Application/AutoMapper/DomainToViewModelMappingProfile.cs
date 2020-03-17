using System;
using AutoMapper;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Data.Entities;
using WebVuiVN.Application.ViewModels.System;

namespace WebVuiVN.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<PostCategory, PostCategoryViewModel>();
            CreateMap<Post,PostViewModel>();
            CreateMap<PostTag, PostTagViewModel>().MaxDepth(2);
            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentTag, CommentTagViewModel>().MaxDepth(2);
            CreateMap<RoomChat, RoomChatViewModel>();
            CreateMap<Message, MessageViewModel>();
            CreateMap<Relationship, RelationShipViewModel>();
            CreateMap<AppUserChat, AppUserChatViewModel>();
        }
    }
}
