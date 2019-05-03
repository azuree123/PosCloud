using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.SecurityFilters;

namespace POSApp.Controllers
{
    [Authorize]
    public class UserController : LanguageController
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
            return View(Mapper.Map<UserViewModel[]>(_unitOfWork.UserRepository.GetUsers((int)user.StoreId).Where(a=>!a.IsDisabled).OrderByDescending(a => a.Id)));
        }
        [HttpGet]
        public ActionResult UpdateUser(string id)
        {
            ViewBag.edit = "UpdateUser";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            UserViewModel userMv =
                Mapper.Map<UserViewModel>(_unitOfWork.UserRepository.GetUserById(id));
            return View(userMv);
        }
        [HttpPost]
        public ActionResult UpdateUser(string id, UserViewModel userMv)
        {
           
            ViewBag.edit = "UpdateUser";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    ApplicationUser discount = Mapper.Map<ApplicationUser>(userMv);
                    _unitOfWork.UserRepository.UpdateUser(id, discount, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The user updated successfully", AlertType.Success);
                    return RedirectToAction("UserList", "User");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("UserList", "User");



        }
        public ActionResult DeleteUser(string id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                string[] roles = UserManager.GetRoles(id).ToArray();
                UserManager.RemoveFromRoles(id, roles);
                _unitOfWork.UserRepository.DeleteUser(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The user deleted successfully", AlertType.Success);
                return RedirectToAction("UserList", "User");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

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
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {
                    SecurityObject securityObject = Mapper.Map<SecurityObject>(securityObjectMv);
                    _unitOfWork.SecurityObjectRepository.AddSecurityObject(securityObject);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The security object added successfully", AlertType.Success);
                    return RedirectToAction("SecurityObjectList", "User");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("SecurityObjectList", "User");



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
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                }
                else
                {
                    SecurityObject securityObject = Mapper.Map<SecurityObject>(securityObjectMv);
                    _unitOfWork.SecurityObjectRepository.UpdateSecurityObject(id, securityObject);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The security object updated successfully", AlertType.Success);
                    return RedirectToAction("SecurityObjectList", "User");
                }
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("SecurityObjectList", "User");


        }
        public ActionResult DeleteSecurityObject(int id)
        {
            try
            {
                _unitOfWork.SecurityObjectRepository.DeleteSecurityObject(id);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The security object deleted successfully", AlertType.Success);
                return RedirectToAction("SecurityObjectList", "User");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("SecurityObjectList", "User");

        }
        [HttpPost]
        public JsonResult GetDdlEnum(Config.SecurityRights id)
        {
            try
            {
                IList<SelectListItem> send = new List<SelectListItem>();
                if (id == Config.SecurityRights.SaleOrders)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.SaleOrders));
                }
                else if (id == Config.SecurityRights.PurchaseOrders)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.PurchaseOrders));
                }
                else if (id == Config.SecurityRights.Expense)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.Expense));
                }
                else if (id == Config.SecurityRights.ExcelImport)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.ExcelImport));
                }
                else if (id == Config.SecurityRights.Device)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.Device));
                }
                else if (id == Config.SecurityRights.OpeningStock)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.OpeningStock));
                }
                else if (id == Config.SecurityRights.Reports)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.Reports));
                }
                else if (id == Config.SecurityRights.Setup)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.Setup));
                }
                else if (id == Config.SecurityRights.PointOfSale)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.PointOfSale));
                }
                else if (id == Config.SecurityRights.Account)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.Account));
                }
                else if (id == Config.SecurityRights.StockTaking)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.StockTaking));
                }
                else if (id == Config.SecurityRights.Store)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.Store));
                }
                else if (id == Config.SecurityRights.User)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.User));
                }
                else if (id == Config.SecurityRights.Products)
                {
                    send = EnumHelper.GetSelectList(typeof(Config.Products));
                }
                else { }
                return Json(send, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}