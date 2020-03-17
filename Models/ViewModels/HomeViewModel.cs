using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Application.ViewModels.System;

namespace WebVuiVN.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<PostViewModel> LastestPosts { get; set; }
        public string Title { set; get; }
        public PostViewModel GetPost { get; set; }
        public AppUserViewModel User {get;set;}
        public List<AppUserViewModel> listFriend { get; set; }
    }
}
