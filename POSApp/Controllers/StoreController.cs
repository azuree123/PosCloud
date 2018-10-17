using System;
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
            if (!ModelState.IsValid)
            {
                return View(storeVm);
            }
            else
            {
                Store store = Mapper.Map<Store>(storeVm);
                _unitOfWork.StoreRepository.AddStore(store);
                _unitOfWork.Complete();
                return RedirectToAction("StoresList", "Store");
            }

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
            if (!ModelState.IsValid)
            {
                return View("AddStore", storeVm);
            }
            else
            {
                Store store = Mapper.Map<Store>(storeVm);
                _unitOfWork.StoreRepository.UpdateStore(id, store);
                _unitOfWork.Complete();
                return RedirectToAction("StoresList", "Store");

            }

        }
        public ActionResult DeleteStore(int id)
        {
            _unitOfWork.StoreRepository.DeleteStore(id);
            _unitOfWork.Complete();
            return RedirectToAction("StoresList", "Store");
        }
    }
}