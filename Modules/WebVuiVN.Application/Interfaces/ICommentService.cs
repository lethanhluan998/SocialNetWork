using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Utilities;

namespace WebVuiVN.Application.Interfaces
{
    public interface ICommentService
    {
        CommentViewModel Add(CommentViewModel cmVm);
        void Delete(int id);
        List<CommentViewModel> GetAll();
        PagedResult<CommentViewModel> GetAllPaging(string keyword, int pageSize, int page = 1);
        CommentViewModel GetById(int id);
        void Update(CommentViewModel cm);
        List<CommentViewModel> GetLastest(int top);
        List<CommentViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);
    }
}
