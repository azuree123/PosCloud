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
    public class DeviceController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        public DeviceController()
        {
            
        }
        public DeviceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Device

        public ActionResult DeviceList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DeviceRepository.GetDevices((int)user.StoreId));
        }
        [HttpGet]
        public ActionResult AddDevice()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DeviceViewModel Device = new DeviceViewModel();
            ViewBag.edit = "AddDevice";
            return View(Device);
        }
        [HttpPost]
        public ActionResult AddDevice(DeviceViewModel DeviceMv)
        {

            ViewBag.edit = "AddDevice";
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
                    Device Device = Mapper.Map<Device>(DeviceMv);
                    Device.StoreId = (int)user.StoreId;
                    _unitOfWork.DeviceRepository.AddDevice(Device);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The device added successfully", AlertType.Success);
                    return RedirectToAction("DeviceList", "Device");
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

            return RedirectToAction("DeviceList", "Device");


        }
        [HttpGet]
        public ActionResult UpdateDevice(int id)
        {
            ViewBag.edit = "UpdateDevice";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DeviceViewModel DeviceMv =
                Mapper.Map<DeviceViewModel>(_unitOfWork.DeviceRepository.GetDeviceById(id, (int)user.StoreId));
            return View("AddDevice", DeviceMv);
        }
        [HttpPost]
        public ActionResult UpdateDevice(int id, DeviceViewModel DeviceMv)
        {
            ViewBag.edit = "UpdateDevice";
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
                    Device Device = Mapper.Map<Device>(DeviceMv);
                    _unitOfWork.DeviceRepository.UpdateDevice(id, Device, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The device updated successfully", AlertType.Success);
                    return RedirectToAction("DeviceList", "Device");

                }
            }
            catch(DbEntityValidationException ex)
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

            return RedirectToAction("DeviceList", "Device");



        }
        public ActionResult DeleteDevice(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.DeviceRepository.DeleteDevice(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The device deleted successfully", AlertType.Success);
                return RedirectToAction("DeviceList", "Device");
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

            return RedirectToAction("DeviceList", "Device");


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