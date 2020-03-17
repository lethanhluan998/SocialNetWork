using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using WebVuiVN.Application.Interfaces;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Data.Entities;
using WebVuiVN.Data.Enums;
using WebVuiVN.Infrastructure.Interfaces;
using WebVuiVN.Utilities;
using WebVuiVN.Utilities.Helpers;

namespace WebVuiVN.Application.Implementation
{
    public class PostService : IPostService
    {
        private IRepository<Post, int> _postRepository;
        private IRepository<Relationship, int> _relationshipRepository;
        private IRepository<Tag, string> _tagRepository;
        private IRepository<PostImage, int> _postImageRepository;
        private IRepository<PostTag, int> _postTagRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IRepository<Post, int> postRepository,
            IRepository<Tag, string> tagRepository,
            IRepository<PostTag, int> postTagRepository,
            IUnitOfWork unitOfWork, IRepository<Relationship, int> relationshipRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
            _unitOfWork = unitOfWork;
            _relationshipRepository = relationshipRepository;
        }
        public PostViewModel Add(PostViewModel postVm)
        {
            var post = Mapper.Map<PostViewModel, Post>(postVm);
            post.ViewCount = 0;
            if (!string.IsNullOrEmpty(post.Tags))
            {
                var tags = post.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = "Post"
                        };
                        _tagRepository.Add(tag);
                    }
                    var postTag = new PostTag { TagId = tagId };
                    post.PostTags.Add(postTag);
                }
            }
            _postRepository.Add(post);
            return postVm;
        }
        public void Delete(int id)
        {
            _postRepository.Remove(id);
        }
        public List<PostViewModel> GetAll()
        {
            return _postRepository.FindAll(c => c.PostTags)
                .ProjectTo<PostViewModel>().ToList();
        }
        public PagedResult<PostViewModel> GetAllPaging(string keyword, int page , int pageSize)
        {
            var query = _postRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            var paginationSet = new PagedResult<PostViewModel>()
            {
                Results = data.ProjectTo<PostViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize,
            };
            return paginationSet;
        }
        public PostViewModel GetById(int id)
        {
            return Mapper.Map<Post, PostViewModel>(_postRepository.FindById(id));
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public void Update(PostViewModel post)
        {
            _postRepository.Update(Mapper.Map<PostViewModel, Post>(post));
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = "Post"
                        };
                        _tagRepository.Add(tag);
                    }
                    _postTagRepository.RemoveMultiple(_postTagRepository.FindAll(x => x.Id == post.Id).ToList());
                    PostTag postTag = new PostTag
                    {
                        PostId = post.Id,
                        TagId = tagId
                    };
                    _postTagRepository.Add(postTag);
                }
            }
        }
        public List<PostImageViewModel> GetImages(int postId)
        {
            return _postImageRepository.FindAll(x => x.PostId == postId)
                .ProjectTo<PostImageViewModel>().ToList();
        }
        public void AddImages(int postId, string[] images)
        {
            _postImageRepository.RemoveMultiple(_postImageRepository.FindAll(x => x.PostId == postId).ToList());
            foreach (var image in images)
            {
                _postImageRepository.Add(new PostImage()
                {
                    Path = image,
                    PostId = postId,
                    Caption = string.Empty
                });
            }
        }
        public List<PostViewModel> GetLastest(int top, Guid userId)
        {
            var query= _postRepository.FindAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<PostViewModel>().ToList();
            var listFriend = _relationshipRepository.FindAll(x=>x.User_one_id==userId&&x.Status==StatusRS.Accepted);
            string[] idFriends;
            foreach (var item in listFriend)
            {
                query = query.FindAll(x => x.Status == Status.Active && (x.UserId == userId || x.UserId == item.User_two_id));
            }
            return query;
        }
        public List<PostViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow)
        {
            var query = _postRepository.FindAll(x => x.Status == Status.Active);
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<PostViewModel>().ToList();
        }
        public List<string> GetListByName(string name)
        {
            return _postRepository.FindAll(x => x.Status == Status.Active
            && x.Name.Contains(name)).Select(y => y.Name).ToList();
        }
        public List<PostViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _postRepository.FindAll(x => x.Status == Status.Active
            && x.Name.Contains(keyword));
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<PostViewModel>()
                .ToList();
        }
        public List<TagViewModel> GetListTagById(int id)
        {
            return _postTagRepository.FindAll(x => x.PostId == id, c => c.Tag)
                .Select(y => y.Tag)
                .ProjectTo<TagViewModel>()
                .ToList();
        }
        public void IncreaseView(int id)
        {
            var viewPost = _postRepository.FindById(id);
            if (viewPost.ViewCount.HasValue)
                viewPost.ViewCount += 1;
            else
                viewPost.ViewCount = 1;
        }
        public List<PostViewModel> GetListByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in _postRepository.FindAll()
                        join pt in _postTagRepository.FindAll()
                        on p.Id equals pt.PostId
                        where pt.TagId == tagId && p.Status == Status.Active
                        orderby p.DateCreated descending
                        select p;
            totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var model = query
                .ProjectTo<PostViewModel>();
            return model.ToList();
        }
        public List<PostViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _postRepository.FindAll(x => x.Name.Contains(keyword)).ProjectTo<PostViewModel>()
                : _postRepository.FindAll().ProjectTo<PostViewModel>();
            return query.ToList();
        }
        public List<TagViewModel> GetListTag(string searchText)
        {
            return _tagRepository.FindAll(x => x.Type == "Post"
            && searchText.Contains(x.Name)).ProjectTo<TagViewModel>().ToList();
        }
    }
}
