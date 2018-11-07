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
            if (!ModelState.IsValid)
            {
                return View(DeviceMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                Device Device = Mapper.Map<Device>(DeviceMv);
                Device.StoreId = (int)user.StoreId;
                _unitOfWork.DeviceRepository.AddDevice(Device);
                _unitOfWork.Complete();
                return RedirectToAction("DeviceList", "Device");
            }

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
            if (!ModelState.IsValid)
            {
                return View("AddDevice", DeviceMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                Device Device = Mapper.Map<Device>(DeviceMv);
                _unitOfWork.DeviceRepository.UpdateDevice(id, Device, (int)user.StoreId);
                _unitOfWork.Complete();
                return RedirectToAction("DeviceList", "Device");

            }

        }
        public ActionResult DeleteDevice(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.DeviceRepository.DeleteDevice(id, (int)user.StoreId);
            _unitOfWork.Complete();
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