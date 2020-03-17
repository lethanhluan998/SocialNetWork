using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebVuiVN.Application.Interfaces;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Data.Entities;
using WebVuiVN.Data.Enums;
using WebVuiVN.Infrastructure.Interfaces;

namespace WebVuiVN.Application.Implementation
{
    public class PostCategoryService : IPostCategoryService
    {
        private IRepository<PostCategory, int> _postCategoryRepository;
        private IUnitOfWork _unitOfWork;
        public PostCategoryService(IRepository<PostCategory, int> postCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _postCategoryRepository = postCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        public PostCategoryViewModel Add(PostCategoryViewModel postCategoryVm)
        {
            var postCategory = Mapper.Map<PostCategoryViewModel, PostCategory>(postCategoryVm);
            _postCategoryRepository.Add(postCategory);
            return postCategoryVm;
        }
        public void Delete(int id)
        {
            _postCategoryRepository.Remove(id);
        }
        public List<PostCategoryViewModel> GetAll()
        {
            return _postCategoryRepository.FindAll().OrderBy(x => x.ParentId)
                 .ProjectTo<PostCategoryViewModel>().ToList();
        }
        public List<PostCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _postCategoryRepository.FindAll(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword))
                    .OrderBy(x => x.ParentId).ProjectTo<PostCategoryViewModel>().ToList();
            else
                return _postCategoryRepository.FindAll().OrderBy(x => x.ParentId)
                    .ProjectTo<PostCategoryViewModel>()
                    .ToList();
        }
        public List<PostCategoryViewModel> GetAllByParentId(int parentId)
        {
            return _postCategoryRepository.FindAll(x => x.Status == Status.Active
            && x.ParentId == parentId)
             .ProjectTo<PostCategoryViewModel>()
             .ToList();
        }

        public PostCategoryViewModel GetById(int id)
        {
            return Mapper.Map<PostCategory, PostCategoryViewModel>(_postCategoryRepository.FindById(id));
        }

        //public List<PostCategoryViewModel> GetHomeCategories(int top)
        //{
        //    var query = _postCategoryRepository
        //        .FindAll(x => x.HomeFlag == true, c => c.Products)
        //          .OrderBy(x => x.HomeOrder)
        //          .Take(top).ProjectTo<PostCategoryViewModel>();

        //    var categories = query.ToList();
        //    foreach (var category in categories)
        //    {
        //        //category.Products = _productRepository
        //        //    .FindAll(x => x.HotFlag == true && x.CategoryId == category.Id)
        //        //    .OrderByDescending(x => x.DateCreated)
        //        //    .Take(5)
        //        //    .ProjectTo<ProductViewModel>().ToList();
        //    }
        //    return categories;
        //}

        //public void ReOrder(int sourceId, int targetId)
        //{
        //    var source = _postCategoryRepository.FindById(sourceId);
        //    var target = _postCategoryRepository.FindById(targetId);
        //    int tempOrder = source.SortOrder;
        //    source.SortOrder = target.SortOrder;
        //    target.SortOrder = tempOrder;

        //    _productCategoryRepository.Update(source);
        //    _productCategoryRepository.Update(target);
        //}

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PostCategoryViewModel postCategoryVm)
        {
            var postCategory = Mapper.Map<PostCategoryViewModel, PostCategory>(postCategoryVm);
            _postCategoryRepository.Update(postCategory);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            var sourceCategory = _postCategoryRepository.FindById(sourceId);
            sourceCategory.ParentId = targetId;
            _postCategoryRepository.Update(sourceCategory);

            //Get all sibling
            var sibling = _postCategoryRepository.FindAll(x => items.ContainsKey(x.Id));
            foreach (var child in sibling)
            {
             //   child.SortOrder = items[child.Id];
                _postCategoryRepository.Update(child);
            }
        }
    }
}
