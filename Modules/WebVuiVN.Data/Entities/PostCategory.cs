using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebVuiVN.Data.Enums;
using WebVuiVN.Data.Interface;
using WebVuiVN.Infrastructure.SharedKernel;

namespace WebVuiVN.Data.Entities
{
    [Table("PostCategories")]
    public class PostCategory : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public PostCategory()
        {
            Posts = new List<Post>();
        }

        public PostCategory(string name, string description, int? parentId,
            string image, Status status)
        {
            ParentId = parentId;
            Name = name;
            Description = description;
            Image = image;
            Status = status;
        }
        public string Name { get; set; }

        public string Description { get; set; }


        public string Image { get; set; }

        public int? ParentId { get; set; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public Status Status { set; get; }
      
        public virtual ICollection<Post> Posts { set; get; }
    }
}
