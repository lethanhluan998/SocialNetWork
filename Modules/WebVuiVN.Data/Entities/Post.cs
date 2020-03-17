using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Data.Enums;
using WebVuiVN.Data.Interface;
using WebVuiVN.Infrastructure.SharedKernel;

namespace WebVuiVN.Data.Entities
{
    [Table("Posts")]
    public class Post : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public Post() 
        {
            PostTags = new List<PostTag>();
            Comments= new List<Comment>();
        }
        public Post(int id,string name, string tags, string text,string image,int categoryId, Status status, Guid? userId)
        {
            Id = id;
            Name = name;
            Text = text;
            Image = image;
            Tags = tags;
            CategoryId = categoryId;
            UserId = userId;
            Status = status;
            PostTags = new List<PostTag>();
            Comments = new List<Comment>();
        }
        public Post(string name, string text, string tags, string image, int categoryId, Status status, Guid? userId)
        {
            Tags = tags;
            Name = name;
            Text = text;
            Image = image;
            CategoryId = categoryId;
            Status = status;
            UserId = userId;
            PostTags = new List<PostTag>();
            Comments = new List<Comment>();
        }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Text { get; set; }
        [MaxLength(200)]
        public string Image { set; get; }
        public int? ViewCount { set; get; }
        public int CategoryId { get; set; }
        public Guid? UserId { set; get; }
        public DateTime DateCreated { get; set; }
        public Status Status { set; get; }
        public string Tags { get; set; }
        public virtual ICollection<PostTag> PostTags { set; get; }
        public virtual ICollection<Comment> Comments { set; get; }
        [ForeignKey("CategoryId")]
        public virtual PostCategory PostCategory { set; get; }
        [ForeignKey("UserId")]
        public virtual AppUser AppUser { set; get; }
        public DateTime DateModified { set; get; }
    }
}
