using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Data.Enums;

namespace WebVuiVN.Application.ViewModels
{
    public class CommentViewModel
    {
        public string Content { get; set; }
        public string Images { get; set; }
        public int Sticker { get; set; }
        public string Reply { get; set; }
        public int PostId { set; get; }
        public virtual List<CommentTagViewModel> CommentTags { set; get; }
        public DateTime DateModified { set; get; }
        public DateTime DateCreated { get; set; }
        public Status Status { set; get; }
    }
}
