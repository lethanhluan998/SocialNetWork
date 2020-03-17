using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Data.Entities;
using WebVuiVN.Utilities;

namespace WebVuiVN.Application.Interfaces
{
    public interface IPostService
    {
        List<PostViewModel> GetAll();
        PostViewModel Add(PostViewModel post);

        void Update(PostViewModel post);

        void Delete(int id);
        PagedResult<PostViewModel> GetAllPaging(string keyword, int page , int pageSize);
        PostViewModel GetById(int id);
        void AddImages(int postID, string[] images);
        List<PostImageViewModel> GetImages(int postID);
        List<PostViewModel> GetLastest(int top, Guid userId);
        List<PostViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow);
        List<string> GetListByName(string name);
        List<PostViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow);
        List<TagViewModel> GetListTagById(int id);
        void IncreaseView(int id);
        List<PostViewModel> GetListByTag(string tagId, int page, int pageSize, out int totalRow);
        List<PostViewModel> GetList(string keyword);
        List<TagViewModel> GetListTag(string searchText);
        void Save();
    }
}
