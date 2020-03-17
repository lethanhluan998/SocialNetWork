using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebVuiVN.Infrastructure.SharedKernel;

namespace WebVuiVN.Data.Entities
{
    [Table("PostImages")]
    public class PostImage : DomainEntity<int>
    {
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        [StringLength(250)]
        public string Path { get; set; }
        [StringLength(250)]
        public string Caption { get; set; }
    }
}
