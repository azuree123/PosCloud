using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    [Authorize]
    public class TransMasterPaymentMethodController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        public TransMasterPaymentMethodController()
        {

        }
        public TransMasterPaymentMethodController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //TransMasterPaymentMethod

        public ActionResult TransMasterPaymentMethodList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.TransMasterPaymentMethodRepository.GetTransMasterPaymentMethods((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        [HttpGet]
        public ActionResult AddTransMasterPaymentMethod()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TransMasterPaymentMethodViewModel TransMasterPaymentMethod = new TransMasterPaymentMethodViewModel();
            ViewBag.edit = "AddTransMasterPaymentMethod";
            return View(TransMasterPaymentMethod);
        }
        [HttpPost]
        public ActionResult AddTransMasterPaymentMethod(TransMasterPaymentMethodViewModel TransMasterPaymentMethodMv)
        {

            ViewBag.edit = "AddTransMasterPaymentMethod";
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
                    TransMasterPaymentMethod TransMasterPaymentMethod = Mapper.Map<TransMasterPaymentMethod>(TransMasterPaymentMethodMv);
                    
                    _unitOfWork.TransMasterPaymentMethodRepository.AddTransMasterPaymentMethod(TransMasterPaymentMethod);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The transMasterPaymentMethod added successfully", AlertType.Success);
                    return RedirectToAction("TransMasterPaymentMethodList", "TransMasterPaymentMethod");
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

            return RedirectToAction("TransMasterPaymentMethodList", "TransMasterPaymentMethod");


        }
        [HttpGet]
        public ActionResult UpdateTransMasterPaymentMethod(int id)
        {
            ViewBag.edit = "UpdateTransMasterPaymentMethod";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TransMasterPaymentMethodViewModel TransMasterPaymentMethodMv =
                Mapper.Map<TransMasterPaymentMethodViewModel>(_unitOfWork.TransMasterPaymentMethodRepository.GetTransMasterPaymentMethodById(id, (int)user.StoreId));
            return View("AddTransMasterPaymentMethod", TransMasterPaymentMethodMv);
        }
        [HttpPost]
        public ActionResult UpdateTransMasterPaymentMethod(int id, TransMasterPaymentMethodViewModel TransMasterPaymentMethodMv)
        {
            ViewBag.edit = "UpdateTransMasterPaymentMethod";
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
                    TransMasterPaymentMethod TransMasterPaymentMethod = Mapper.Map<TransMasterPaymentMethod>(TransMasterPaymentMethodMv);
                    _unitOfWork.TransMasterPaymentMethodRepository.UpdateTransMasterPaymentMethod(id, TransMasterPaymentMethod, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The transMasterPaymentMethod updated successfully", AlertType.Success);
                    return RedirectToAction("TransMasterPaymentMethodList", "TransMasterPaymentMethod");

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

            return RedirectToAction("TransMasterPaymentMethodList", "TransMasterPaymentMethod");



        }
        public ActionResult DeleteTransMasterPaymentMethod(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterPaymentMethodRepository.DeleteTransMasterPaymentMethod(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The transMasterPaymentMethod deleted successfully", AlertType.Success);
                return RedirectToAction("TransMasterPaymentMethodList", "TransMasterPaymentMethod");
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

            return RedirectToAction("TransMasterPaymentMethodList", "TransMasterPaymentMethod");


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

    }
}