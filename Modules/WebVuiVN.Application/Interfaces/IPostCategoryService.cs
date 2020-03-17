using System;
using System.Collections.Generic;
using System.Text;
using WebVuiVN.Application.ViewModels;

namespace WebVuiVN.Application.Interfaces
{
    public interface IPostCategoryService
    {
        PostCategoryViewModel Add(PostCategoryViewModel postCategoryVm);
        void Delete(int id);
        List<PostCategoryViewModel> GetAll();
        List<PostCategoryViewModel> GetAll(string keyword);
        List<PostCategoryViewModel> GetAllByParentId(int parentId);
        PostCategoryViewModel GetById(int id);
        void Save();
        void Update(PostCategoryViewModel postCategoryVm);
        void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items);
    }
}
