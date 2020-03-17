using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebVuiVN.Application.ViewModels
{
    public class CommentTagViewModel
    {
        public int CommentId { set; get; }

        [StringLength(50)]
        public string TagId { set; get; }
    }
}
