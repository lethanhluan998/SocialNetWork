using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebVuiVN.Infrastructure.SharedKernel;

namespace WebVuiVN.Data.Entities
{
    public class PostTag : DomainEntity<int>
    {
        public int PostId { set; get; }

        [StringLength(50)]
        public string TagId { set; get; }

        [ForeignKey("PostId")]
        public virtual Post Post { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
