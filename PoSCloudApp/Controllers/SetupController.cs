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
    [Authorize]
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
            return View("AddDesignation",designationVm);
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
            EmployeeModelView employee = new EmployeeModelView();
            employee.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments()
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            employee.DesignationDdl = _unitOfWork.DesignationRepository.GetDesignations()
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            ViewBag.edit = "AddEmployee";
            return View(employee);
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
            employeeMv.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employeeMv.DesignationDdl = _unitOfWork.DesignationRepository.GetDesignations()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
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
        public ActionResult AddCustomer(CustomerModelView customerMv)
        {
            ViewBag.edit = "AddCustomer";
            if (!ModelState.IsValid)
            {
                return View(customerMv);
            }
            else
            {
                Customer customer = Mapper.Map<Customer>(customerMv);
                _unitOfWork.CustomerRepository.AddCustomer(customer);
                _unitOfWork.Complete();
                return RedirectToAction("CustomerList","Setup");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
            ViewBag.edit = "UpdateCustomer";
            CustomerModelView customerMv =
                Mapper.Map<CustomerModelView>(_unitOfWork.CustomerRepository.GetCustomerById(id));
            return View("AddCustomer",customerMv);
        }
        [HttpPost]
        public ActionResult UpdateCustomer(int id, CustomerModelView customerMv)
        {
            ViewBag.edit = "UpdateCustomer";
            if (!ModelState.IsValid)
            {
                return View("AddCustomer",customerMv);
            }
            else
            {
                Customer customer = Mapper.Map<Customer>(customerMv);
                _unitOfWork.CustomerRepository.UpdateCustomer(id,customer);
                _unitOfWork.Complete();
                return RedirectToAction("CustomerList", "Setup");
            }
            
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
        [HttpGet]
        public ActionResult AddSupplier()
        {
            ViewBag.edit = "AddSupplier";
            return View();
        }
        [HttpPost]
        public ActionResult AddSupplier(SupplierModelView supplierMv)
        {
            ViewBag.edit = "AddSupplier";
            if (!ModelState.IsValid)
            {
                return View(supplierMv);
            }
            else
            {
                Supplier supplier = Mapper.Map<Supplier>(supplierMv);
                _unitOfWork.SupplierRepository.AddSupplier(supplier);
                _unitOfWork.Complete();
                return RedirectToAction("SupplierList","Setup");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateSupplier(int id)
        {
            ViewBag.edit = "UpdateSupplier";
            SupplierModelView supplierVm =
                Mapper.Map<SupplierModelView>(_unitOfWork.SupplierRepository.GetSupplierById(id));
            return View("AddSupplier",supplierVm);
        }
        [HttpPost]
        public ActionResult UpdateSupplier(int id, SupplierModelView supplierVm)
        {
            ViewBag.edit = "UpdateSupplier";
            if (!ModelState.IsValid)
            {
                return View("AddSupplier", supplierVm);
            }

            {
                Supplier supplier = Mapper.Map<Supplier>(supplierVm);
                _unitOfWork.SupplierRepository.UpdateSupplier(id,supplier);
                _unitOfWork.Complete();
                return RedirectToAction("SupplierList", "Setup");
            }
           
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
        [HttpGet]
        public ActionResult AddState()
        {
            ViewBag.edit = "AddState";
            return View();
        }
        [HttpPost]
        public ActionResult AddState(StateModelView stateMv)
        {
            ViewBag.edit = "AddState";
            if (!ModelState.IsValid)
            {
                return View(stateMv);
            }
            else
            {
                State state = Mapper.Map<State>(stateMv);
                _unitOfWork.StateRepository.AddState(state);
                _unitOfWork.Complete();
                return RedirectToAction("StateList","Setup");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateState(int id)
        {
            ViewBag.edit = "UpdateState";
            StateModelView sateMv = Mapper.Map<StateModelView>(_unitOfWork.StateRepository.GetStateById(id));
            return View("AddState",sateMv);
        }
        [HttpPost]
        public ActionResult UpdateState(int id, StateModelView stateMv)
        {
            ViewBag.edit = "UpdateSate";
            if (!ModelState.IsValid)
            {
                return View("AddState", stateMv);
            }
            else
            {
                State state = Mapper.Map<State>(stateMv);
                _unitOfWork.StateRepository.UpdateState(id,state);
                _unitOfWork.Complete();
                return RedirectToAction("StateList","Setup");

            }
            
        }
        public ActionResult DeleteState(int id)
        {
            _unitOfWork.StateRepository.DeleteState(id);
            _unitOfWork.Complete();
            return RedirectToAction("StateList","Setup");
        }
        public ActionResult CityList(int stateId=0)
        {
            if (stateId == 0)
            {
                return View(_unitOfWork.CityRepository.GetCities().Select(a=>new CityListModelView{Id = a.Id,Name = a.Name,StateName = a.State.Name}));
            }
            return View(_unitOfWork.CityRepository.GetCities(stateId).Select(a => new CityListModelView { Id = a.Id, Name = a.Name, StateName = a.State.Name }));
        }
        public ActionResult AddCity()
        {
            CityModelView city = new CityModelView();
            city.StateDdl = _unitOfWork.StateRepository.GetStates()
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            ViewBag.edit = "AddCity";
            return View(city);
        }

        [HttpPost]
        public ActionResult AddCity(CityModelView cityVm)
        {
            ViewBag.edit = "AddCity";
            if (!ModelState.IsValid)
            {
                return View( cityVm);
            }
            else
            {
                City city = Mapper.Map<City>(cityVm);
                _unitOfWork.CityRepository.AddCity(city);
                _unitOfWork.Complete();
                return RedirectToAction("CityList","Setup");
            }
            
        }
       
        [HttpGet]
        public ActionResult UpdateCity(int id)
        { 
        ViewBag.edit = "UpdateCity";
            CityModelView cityMv = Mapper.Map<CityModelView>(_unitOfWork.CityRepository.GetCity(id));
            cityMv.StateDdl = _unitOfWork.StateRepository.GetStates()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddCity",cityMv);
        }
        [HttpPost]
        public ActionResult UpdateCity(int id, CityModelView cityVm)
        {
            ViewBag.edit = "UpdateCity";
            if (!ModelState.IsValid)
            {
                return View("AddCity", cityVm);
            }
            else
            {
                City city = Mapper.Map<City>(cityVm);
                _unitOfWork.CityRepository.UpdateCity(id,city);
                _unitOfWork.Complete();
                return RedirectToAction("CityList", "Setup");
            }          
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
        [HttpGet]
        public ActionResult AddLocation()
        {
            ViewBag.edit = "AddLocation";
            return View();
        }
        [HttpPost]
        public ActionResult AddLocation(LocationModelView locationMv)
        {
            ViewBag.edit = "AddLocation";
            if (!ModelState.IsValid)
            {
                return View(locationMv);
            }
            else
            {
                Location location = Mapper.Map<Location>(locationMv);
                _unitOfWork.LocationRepository.AddLocation(location);
                _unitOfWork.Complete();
                return RedirectToAction("LocationList", "Setup");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateLocation(int id)
        {
            ViewBag.edit = "UpdateLocation";
            LocationModelView locationMv =
                Mapper.Map<LocationModelView>(_unitOfWork.LocationRepository.GetLocationById(id));
            return View("AddLocation",locationMv);
        }
        [HttpPost]
        public ActionResult UpdateLocation(int id, LocationModelView locationMv)
        {
            ViewBag.edit = "UpdateLocation";
            if (!ModelState.IsValid)
            {
                return View("AddLocation",locationMv);
            }
            else
            {
                Location location = Mapper.Map<Location>(locationMv);
                _unitOfWork.LocationRepository.UpdateLocation(id,location);
                _unitOfWork.Complete();
                return RedirectToAction("LocationList","Setup");
            }
            
        }
        public ActionResult DeleteLocation(int id)
        {
            _unitOfWork.LocationRepository.DeleteLocation(id);
            _unitOfWork.Complete();
            return RedirectToAction("LocationList", "Setup");
        }

    }
}