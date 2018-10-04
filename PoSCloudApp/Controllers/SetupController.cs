using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoSCloudApp.Core;

namespace PoSCloudApp.Controllers
{
    public class SetupController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public SetupController()
        {

        }

        public SetupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Setup
        public ActionResult DepartmentList()
        {
            
            return View(_unitOfWork.DepartmentRepository.GetDepartments());
        }
        public ActionResult DepartmentAdd()
        {

            return View();
        }
        public ActionResult DepartmentUpdate()
        {
            return View();
        }
        public ActionResult DepartmentDelete()
        {
            return View();
        }

        public ActionResult DesignationList()
        {
            return View(_unitOfWork.DesignationRepository.GetDesignations());
        }
        public ActionResult DesignationAdd()
        {

            return View();
        }
        public ActionResult DesignationUpdate()
        {
            return View();
        }
        public ActionResult DesignationDelete()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            return View(_unitOfWork.EmployeeRepository.GetEmployees());
        }
        public ActionResult EmployeeAdd()
        {

            return View();
        }
        public ActionResult EmployeeUpdate()
        {
            return View();
        }
        public ActionResult EmployeeDelete()
        {
            return View();
        }

        public ActionResult CustomerList()
        {
            return View(_unitOfWork.CustomerRepository.GetCustomers());
        }
        public ActionResult CustomerAdd()
        {

            return View();
        }
        public ActionResult CustomerUpdate()
        {
            return View();
        }
        public ActionResult CustomerDelete()
        {
            return View();
        }

        public ActionResult SupplierList()
        {
            return View(_unitOfWork.SupplierRepository.GetSuppliers());
        }
        public ActionResult SupplierAdd()
        {

            return View();
        }
        public ActionResult SupplierUpdate()
        {
            return View();
        }
        public ActionResult SupplierDelete()
        {
            return View();
        }

        public ActionResult StateList()
        {
            return View(_unitOfWork.StateRepository.GetStates());
        }
        public ActionResult StateAdd()
        {

            return View();
        }
        public ActionResult StateUpdate()
        {
            return View();
        }
        public ActionResult StateDelete()
        {
            return View();
        }
        public ActionResult CityList(int stateId)
        {
            return View(_unitOfWork.CityRepository.GetCities(stateId));
        }
        public ActionResult CityAdd()
        {

            return View();
        }
        public ActionResult CityUpdate()
        {
            return View();
        }
        public ActionResult CityDelete()
        {
            return View();
        }

        public ActionResult LocationList()
        {
            return View(_unitOfWork.LocationRepository.GetLocations());
        }
        public ActionResult LocationAdd()
        {

            return View();
        }
        public ActionResult LocationUpdate()
        {
            return View();
        }
        public ActionResult LocationDelete()
        {
            return View();
        }

    }
}