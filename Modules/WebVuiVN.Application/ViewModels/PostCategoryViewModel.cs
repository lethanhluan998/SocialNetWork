using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Data.Enums;

namespace WebVuiVN.Application.ViewModels
{
    public class PostCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
        public int? ParentId { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
  
        public Status Status { set; get; }
        public ICollection<PostViewModel> Posts { set; get; }
    }
}
