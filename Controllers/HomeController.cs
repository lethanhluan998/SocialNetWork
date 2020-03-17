using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using WebVuiVN.Application.Interfaces;
using WebVuiVN.Application.ViewModels.System;
using WebVuiVN.Data.Entities;
using WebVuiVN.Data.Enums;
using WebVuiVN.Models.ViewModels;

namespace WebVuiVN.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private IPostService _postService;
        IConfiguration _configuration;
        private IPostCategoryService _postCategoryService;
        private IUserService _userService;
        private IRelationshipService _relationshipService;
        private readonly IStringLocalizer<HomeController> _localizer;
        public HomeController(UserManager<AppUser> userManager, IConfiguration configuration, IPostService postService, IStringLocalizer<HomeController> localizer, IUserService userService, IRelationshipService relationshipService)
        {
            _userManager = userManager;
            _postService = postService;
            _localizer = localizer;
            _configuration = configuration;
            _userService = userService;
            _relationshipService = relationshipService;
    }
       public async Task<IActionResult> Index()
        {
            var title = _localizer["Title"];
            var culture = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var homeVm = new HomeViewModel();
            homeVm.User= Mapper.Map<AppUser, AppUserViewModel>(user);
            homeVm.Title = "Home";
            homeVm.listFriend = await _relationshipService.GetlistFriend(user.Id.ToString());
            homeVm.LastestPosts = _postService.GetLastest(5,user.Id);
            ViewData["idUser"] = user.Id;
            return View(homeVm);
        }
        [HttpPost]
        public async Task<IActionResult> Index([Bind("GetPost")]HomeViewModel homeVm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
              //  throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                homeVm.LastestPosts = _postService.GetLastest(5, user.Id);
                return View(homeVm);
            }
            else if (user != null)
            {
                homeVm.GetPost.UserId = user.Id;
                homeVm.GetPost.Name = user.FullName;
                homeVm.GetPost.CategoryId = 1;
                homeVm.GetPost.Status = Status.Active;
                _postService.Add(homeVm.GetPost);
                _postService.Save();
            }
            homeVm.LastestPosts = _postService.GetLastest(5, user.Id);
            return View(homeVm);
        }
        [Route("search.cshtml")]
        [HttpPost]
        public async Task<IActionResult> Search(string keyword, string user, string posts, string groups, string photos, string all, string pages, int page = 1)
        {
            var user1 = await _userManager.GetUserAsync(User);
            ViewData["idUser"] = user1.Id;
            var catalog = new SearchResultViewModel();
            ViewData["BodyClass"] = "search-result-page";
            int? pageSize = 5;
            string sortBy = "lastest";
            catalog.PageSize = pageSize;
            catalog.SortType = sortBy;
            //if (pageSize == null)
            //    pageSize = _configuration.GetValue<int>("PageSize");
            if (user != null) {
                ViewData.Add(new KeyValuePair<string, object>("DataSearch", "user"));
                catalog.Data.Users = _userService.GetAllPagingAsync(keyword, page, pageSize.Value);
                catalog.Keyword = keyword;
                return View(catalog);
            }
            if (posts != null)
            {
                ViewData.Add(new KeyValuePair<string, object>("DataSearch", "posts"));
                catalog.Data.Posts = _postService.GetAllPaging(keyword, page, pageSize.Value);
                catalog.Keyword = keyword;
                return View(catalog);
            }
            ViewData.Add(new KeyValuePair<string, object>("DataSearch", "all"));
            catalog.Data.Users = _userService.GetAllPagingAsync(keyword, page, pageSize.Value);
            catalog.Data.Posts = _postService.GetAllPaging(keyword, page, pageSize.Value);
            catalog.Keyword = keyword;
            return View(catalog);
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
    }
}
