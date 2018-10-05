using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PoSCloudApp.Core;
using PoSCloudApp.Core.Models;
using PoSCloudApp.Core.ViewModels;

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
            ViewBag.edit = "AddDepartment";
            return View();
        }
        [HttpPost]
        public ActionResult AddDepartment(DepartmentViewModel departmentVm)
        {
            ViewBag.edit = "AddDepartment";
            if (!ModelState.IsValid)
            {
                return View(departmentVm);
            }
            else
            {
                Department department = Mapper.Map<Department>(departmentVm);
                _unitOfWork.DepartmentRepository.AddDepartment(department);
                _unitOfWork.Complete();
                return RedirectToAction("DepartmentList","Setup");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateDepartment(int id)
        {
            ViewBag.edit = "UpdateDepartment";
            DepartmentViewModel departmentVm =
                Mapper.Map<DepartmentViewModel>(_unitOfWork.DepartmentRepository.GetDepartmentById(id));
            return View("AddDepartment", departmentVm);
        }
        [HttpPost]
        public ActionResult UpdateDepartment(int id, DepartmentViewModel departmentVm)
        {
            ViewBag.edit = "UpdateDepartment";
            if (!ModelState.IsValid)
            {
                return View("AddDepartment",departmentVm);
            }
            else
            {
                Department department = Mapper.Map<Department>(departmentVm);
                _unitOfWork.DepartmentRepository.UpdateDepartment(id,department);
                _unitOfWork.Complete();
                return RedirectToAction("DepartmentList", "Setup");
            }
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
        [HttpGet]
        public ActionResult AddDesignation()
        {
            ViewBag.edit = "AddDesignation";
            return View();
        }
        [HttpPost]
        public ActionResult AddDesignation(DesignationViewModel designationVm)
        {
            ViewBag.edit = "AddDesignation";
            if (!ModelState.IsValid)
            {
                return View(designationVm);
            }

            Designation designation = Mapper.Map<Designation>(designationVm);
            _unitOfWork.DesignationRepository.AddDesignation(designation);
            _unitOfWork.Complete();
            return RedirectToAction("DesignationList", "Setup");
        }
        [HttpGet]
        public ActionResult UpdateDesignation(int id)
        {
            ViewBag.edit = "UpdateDesignation";
            DesignationViewModel designationVm =
                Mapper.Map<DesignationViewModel>(_unitOfWork.DesignationRepository.GetDesignationById(id));
            return RedirectToAction("AddDesignation",designationVm);
        }
        [HttpPost]
        public ActionResult UpdateDesignation(int id, DesignationViewModel designationVm)
        {
            ViewBag.edit = "UpdateDesignation";
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddDesignation", designationVm);
            }
            else
            {
                Designation designation = Mapper.Map<Designation>(designationVm);
                _unitOfWork.DesignationRepository.UpdateDesignation(id,designation);
                _unitOfWork.Complete();
                return RedirectToAction("DesignationList", "Setup");
            }
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

        [HttpGet]
        public ActionResult AddEmployee()
        {
            ViewBag.edit = "AddEmployee";
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModelView employeeMv)
        {
            ViewBag.edit = "AddEmployee";
            if (!ModelState.IsValid)
            {
                return View(employeeMv);
            }
            else
            {
                Employee employee = Mapper.Map<Employee>(employeeMv);
                _unitOfWork.EmployeeRepository.AddEmployee(employee);
                _unitOfWork.Complete();
                return RedirectToAction("EmployeeList","Setup");
            }
           
        }
        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            ViewBag.edit = "UpdateEmployee";
            EmployeeModelView employeeMv = Mapper.Map<EmployeeModelView>(_unitOfWork.EmployeeRepository.GetEmployeeById(id));
            return View("AddEmployee",employeeMv);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(int id, EmployeeModelView employeeMv)
        {
            ViewBag.edit = "UpdateEmployee";
            if (!ModelState.IsValid)
            {
                return View("AddEmployee",employeeMv);
            }
            else
            {
                Employee employee = Mapper.Map<Employee>(employeeMv);
                _unitOfWork.EmployeeRepository.UpdateEmployee(id,employee);
                _unitOfWork.Complete();
                return RedirectToAction("EmployeeList","Setup");
            }
            
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
        [HttpGet]
        public ActionResult AddCustomer()
        {
            ViewBag.edit = "AddCustomer";
            return View();
        }
        [HttpPost]
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