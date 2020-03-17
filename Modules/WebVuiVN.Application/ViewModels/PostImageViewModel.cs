using System;
using System.Collections.Generic;
using System.Text;

namespace WebVuiVN.Application.ViewModels
{
    public class PostImageViewModel
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public PostViewModel Post { get; set; }

        public string Path { get; set; }

        public string Caption { get; set; }
    }
}
