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
using WebVuiVN.Utilities;

namespace WebVuiVN.Application.Implementation
{
    public class CommentService : ICommentService
    {
        private IRepository<Comment, int> _commentRepository;
        private IRepository<Tag, string> _tagRepository;
        private IRepository<CommentTag, int> _commentTagRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IRepository<Comment, int> commentRepository, IRepository<Tag, string> tagRepository,
            IRepository<CommentTag, int> commentTagRepository, IUnitOfWork unitOfWork) 
        {
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
            _commentTagRepository = commentTagRepository;
            _unitOfWork = unitOfWork;
        }
        public CommentViewModel Add(CommentViewModel cmVm)
        {
            var cm = Mapper.Map<CommentViewModel, Comment>(cmVm);

            _commentRepository.Add(cm);
            return cmVm;
        }
        public void Delete(int id)
        {
            _commentRepository.Remove(id);
        }
        public List<CommentViewModel> GetAll()
        {
            return _commentRepository.FindAll(c => c.CommentTags)
                .ProjectTo<CommentViewModel>().ToList();
        }
        public PagedResult<CommentViewModel> GetAllPaging(string keyword, int pageSize, int page = 1)
        {
            var query = _commentRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Content.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<CommentViewModel>()
            {
                Results = data.ProjectTo<CommentViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize,
            };

            return paginationSet;
        }
        public CommentViewModel GetById(int id)
        {
            return Mapper.Map<Comment, CommentViewModel>(_commentRepository.FindById(id));
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public void Update(CommentViewModel cm)
        {
            _commentRepository.Update(Mapper.Map<CommentViewModel, Comment>(cm));
        }
        public List<CommentViewModel> GetLastest(int top)
        {
            return _commentRepository.FindAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<CommentViewModel>().ToList();
        }
        public List<CommentViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _commentRepository.FindAll(x => x.Status == Status.Active
            && x.Content.Contains(keyword));
            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CommentViewModel>()
                .ToList();
        }
    }
}
