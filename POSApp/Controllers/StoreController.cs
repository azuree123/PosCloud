using System;
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
    public class StoreController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public StoreController()
        {

        }

        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult StoresList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

            


            var store = _unitOfWork.StoreRepository.GetStoreById((int)user.StoreId);
            var clientStores = _unitOfWork.ClientRepository.GetClientStore((int)store.ClientId);
            return View(clientStores);
        }

        public ActionResult AddStorePartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("StoresList");
            }
            ViewBag.edit = "AddStorePartial";
            return View();
        }
        [HttpPost]
        public ActionResult AddStorePartial(StoreViewModel storevm)
        {
            ViewBag.edit = "AddStorePartial";
            if (!ModelState.IsValid)
            {
                return View(storevm);
            }
            else
            {
                Store store = Mapper.Map<Store>(storevm);
                _unitOfWork.StoreRepository.AddStore(store);
                _unitOfWork.Complete();
                return PartialView("Test");
            }

        }

        [HttpGet]
        public ActionResult AddStore()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("StoresList");
            }
            ViewBag.edit = "AddStore";
            return View(new StoreViewModel());
        }
        [HttpPost]
        public ActionResult AddStore(StoreViewModel storeVm)
        {
            ViewBag.edit = "AddStore";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(storeVm);
                }
                else
                {
                    Store store = Mapper.Map<Store>(storeVm);
                    _unitOfWork.StoreRepository.AddStore(store);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Store added successfully", AlertType.Success);
                    return null;
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

            return View(storeVm);


        }
        [HttpGet]
        public ActionResult UpdateStore(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("StoresList");
            }
            ViewBag.edit = "UpdateStore";
            StoreViewModel storeVm = Mapper.Map<StoreViewModel>(_unitOfWork.StoreRepository.GetStoreById(id));
            return View("AddStore", storeVm);
        }
        [HttpPost]
        public ActionResult UpdateStore(int id, StoreViewModel storeVm)
        {
            ViewBag.edit = "UpdateStore";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddStore",storeVm);
                }
                else
                {
                    Store store = Mapper.Map<Store>(storeVm);
                    _unitOfWork.StoreRepository.UpdateStore(id, store);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Store updated successfully", AlertType.Success);
                    return null;

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

            return View("AddStore", storeVm);


        }
        public ActionResult DeleteStore(int id)
        {
            try
            {
                _unitOfWork.StoreRepository.DeleteStore(id);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Store deleted successfully", AlertType.Success);
                return RedirectToAction("StoresList", "Store");
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

            return RedirectToAction("StoresList", "Store");

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
        public JsonResult GetStoreDdl()
        {
            try
            {
                return Json(Mapper.Map<StateModelView[]>(_unitOfWork.StoreRepository.GetStores()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}