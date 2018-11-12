using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: User
        public ActionResult UserList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<UserViewModel[]>(_unitOfWork.UserRepository.GetUsers((int)user.StoreId)));
        }
        [HttpGet]
        public ActionResult UpdateUser(string id)
        {
            ViewBag.edit = "UpdateUser";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            UserViewModel userMv =
                Mapper.Map<UserViewModel>(_unitOfWork.UserRepository.GetUserById(id, (int)user.StoreId));
            return View(userMv);
        }
        [HttpPost]
        public ActionResult UpdateUser(string id, UserViewModel userMv)
        {
           
            ViewBag.edit = "UpdateUser";
            if (!ModelState.IsValid)
            {
                return View(userMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ApplicationUser discount = Mapper.Map<ApplicationUser>(userMv);
                _unitOfWork.UserRepository.UpdateUser(id, discount, (int)user.StoreId);
                _unitOfWork.Complete();
                return RedirectToAction("UserList", "User");
            }

        }
        public ActionResult DeleteUser(string id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            string[] roles = UserManager.GetRoles(id).ToArray();
            UserManager.RemoveFromRoles(id, roles);
            _unitOfWork.UserRepository.DeleteUser(id, (int)user.StoreId);
            _unitOfWork.Complete();
            return RedirectToAction("UserList", "User");
        }
    }
}