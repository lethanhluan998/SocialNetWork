using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVuiVN.Application.Interfaces;
using WebVuiVN.Application.ViewModels;
using WebVuiVN.Data.Enums;

namespace WebVuiVN.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private IPostService _postService;
        private IPostCategoryService _postCategoryService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public PostController(IPostService postService,
            IHostingEnvironment hostingEnvironment)
        {
            _postService = postService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var model = _postService.GetAll();
            ViewBag.ListPost = model;
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Update(int id)
        {
            var model = _postService.GetById(id);
            ViewBag.Post = model;
            return View();
        }
        #region AJAX API

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _postService.GetAll();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _postService.GetById(id);

            return new OkObjectResult(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _postService.Delete(id);
                _postService.Save();
                //  return new OkObjectResult(id);
                return RedirectToAction("Index", "Post");
            }
        }
        [HttpPost]
        public IActionResult SaveEntity(PostViewModel postVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                //postVm.SeoAlias = TextHelper.ToUnsignString(postVm.Name);
                if (postVm.Id == 0)
                {
                    postVm.Status = Status.Active;
                    _postService.Add(postVm);
                }
                else
                {
                    _postService.Update(postVm);
                }
                _postService.Save();
                // return new OkObjectResult(postVm);
                return RedirectToAction("Index", "Post");
            }
        }
        [HttpPost]
        public IActionResult SaveImages(int postID, string[] images)
        {
            _postService.AddImages(postID, images);
            _postService.Save();
            return new OkObjectResult(images);
        }
        [HttpGet]
        public IActionResult GetImages(int postId)
        {
            var images = _postService.GetImages(postId);
            return new OkObjectResult(images);
        }
        #endregion AJAX API
    }
}
