using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Data.Enums;

namespace WebVuiVN.Application.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Text { get; set; }
        [MaxLength(200)]
        public string Image { set; get; }
        public int? ViewCount { set; get; }
        public int CategoryId { get; set; }
        public DateTime DateCreated { get; set; }
        public Status Status { set; get; }
        public string Tags { get; set; }
        public List<PostTagViewModel> PostTags { set; get; }
        public PostCategoryViewModel PostCategory { set; get; }
        public DateTime DateModified { set; get; }

        public Guid? UserId { set; get; }

    }
}
