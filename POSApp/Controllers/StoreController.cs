using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Controllers
{
    public class StoreController : Controller
    {
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
            return View(_unitOfWork.StoreRepository.GetStores());
        }
        [HttpGet]
        public ActionResult AddStore()
        {
            ViewBag.edit = "AddStore";
            return View();
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
                }
                else
                {
                    Store store = Mapper.Map<Store>(storeVm);
                    _unitOfWork.StoreRepository.AddStore(store);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Store added successfully", AlertType.Success);
                    return RedirectToAction("StoresList", "Store");
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
            }

            return RedirectToAction("StoresList", "Store");


        }
        [HttpGet]
        public ActionResult UpdateStore(int id)
        {
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
                }
                else
                {
                    Store store = Mapper.Map<Store>(storeVm);
                    _unitOfWork.StoreRepository.UpdateStore(id, store);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Store updated successfully", AlertType.Success);
                    return RedirectToAction("StoresList", "Store");

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
            }

            return RedirectToAction("StoresList", "Store");


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
            }

            return RedirectToAction("StoresList", "Store");

        }
    }
}