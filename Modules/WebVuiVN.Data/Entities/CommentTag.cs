using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebVuiVN.Data.Entities
{
    public class CommentTag : DomainEntity<int>
    {
        public int CommentId { set; get; }

        [StringLength(50)]
        public string TagId { set; get; }

        [ForeignKey("CommentId")]
        public virtual Comment Comment { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
