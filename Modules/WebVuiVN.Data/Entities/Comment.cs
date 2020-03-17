using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebVuiVN.Data.Enums;
using WebVuiVN.Data.Interface;
using WebVuiVN.Infrastructure.SharedKernel;

namespace WebVuiVN.Data.Entities
{
    
    [Table("Comments")]
    public class Comment : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public Comment() 
        {
            CommentTags = new List<CommentTag>();
        }
        public Comment(string content, string images, int sticker, string reply, int postId) 
        {
            Content = content;
            Images = images;
            Sticker = sticker;
            Reply = reply;
            PostId = postId;
            CommentTags = new List<CommentTag>();
        }

        public string Content { get; set; }
        public string Images { get; set; }
        public int Sticker { get; set; }
        public string Reply { get; set; }
        public int PostId { set; get; }

        [ForeignKey("PostId")]
        public virtual Post Post { set; get; }

        public virtual ICollection<CommentTag> CommentTags { set; get; }
        public DateTime DateModified { set; get; }
        public DateTime DateCreated { get; set; }
        public Status Status { set; get; }
    }

}
