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
        public ActionResult SecurityObjectList()
        {
            return View(Mapper.Map<SecurityObjectViewModel[]>(_unitOfWork.SecurityObjectRepository.GetSecurityObjects()));
        }

        public ActionResult AddSecurityObject()
        {
            ViewBag.edit = "AddSecurityObject";
            return View();
        }
        [HttpPost]
        public ActionResult AddSecurityObject(SecurityObjectViewModel securityObjectMv)
        {
            ViewBag.edit = "AddSecurityObject";
            if (!ModelState.IsValid)
            {
                return View(securityObjectMv);
            }
            else
            {
               SecurityObject securityObject = Mapper.Map<SecurityObject>(securityObjectMv);
                _unitOfWork.SecurityObjectRepository.AddSecurityObject(securityObject);
                _unitOfWork.Complete();
                return RedirectToAction("SecurityObjectList", "User");
            }

        }
        public ActionResult UpdateSecurityObject(int id)
        {
            ViewBag.edit = "UpdateSecurityObject";
            SecurityObjectViewModel securityObjectMv = Mapper.Map<SecurityObjectViewModel>(_unitOfWork.SecurityObjectRepository.GetSecurityObject(id));
            return View("AddSecurityObject", securityObjectMv);
        }
        [HttpPost]
        public ActionResult UpdateSecurityObject(int id,SecurityObjectViewModel securityObjectMv)
        {
            ViewBag.edit = "UpdateSecurityObject";
            if (!ModelState.IsValid)
            {
                return View("AddSecurityObject", securityObjectMv);
            }
            else
            {
               SecurityObject securityObject = Mapper.Map<SecurityObject>(securityObjectMv);
                _unitOfWork.SecurityObjectRepository.UpdateSecurityObject(id, securityObject);
                _unitOfWork.Complete();
                return RedirectToAction("SecurityObjectList", "User");
            }

        }
        public ActionResult DeleteSecurityObject(int id)
        {
            _unitOfWork.SecurityObjectRepository.DeleteSecurityObject(id);
            _unitOfWork.Complete();
            return RedirectToAction("SecurityObjectList", "User");
        }
    }
}