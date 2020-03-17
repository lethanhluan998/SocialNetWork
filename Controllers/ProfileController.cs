using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVuiVN.Application.Interfaces;
using WebVuiVN.Application.ViewModels.System;
using WebVuiVN.Areas.Admin.Controllers;
using WebVuiVN.Data.Entities;
using WebVuiVN.Data.Enums;
using WebVuiVN.Models.ViewModels.ProfileViewModels;

namespace WebVuiVN.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IUserService _userService;
        private readonly IRelationshipService _relationshipService;
        public ProfileController(UserManager<AppUser> userManager,IUserService userService, IRelationshipService relationshipService)
        {
            _userService = userService;
            _userManager = userManager;
            _relationshipService = relationshipService;
        }
        [Route("index.cshtml")]
        [HttpGet]
        public async Task<IActionResult> Index(string email)
        {
            var myUser = await _userManager.GetUserAsync(User);
            AppUserViewModel myUserVM =Mapper.Map<AppUser, AppUserViewModel>(myUser);
            var user = await _userService.GetByName(email);
           
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userService.GetByName(email)}'.");
            }
            var query = _relationshipService.GetByUser(myUserVM, user.Id.ToString());
            if (query == null)
            {
                ViewData["Status"] = "Add Friend";
                ViewData["Action"] = "AddFriend";
            }
            else if (query.Action_user_id == myUser.Id && query.Status == StatusRS.Pending)
            {
                ViewData["Status"] = "Đã gửi lời mời";
                ViewData["Action"] = "CancelRequest";
            }
            else if (query.Action_user_id == user.Id && query.Status == StatusRS.Pending)
            {
                ViewData["Status"] = "Chấp nhận";
                ViewData["Action"] = "AcceptFriend";
            }
            else if (query.Status == StatusRS.Accepted)
            {
                ViewData["Status"] = "Bạn bè";
                ViewData["Action"] = "UnFriend";
            }
            var model = new ProfileViewModel
            {
                Id =user.Id,
                Name = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                StatusMessage = "",
                BirthDay=user.BirthDay,
                Avatar=user.Avatar
            };
            return View(model);
        }
        #region AJAX API
        [HttpPost]
        public async Task<IActionResult> RelationShip(string id,string action)
        {
            var user = await _userManager.GetUserAsync(User);
            AppUserViewModel userVM = Mapper.Map<AppUser, AppUserViewModel>(user);
            var profile = await _userService.GetById(id);
            if (action == "AddFriend")
            {
                var query = await _relationshipService.AddFriendAsync(user.Id, id);
            }
            else if (action == "AcceptFriend")
            {
                await _relationshipService.AcceptFriendAsync(user.Id, id);
            }
            else if (action == "CancelRequest")
            {
                await _relationshipService.CancelRequestAsync(user.Id, id);
            }
            else if (action == "UnFriend")
            {
                await _relationshipService.UnFriendAsync(user.Id, id);
            }
            _relationshipService.Save();
            var result = _relationshipService.GetByUser(userVM, id).Status;
           
            return new OkObjectResult(result);
            //return RedirectToAction("Index", "Profile", new {/* routeValues, for example: */ email = profile.Email });
        }
        #endregion AJAX API
        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion
    }
}
