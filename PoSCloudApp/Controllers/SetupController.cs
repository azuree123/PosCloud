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
        [HttpGet]
        public ActionResult AddDepartment()
        {

            return View();
        }
        public ActionResult UpdateDepartment()
        {
            return View();
        }
        public ActionResult DeleteDepartment( int id)
        {
            _unitOfWork.DepartmentRepository.DeleteDepartment(id);
            _unitOfWork.Complete();
            return RedirectToAction("DepartmentList","Setup");
        }

        public ActionResult DesignationList()
        {
            return View(_unitOfWork.DesignationRepository.GetDesignations());
        }
        public ActionResult AddDesignation()
        {

            return View();
        }
        public ActionResult UpdateDesignation()
        {
            return View();
        }
        public ActionResult DeleteDesignation(int id)
        {
            _unitOfWork.DesignationRepository.DeleteDesignation(id);
            _unitOfWork.Complete();
            return RedirectToAction("DesignationList","Setup");
        }

        public ActionResult EmployeeList()
        {
            return View(_unitOfWork.EmployeeRepository.GetEmployees());
        }
        public ActionResult AddEmployee()
        {

            return View();
        }
        public ActionResult UpdateEmployee()
        {
            return View();
        }
        public ActionResult DeleteEmployee(int id)
        {
            _unitOfWork.EmployeeRepository.DeleteEmployee(id);
            _unitOfWork.Complete();
            return RedirectToAction("EmployeeList","Setup");
        }

        public ActionResult CustomerList()
        {
            return View(_unitOfWork.CustomerRepository.GetCustomers());
        }
        public ActionResult AddCustomer()
        {

            return View();
        }
        public ActionResult UpdateCustomer()
        {
            return View();
        }
        public ActionResult DeleteCustomer(int id)
        {
            _unitOfWork.CustomerRepository.DeleteCustomer(id);
            _unitOfWork.Complete();
            return RedirectToAction("CustomerList","Setup");
        }

        public ActionResult SupplierList()
        {
            return View(_unitOfWork.SupplierRepository.GetSuppliers());
        }
        public ActionResult AddSupplier()
        {

            return View();
        }
        public ActionResult UpdateSupplier()
        {
            return View();
        }
        public ActionResult DeleteSupplier(int id)
        {
            _unitOfWork.SupplierRepository.DeleteSupplier(id);
            _unitOfWork.Complete();
            return RedirectToAction("SupplierList","Setup");
        }

        public ActionResult StateList()
        {
            return View(_unitOfWork.StateRepository.GetStates());
        }
        public ActionResult AddState()
        {

            return View();
        }
        public ActionResult UpdateState()
        {
            return View();
        }
        public ActionResult DeleteState(int id)
        {
            _unitOfWork.StateRepository.DeleteState(id);
            _unitOfWork.Complete();
            return RedirectToAction("StateList","Setup");
        }
        public ActionResult CityList(int stateId)
        {
            return View(_unitOfWork.CityRepository.GetCities(stateId));
        }
        public ActionResult AddCity()
        {

            return View();
        }
        public ActionResult UpdateCity()
        {
            return View();
        }
        public ActionResult DeleteCity(int id)
        {
            _unitOfWork.CityRepository.DeleteCity(id);
            _unitOfWork.Complete();
            return RedirectToAction("CityList","Setup");
        }

        public ActionResult LocationList()
        {
            return View(_unitOfWork.LocationRepository.GetLocations());
        }
        public ActionResult AddLocation()
        {

            return View();
        }
        public ActionResult UpdateLocation()
        {
            return View();
        }
        public ActionResult DeleteLocation(int id)
        {
            _unitOfWork.LocationRepository.DeleteLocation(id);
            _unitOfWork.Complete();
            return RedirectToAction("LocationList", "Setup");
        }

    }
}