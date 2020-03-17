using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Application.ViewModels.System;
using WebVuiVN.Utilities;

namespace WebVuiVN.Models.ViewModels
{
    public class ListSearchViewModel
    {
        public PagedResult<AppUserViewModel> Users { get; set; }
        public PagedResult<PostViewModel> Posts { get; set; }
    }
}
