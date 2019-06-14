using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Persistence;
using System.Data.Entity.Validation;
using POSApp.Persistence.Repositories;
using POSApp.SecurityFilters;

namespace POSApp.Controllers
{
    [Authorize]
    public class SetupController : LanguageController
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;


        private IUnitOfWork _unitOfWork;

        public SetupController()
        {

        }

        public SetupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Setup
        [View(Config.Setup.Department)]

        public ActionResult DepartmentList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId).OrderByDescending(a => a.Id));
        }

        [Manage(Config.Setup.Department)]

        public ActionResult AddDepartmentPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("DepartmentList");
            }
            ViewBag.edit = "AddDepartmentPartial";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Department)]

        public ActionResult AddDepartmentPartial(DepartmentViewModel Departmentvm)
        {
            ViewBag.edit = "AddDepartmentPartial";
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
                    Departmentvm.StoreId = user.StoreId;
                    Department Department = Mapper.Map<Department>(Departmentvm);
                    _unitOfWork.DepartmentRepository.AddDepartment(Department);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Department added successfully", AlertType.Success);
                    return PartialView("Test");
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

            return PartialView("Test");


        }

        [Manage(Config.Setup.Designation)]

        public ActionResult AddDesignationPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("DesignationList");
            }
            ViewBag.edit = "AddDesignationPartial";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Designation)]

        public ActionResult AddDesignationPartial(DesignationViewModel Designationvm)
        {
            ViewBag.edit = "AddDesignationPartial";
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
                    Designationvm.StoreId = user.StoreId;
                    Designation Designation = Mapper.Map<Designation>(Designationvm);
                    _unitOfWork.DesignationRepository.AddDesignation(Designation);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The designation added successfully", AlertType.Success);
                    return PartialView("Test");
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

            return PartialView("Test");


        }



        [Manage(Config.Setup.State)]

        public ActionResult AddStatePartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("StateList");
            }
            ViewBag.edit = "AddStatePartial";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.State)]

        public ActionResult AddStatePartial(StateModelView statevm)
        {
            ViewBag.edit = "AddStatePartial";
            if (!ModelState.IsValid)
            {
                return View(statevm);
            }
            else
            {
                State state = Mapper.Map<State>(statevm);
                _unitOfWork.StateRepository.AddState(state);
                _unitOfWork.Complete();
                return PartialView("Test");
            }

        }




        [Manage(Config.Setup.Supplier)]

        public ActionResult AddSupplierPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SupplierList");
            }
            ViewBag.edit = "AddSupplierPartial";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Supplier)]

        public ActionResult AddSupplierPartial(SupplierModelView suppliervm)
        {
            ViewBag.edit = "AddSupplierPartial";
            if (!ModelState.IsValid)
            {
                return View(suppliervm);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                suppliervm.StoreId = user.StoreId;
                BusinessPartner supplier = Mapper.Map<BusinessPartner>(suppliervm);
                supplier.StoreId = (int) user.StoreId;
                supplier.Type = "S";
                supplier.Birthday=DateTime.Now;
                _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(supplier);
                _unitOfWork.Complete();
                return PartialView("Test");
            }

        }

        [HttpGet]

        [Manage(Config.Setup.Department)]

        public ActionResult AddDepartment()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("DepartmentList");
            }
            ViewBag.edit = "AddDepartment";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Department)]

        public ActionResult AddDepartment(DepartmentViewModel departmentVm)
        {
            ViewBag.edit = "AddDepartment";
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                    return View(departmentVm);
                }
                else
                {
                    Department department = Mapper.Map<Department>(departmentVm);
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    department.StoreId = (int)user.StoreId;
                    _unitOfWork.DepartmentRepository.AddDepartment(department);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("Department Added Successfully",AlertType.Success);
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


            return View(departmentVm);


        }
        [HttpGet]
        [Manage(Config.Setup.Department)]

        public ActionResult UpdateDepartment(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("DepartmentList");
            }
            ViewBag.edit = "UpdateDepartment";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DepartmentViewModel departmentVm =
                Mapper.Map<DepartmentViewModel>(_unitOfWork.DepartmentRepository.GetDepartmentById(id, (int)user.StoreId));
            return View("AddDepartment", departmentVm);
        }
        [HttpPost]
        [Manage(Config.Setup.Department)]

        public ActionResult UpdateDepartment(int id, DepartmentViewModel departmentVm)
        {
            ViewBag.edit = "UpdateDepartment";
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                    return View("AddDepartment", departmentVm);
                }
                else
                {
                    Department department = Mapper.Map<Department>(departmentVm);
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    department.StoreId = (int)user.StoreId;
                    _unitOfWork.DepartmentRepository.UpdateDepartment(id, department.StoreId, department);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Department updated successfully", AlertType.Success);
                    return null;
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

            return View("AddDepartment", departmentVm);
        }
        [Manage(Config.Setup.Department)]

        public ActionResult DeleteDepartment( int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.DepartmentRepository.DeleteDepartment(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Department deleted successfully", AlertType.Success);
                return RedirectToAction("DepartmentList", "Setup");
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


            return RedirectToAction("DepartmentList", "Setup");

        }
        [View(Config.Setup.Designation)]

        public ActionResult DesignationList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DesignationRepository.GetDesignations((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        [HttpGet]
        [Manage(Config.Setup.Designation)]

        public ActionResult AddDesignation()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("DesignationList");
            }
            ViewBag.edit = "AddDesignation";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Designation)]

        public ActionResult AddDesignation(DesignationViewModel DesignationVm)
        {
            ViewBag.edit = "AddDesignation";
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                    return View(DesignationVm);
                }
                else
                {
                    Designation Designation = Mapper.Map<Designation>(DesignationVm);
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Designation.StoreId = (int)user.StoreId;
                    _unitOfWork.DesignationRepository.AddDesignation(Designation);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("Designation Added Successfully", AlertType.Success);
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


            return View(DesignationVm);


        }
        [HttpGet]
        [Manage(Config.Setup.Designation)]

        public ActionResult UpdateDesignation(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("DesignationList");
            }
            ViewBag.edit = "UpdateDesignation";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DesignationViewModel DesignationVm =
                Mapper.Map<DesignationViewModel>(_unitOfWork.DesignationRepository.GetDesignationById(id, (int)user.StoreId));
            return View("AddDesignation", DesignationVm);
        }
        [HttpPost]
        [Manage(Config.Setup.Designation)]

        public ActionResult UpdateDesignation(int id, DesignationViewModel DesignationVm)
        {
            ViewBag.edit = "UpdateDesignation";
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                    return View("AddDesignation", DesignationVm);
                }
                else
                {
                    Designation Designation = Mapper.Map<Designation>(DesignationVm);
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Designation.StoreId = (int)user.StoreId;
                    _unitOfWork.DesignationRepository.UpdateDesignation(id, Designation.StoreId, Designation);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Designation updated successfully", AlertType.Success);
                    return null;
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

            return View("AddDesignation", DesignationVm);
        }

        [Manage(Config.Setup.Designation)]

        public ActionResult DeleteDesignation(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.DesignationRepository.DeleteDesignation(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Designation deleted successfully", AlertType.Success);
                return RedirectToAction("DesignationList", "Setup");
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


            return RedirectToAction("DesignationList", "Setup");

        }

        
        [HttpGet]
        [View(Config.Setup.Employee)]

        public ActionResult EmployeeList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View("EmployeeList", _unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        //Add partial
        [HttpGet]
        [Manage(Config.Setup.Employee)]

        public ActionResult AddEmployeePartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("EmployeeList");
            }
            EmployeeModelView employee = new EmployeeModelView();
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            employee.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employee.DesignationDdl = _unitOfWork.DesignationRepository.GetDesignations((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employee.ShiftDdl = _unitOfWork.ShiftRepository.GetShifts((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.ShiftId.ToString(), Text = a.Name }).AsEnumerable();
            ViewBag.edit = "AddEmployeePartial";
            return View(employee);
        }
        [HttpPost]
        [Manage(Config.Setup.Employee)]

        public ActionResult AddEmployeePartial(EmployeeModelView employeeMv)
        {
            ViewBag.edit = "AddEmployeePartial";
            if (ModelState.ContainsKey("JoinDate"))
                ModelState["JoinDate"].Errors.Clear();
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                }
                else
                {
                    employeeMv.StoreId = user.StoreId;
                    Employee employee = Mapper.Map<Employee>(employeeMv);

                    _unitOfWork.EmployeeRepository.AddEmployee(employee);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The employee added successfully", AlertType.Success);
                    return PartialView("Test");
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


            return PartialView("Test");



        }
        public JsonResult GetEmployeeDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<EmployeeModelView[]>(_unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public JsonResult GetShiftDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<EmployeeModelView[]>(_unitOfWork.ShiftRepository.GetShifts((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //Add Employee
        [HttpGet]
        [Manage(Config.Setup.Employee)]

        public ActionResult AddEmployee()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("EmployeeList");
            }
            EmployeeModelView employee = new EmployeeModelView();
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            employee.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId)
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            employee.DesignationDdl = _unitOfWork.DesignationRepository.GetDesignations((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employee.ShiftDdl = _unitOfWork.ShiftRepository.GetShifts((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.ShiftId.ToString(), Text = a.Name }).AsEnumerable();
            ViewBag.edit = "AddEmployee";
            return View(employee);
        }
        [HttpPost]
        [Manage(Config.Setup.Employee)]

        public ActionResult AddEmployee(EmployeeModelView employeeMv)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "AddEmployee";
            employeeMv.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employeeMv.DesignationDdl = _unitOfWork.DesignationRepository.GetDesignations((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employeeMv.ShiftDdl = _unitOfWork.ShiftRepository.GetShifts((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.ShiftId.ToString(), Text = a.Name }).AsEnumerable();
            if (ModelState.ContainsKey("JoinDate"))
                ModelState["JoinDate"].Errors.Clear();
            try
            {
                
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                    return View(employeeMv);
                }
                else
                {
                    employeeMv.StoreId = user.StoreId;
                    Employee employee = Mapper.Map<Employee>(employeeMv);

                    _unitOfWork.EmployeeRepository.AddEmployee(employee);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The employee added successfully", AlertType.Success);
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


            return View(employeeMv);



        }
        [HttpGet]
        [Manage(Config.Setup.Employee)]

        public ActionResult UpdateEmployee(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("EmployeeList");
            }
            
            
            ViewBag.edit = "UpdateEmployee";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            EmployeeModelView employeeMv = Mapper.Map<EmployeeModelView>(_unitOfWork.EmployeeRepository.GetEmployeeById(id, Convert.ToInt32(user.StoreId)));
            employeeMv.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employeeMv.DesignationDdl = _unitOfWork.DesignationRepository.GetDesignations((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employeeMv.ShiftDdl = _unitOfWork.ShiftRepository.GetShifts((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.ShiftId.ToString(), Text = a.Name }).AsEnumerable();

            return View("AddEmployee",employeeMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Employee)]

        public ActionResult UpdateEmployee(int id, EmployeeModelView employeeMv)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "UpdateEmployee";
            employeeMv.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employeeMv.DesignationDdl = _unitOfWork.DesignationRepository.GetDesignations((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            employeeMv.ShiftDdl = _unitOfWork.ShiftRepository.GetShifts((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.ShiftId.ToString(), Text = a.Name }).AsEnumerable();
            if (ModelState.ContainsKey("JoinDate"))
                ModelState["JoinDate"].Errors.Clear();
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                    return View("AddEmployee", employeeMv);
                }
                else
                {
                    Employee employee = Mapper.Map<Employee>(employeeMv);
                   
                    _unitOfWork.EmployeeRepository.UpdateEmployee(id, employee, Convert.ToInt32(user.StoreId));
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The employee updated successfully", AlertType.Success);
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


            return View("AddEmployee", employeeMv);


        }
        [Manage(Config.Setup.Employee)]

        public ActionResult DeleteEmployee(int id, int storeid)
        {
            try
            {
                _unitOfWork.EmployeeRepository.DeleteEmployee(id, storeid);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The employee deleted successfully", AlertType.Success);
                return RedirectToAction("EmployeeList", "Setup");
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


            return RedirectToAction("EmployeeList", "Setup");

        }
        [Manage(Config.Setup.Shift)]

        public ActionResult AddShiftPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ShiftList");
            }
            ViewBag.edit = "AddShiftPartial";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Shift)]

        public ActionResult AddShiftPartial(ShiftViewModel Shiftvm)
        {
            ViewBag.edit = "AddShiftPartial";
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
                    Shiftvm.StoreId = user.StoreId;
                    Shift Shift = Mapper.Map<Shift>(Shiftvm);
                    _unitOfWork.ShiftRepository.AddShift(Shift);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Shift added successfully", AlertType.Success);
                    return PartialView("Test");
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

            return PartialView("Test");


        }
        [View(Config.Setup.Customer)]

        public ActionResult CustomerList()
        {

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<CustomerModelView[]>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartners("C",(int)user.StoreId).OrderByDescending(a => a.Id)));
        }
        [HttpGet]
        [Manage(Config.Setup.Customer)]

        public ActionResult AddCustomer()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("CustomerList");
            }
            ViewBag.edit = "AddCustomer";          
            return View(new CustomerModelView());
        }
        [HttpPost]
        [Manage(Config.Setup.Customer)]

        public ActionResult AddCustomer(CustomerModelView customerMv)
        {
            ViewBag.edit = "AddCustomer";
            
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(customerMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    customerMv.StoreId = user.StoreId;
                    BusinessPartner customer = Mapper.Map<BusinessPartner>(customerMv);
                    customer.Type = "C";
                    _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(customer);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The customer added successfully", AlertType.Success);
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

            return View(customerMv);

        }
        [HttpGet]
        [Manage(Config.Setup.Customer)]

        public ActionResult UpdateCustomer(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("CustomerList");
            }
            ViewBag.edit = "UpdateCustomer";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            CustomerModelView customerMv =
                Mapper.Map<CustomerModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner(id, Convert.ToInt32(user.StoreId)));
            return View("AddCustomer",customerMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Customer)]

        public ActionResult UpdateCustomer(int id, CustomerModelView customerMv)
        {
            ViewBag.edit = "UpdateCustomer";
    
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. "+message, AlertType.Error);
                    return View("AddCustomer", customerMv);
                }
                else
                {
                    BusinessPartner customer = Mapper.Map<BusinessPartner>(customerMv);
                    customer.Type = "C";
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    _unitOfWork.BusinessPartnerRepository.UpdateBusinessPartner(id, Convert.ToInt32(user.StoreId), customer);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The customer updated successfully", AlertType.Success);
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


            return View("AddCustomer", customerMv);



        }
        [Manage(Config.Setup.Customer)]

        public ActionResult DeleteCustomer(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.BusinessPartnerRepository.DeleteBusinessPartner(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The customer deleted successfully", AlertType.Success);
                return RedirectToAction("CustomerList", "Setup");
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


            return RedirectToAction("CustomerList", "Setup");


        }
        [View(Config.Setup.Supplier)]

        public ActionResult SupplierList()
        {
        
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<SupplierModelView[]>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).OrderByDescending(a => a.Id)));
        }
        [HttpGet]
        [Manage(Config.Setup.Supplier)]

        public ActionResult AddSupplier()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SupplierList");
            }
            ViewBag.edit = "AddSupplier";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Supplier)]

        public ActionResult AddSupplier(SupplierModelView supplierMv)
        {
            ViewBag.edit = "AddSupplier";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(supplierMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    supplierMv.StoreId = user.StoreId;

                    BusinessPartner supplier = Mapper.Map<BusinessPartner>(supplierMv);
                    supplier.Birthday = DateTime.Now;
                    supplier.Type = "S";
                    _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(supplier);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The supplier added successfully", AlertType.Success);
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

            return View(supplierMv);

        }
        [HttpGet]
        [Manage(Config.Setup.Supplier)]

        public ActionResult UpdateSupplier(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SupplierList");
            }
            ViewBag.edit = "UpdateSupplier";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            SupplierModelView supplierVm =
                Mapper.Map<SupplierModelView>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartner(id,Convert.ToInt32(user.StoreId)));
            return View("AddSupplier",supplierVm);
        }
        [HttpPost]
        [Manage(Config.Setup.Supplier)]

        public ActionResult UpdateSupplier(int id, SupplierModelView supplierVm)
        {
            ViewBag.edit = "UpdateSupplier";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddSupplier", supplierVm);
                }

                {
                    BusinessPartner supplier = Mapper.Map<BusinessPartner>(supplierVm);
                    supplier.Type = "S";
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    supplier.Birthday = DateTime.Now;
                    _unitOfWork.BusinessPartnerRepository.UpdateBusinessPartner(id, Convert.ToInt32(user.StoreId), supplier);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The supplier updated successfully", AlertType.Success);
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

            return View("AddSupplier", supplierVm);



        }
        [Manage(Config.Setup.Supplier)]

        public ActionResult DeleteSupplier(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.BusinessPartnerRepository.DeleteBusinessPartner(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The supplier deleted successfully", AlertType.Success);
                return RedirectToAction("SupplierList", "Setup");
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

            return RedirectToAction("SupplierList", "Setup");

        }
        [View(Config.Setup.State)]

        public ActionResult StateList()
        {
            return View(_unitOfWork.StateRepository.GetStates().OrderByDescending(a => a.Id));
        }
        [HttpGet]
        [Manage(Config.Setup.State)]

        public ActionResult AddState()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("StateList");
            }
            ViewBag.edit = "AddState";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.State)]

        public ActionResult AddState(StateModelView stateMv)
        {
            ViewBag.edit = "AddState";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(stateMv);
                }
                else
                {
                    State state = Mapper.Map<State>(stateMv);
                    _unitOfWork.StateRepository.AddState(state);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The state added successfully", AlertType.Success);
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

            return View(stateMv);


        }
        [HttpGet]
        [Manage(Config.Setup.State)]

        public ActionResult UpdateState(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("StateList");
            }
            ViewBag.edit = "UpdateState";
            StateModelView sateMv = Mapper.Map<StateModelView>(_unitOfWork.StateRepository.GetStateById(id));
            return View("AddState",sateMv);
        }
        [HttpPost]
        [Manage(Config.Setup.State)]

        public ActionResult UpdateState(int id, StateModelView stateMv)
        {
            ViewBag.edit = "UpdateSate";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddState", stateMv);
                }
                else
                {
                    State state = Mapper.Map<State>(stateMv);
                    _unitOfWork.StateRepository.UpdateState(id, state);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The state update successfully", AlertType.Success);
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

            return View("AddState", stateMv);


        }
        [Manage(Config.Setup.State)]

        public ActionResult DeleteState(int id)
        {
            try
            {
                _unitOfWork.StateRepository.DeleteState(id);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The state deleted successfully", AlertType.Success);
                return RedirectToAction("StateList", "Setup");
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

            return RedirectToAction("StateList", "Setup");

        }
        [View(Config.Setup.City)]

        public ActionResult CityList(int stateId=0)
        {

            if (stateId == 0)
            {
                return View(_unitOfWork.CityRepository.GetCities().Select(a=>new CityListModelView{Id = a.Id,Name = a.Name,StateName = a.State.Name}));
            }
            return View(_unitOfWork.CityRepository.GetCities(stateId).Select(a => new CityListModelView { Id = a.Id, Name = a.Name, StateName = a.State.Name }));
        }
        [Manage(Config.Setup.City)]

        public ActionResult AddCity()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("CityList");
            }
            CityModelView city = new CityModelView();
            city.StateDdl = _unitOfWork.StateRepository.GetStates()
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            ViewBag.edit = "AddCity";
            return View(city);
        }

        [HttpPost]
        [Manage(Config.Setup.City)]

        public ActionResult AddCity(CityModelView cityVm)
        {
            ViewBag.edit = "AddCity";
            cityVm.StateDdl = _unitOfWork.StateRepository.GetStates()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(cityVm);
                }
                else
                {
                    City city = Mapper.Map<City>(cityVm);
                    _unitOfWork.CityRepository.AddCity(city);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The city added successfully", AlertType.Success);
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

            return View(cityVm);


        }
       
        [HttpGet]
        [Manage(Config.Setup.City)]

        public ActionResult UpdateCity(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("CityList");
            }
            ViewBag.edit = "UpdateCity";
            CityModelView cityMv = Mapper.Map<CityModelView>(_unitOfWork.CityRepository.GetCity(id));
            cityMv.StateDdl = _unitOfWork.StateRepository.GetStates()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddCity",cityMv);
        }
        [HttpPost]
        [Manage(Config.Setup.City)]

        public ActionResult UpdateCity(int id, CityModelView cityVm)
        {
            ViewBag.edit = "UpdateCity";
            cityVm.StateDdl = _unitOfWork.StateRepository.GetStates()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddCity", cityVm);
                }
                else
                {
                    City city = Mapper.Map<City>(cityVm);
                    _unitOfWork.CityRepository.UpdateCity(id, city);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The city updated successfully", AlertType.Success);
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

            return View("AddCity", cityVm);

        }
        [Manage(Config.Setup.City)]

        public ActionResult DeleteCity(int id)
        {
            try
            {
                _unitOfWork.CityRepository.DeleteCity(id);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The city deleted successfully", AlertType.Success);
                return RedirectToAction("CityList", "Setup");
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

            return RedirectToAction("CityList", "Setup");

        }
        [View(Config.Setup.Location)]

        public ActionResult LocationList()
        {
            return View(_unitOfWork.LocationRepository.GetLocations());
        }
        [HttpGet]
        [Manage(Config.Setup.Location)]

        public ActionResult AddLocation()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("LocationList");
            }
            ViewBag.edit = "AddLocation";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Location)]

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
        [Manage(Config.Setup.Location)]

        public ActionResult UpdateLocation(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("LocationList");
            }
            ViewBag.edit = "UpdateLocation";
            LocationModelView locationMv =
                Mapper.Map<LocationModelView>(_unitOfWork.LocationRepository.GetLocationById(id));
            return View("AddLocation",locationMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Location)]

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
        [Manage(Config.Setup.Location)]

        public ActionResult DeleteLocation(int id)
        {
            _unitOfWork.LocationRepository.DeleteLocation(id);
            _unitOfWork.Complete();
            return RedirectToAction("LocationList", "Setup");
        }
        [View(Config.Setup.Discount)]

        public ActionResult DiscountList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DiscountRepository.GetDiscounts((int)user.StoreId));
        }
        [HttpGet]
        [Manage(Config.Setup.Discount)]

        public ActionResult AddDiscount()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("TimedEventList");
            }
            ViewBag.edit = "AddDiscount";
            return View(new DiscountViewModel());
        }
        [HttpPost]
        [Manage(Config.Setup.Discount)]

        public ActionResult AddDiscount(DiscountViewModel dicountMv)
        {
            ViewBag.edit = "AddDiscount";
            try
            {
                dicountMv.Days = string.Join(",", dicountMv.tempDays);
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

                    Discount discount = Mapper.Map<Discount>(dicountMv);
                    discount.StoreId = (int)user.StoreId;
                    _unitOfWork.DiscountRepository.AddDiscount(discount);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The discount added successfully", AlertType.Success);
                    return RedirectToAction("DiscountList", "Setup");
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

            return RedirectToAction("DiscountList", "Setup");



        }
        [HttpGet]
        [Manage(Config.Setup.Discount)]

        public ActionResult UpdateDiscount(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("TimedEventList");
            }
            ViewBag.edit = "UpdateDiscount";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DiscountViewModel discountMv =
                Mapper.Map<DiscountViewModel>(_unitOfWork.DiscountRepository.GetDiscountById(id, (int)user.StoreId));
            discountMv.tempDays = discountMv.Days.Split(',');
            return View("AddDiscount", discountMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Discount)]

        public ActionResult UpdateDiscount(int id, DiscountViewModel discountMv)
        {
            ViewBag.edit = "UpdateDiscount";
            try
            {
                discountMv.Days = string.Join(",", discountMv.tempDays);
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
                    Discount discount = Mapper.Map<Discount>(discountMv);
                    _unitOfWork.DiscountRepository.UpdateDiscount(id, discount, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The discount updated successfully", AlertType.Success);
                    return RedirectToAction("DiscountList", "Setup");
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

            return RedirectToAction("DiscountList", "Setup");


        }
        [Manage(Config.Setup.Discount)]

        public ActionResult DeleteDiscount(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.DiscountRepository.DeleteDiscount(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The discount deleted successfully", AlertType.Success);
                return RedirectToAction("DiscountList", "Setup");
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

            return RedirectToAction("DiscountList", "Setup");

        }
        [View(Config.Setup.Tax)]

        public ActionResult TaxList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.TaxRepository.GetTaxes((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        [HttpGet]
        [Manage(Config.Setup.Tax)]

        public ActionResult AddTax()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("TaxList");
            }
            ViewBag.edit = "AddTax";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Tax)]

        public ActionResult AddTax(TaxViewModel taxMv)
        {

            ViewBag.edit = "AddTax";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(taxMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Tax tax = Mapper.Map<Tax>(taxMv);
                    tax.StoreId = (int)user.StoreId;
                    _unitOfWork.TaxRepository.AddTax(tax);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The tax added successfully", AlertType.Success);
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

            return View(taxMv);


        }
        [HttpGet]
        [Manage(Config.Setup.Tax)]

        public ActionResult UpdateTax(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("TaxList");
            }
            ViewBag.edit = "UpdateTax";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TaxViewModel taxMv =
                Mapper.Map<TaxViewModel>(_unitOfWork.TaxRepository.GetTaxById(id, (int)user.StoreId));
            return View("AddTax", taxMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Tax)]

        public ActionResult UpdateTax(int id, TaxViewModel taxMv)
        {

            ViewBag.edit = "UpdateTax";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddTax",taxMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Tax tax = Mapper.Map<Tax>(taxMv);
                    _unitOfWork.TaxRepository.UpdateTax(id, tax, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The tax added successfully", AlertType.Success);
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

            return View("AddTax", taxMv);


        }
        [Manage(Config.Setup.Tax)]

        public ActionResult DeleteTax(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TaxRepository.DeleteTax(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The tax deleted successfully", AlertType.Success);
                return RedirectToAction("TaxList", "Setup");
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

            return RedirectToAction("TaxList", "Setup");

        }

        //public ActionResult CouponList()
        //{
        //    var userid = User.Identity.GetUserId();
        //    var user = UserManager.FindById(userid);
        //    return View(_unitOfWork.CouponRepository.GetCoupons((int)user.StoreId));
        //}
        //[HttpGet]
        //public ActionResult AddCoupon()
        //{
        //    ViewBag.edit = "AddCoupon";
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddCoupon(CouponModelView couponMv)
        //{
        //    couponMv.Days = string.Join(",", couponMv.tempDays);
        //    ViewBag.edit = "AddCoupon";
        //    if (!ModelState.IsValid)
        //    {
        //        return View(couponMv);
        //    }
        //    else
        //    {
        //        var userid = User.Identity.GetUserId();
        //        var user = UserManager.FindById(userid);
        //        Coupon location = Mapper.Map<Coupon>(couponMv);
        //        location.StoreId= (int)user.StoreId;
        //        _unitOfWork.CouponRepository.AddCoupon(location);
        //        _unitOfWork.Complete();
        //        return RedirectToAction("CouponList", "Setup");
        //    }

        //}
        //[HttpGet]
        //public ActionResult UpdateCoupon(int id)
        //{
        //    ViewBag.edit = "UpdateCoupon";
        //    var userid = User.Identity.GetUserId();
        //    var user = UserManager.FindById(userid);
        //    CouponModelView couponMv =
        //        Mapper.Map<CouponModelView>(_unitOfWork.CouponRepository.GetCouponById(id, (int)user.StoreId));
        //    couponMv.tempDays = couponMv.Days.Split(',');
        //    return View("AddCoupon", couponMv);
        //}
        //[HttpPost]
        //public ActionResult UpdateCoupon(int id, CouponModelView couponMv)
        //{
        //    couponMv.Days = string.Join(",", couponMv.tempDays);
        //    ViewBag.edit = "UpdateCoupon";
        //    if (!ModelState.IsValid)
        //    {
        //        return View("AddCoupon", couponMv);
        //    }
        //    else
        //    {
        //        var userid = User.Identity.GetUserId();
        //        var user = UserManager.FindById(userid);
        //        Coupon location = Mapper.Map<Coupon>(couponMv);
        //        _unitOfWork.CouponRepository.UpdateCoupon(id, location, (int)user.StoreId);
        //        _unitOfWork.Complete();
        //        return RedirectToAction("CouponList", "Setup");
        //    }

        //}
        //public ActionResult DeleteCoupon(int id)
        //{
        //    var userid = User.Identity.GetUserId();
        //    var user = UserManager.FindById(userid);
        //    _unitOfWork.CouponRepository.DeleteCoupon(id, (int)user.StoreId);
        //    _unitOfWork.Complete();
        //    return RedirectToAction("CouponList", "Setup");
        //}
        [View(Config.Setup.Unit)]

        public ActionResult UnitList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.UnitRepository.GetUnit((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        //Unit Partial
        [HttpGet]
        [Manage(Config.Setup.Unit)]

        public ActionResult AddUnitPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("UnitList");
            }
            ViewBag.edit = "AddUnitPartial";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Unit)]

        public ActionResult AddUnitPartial(UnitViewModel unitMv)
        {
            ViewBag.edit = "AddUnitPartial";
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
                    Unit location = Mapper.Map<Unit>(unitMv);
                    location.StoreId = (int)user.StoreId;
                    _unitOfWork.UnitRepository.AddUnit(location);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The unit added successfully", AlertType.Success);
                    return PartialView("Test");

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

            return PartialView("Test");




        }
        public JsonResult GetUnitDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<UnitViewModel[]>(_unitOfWork.UnitRepository.GetUnit((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //Add unit
        [HttpGet]
        [Manage(Config.Setup.Unit)]

        public ActionResult AddUnit()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("UnitList");
            }
            ViewBag.edit = "AddUnit";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Unit)]

        public ActionResult AddUnit(UnitViewModel unitMv)
        {
            ViewBag.edit = "AddUnit";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(unitMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Unit location = Mapper.Map<Unit>(unitMv);
                    location.StoreId = (int)user.StoreId;
                    _unitOfWork.UnitRepository.AddUnit(location);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The unit added successfully", AlertType.Success);
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

            return View(unitMv);



        }
        [HttpGet]
        [Manage(Config.Setup.Unit)]

        public ActionResult UpdateUnit(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("UnitList");
            }
            ViewBag.edit = "UpdateUnit";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            UnitViewModel unitMv =
                Mapper.Map<UnitViewModel>(_unitOfWork.UnitRepository.GetUnitById(id, (int)user.StoreId));
            return View("AddUnit", unitMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Unit)]

        public ActionResult UpdateUnit(int id, UnitViewModel unitMv)
        {
            ViewBag.edit = "UpdateUnit";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddUnit",unitMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Unit location = Mapper.Map<Unit>(unitMv);
                    _unitOfWork.UnitRepository.UpdateUnit(id, location, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The unit updated successfully", AlertType.Success);
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

            return View("AddUnit", unitMv);


        }
        [Manage(Config.Setup.Unit)]

        public ActionResult DeleteUnit(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.UnitRepository.DeleteUnit(id, (int)user.StoreId);
                TempData["Alert"] = new AlertModel("The unit deleted successfully", AlertType.Success);
                _unitOfWork.Complete();
                return RedirectToAction("UnitList", "Setup");
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

            return RedirectToAction("UnitList", "Setup");


        }
        [View(Config.Setup.Client)]

        public ActionResult ClientList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ClientRepository.GetClients().OrderByDescending(a => a.Id));
        }
        [HttpGet]
        [Manage(Config.Setup.Client)]

        public ActionResult AddClient()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ClientList");
            }
            ViewBag.edit = "AddClient";
            return View();
        }
        [HttpPost]
        [Manage(Config.Setup.Client)]

        public ActionResult AddClient(ClientViewModel clientMv, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddClient";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(clientMv);
                }
                else
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        try
                        {
                            string path = Server.MapPath("~/Images/Data/" + file.FileName);
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.Message = "Image Already Exists!";

                            }
                            else
                            {
                                file.SaveAs(path);
                            }
                            clientMv.Image = "/Images/Data/" + file.FileName;
                        }
                        catch (Exception e)
                        {
                            ViewBag.Message = "ERROR:" + e.Message.ToString();
                        }

                    }

                    _unitOfWork.ClientRepository.AddClient(Mapper.Map<Client>(clientMv));
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The client added successfully", AlertType.Success);
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

            return View(clientMv);


        }
        [HttpGet]
        [Manage(Config.Setup.Client)]

        public ActionResult UpdateClient(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ClientList");
            }
            ViewBag.edit = "UpdateClient";

            ClientViewModel clientMv =
                Mapper.Map<ClientViewModel>(_unitOfWork.ClientRepository.GetClient(id));
            return View("AddClient", clientMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Client)]

        public ActionResult UpdateClient(int id, ClientViewModel clientMv, HttpPostedFileBase file)
        {
            ViewBag.edit = "UpdateClient";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddClient", clientMv);
                }
                else
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        try
                        {
                            string path = Server.MapPath("~/Images/Data/" + file.FileName);
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.Message = "Image Already Exists!";

                            }
                            else
                            {
                                file.SaveAs(path);
                            }
                            clientMv.Image = "/Images/Data/" + file.FileName;
                        }
                        catch (Exception e)
                        {
                            ViewBag.Message = "ERROR:" + e.Message.ToString();
                        }

                    }
                    _unitOfWork.ClientRepository.UpdateClient(id, Mapper.Map<Client>(clientMv));
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The client updated successfully", AlertType.Success);
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

            return View("AddClient", clientMv);


        }

        [Manage(Config.Setup.Client)]

        public ActionResult DeleteClient(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ClientRepository.DeleteClient(id);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The client deleted successfully", AlertType.Success);
                return RedirectToAction("ClientList", "Setup");
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

            return RedirectToAction("ClientList", "Setup");

        }

        //TimedEvents
        [View(Config.Setup.TimedEvent)]

        public ActionResult TimedEventList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.TimedEventRepository.GetTimedEvents((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        [HttpGet]
        [Manage(Config.Setup.TimedEvent)]

        public ActionResult AddTimedEvent()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("TimedEventList");
            }
            ViewBag.edit = "AddTimedEvent";
            TimedEventViewModel model=new TimedEventViewModel();
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            model.CatDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int) user.StoreId)
                .Select(a => new SelectListItem{Text = a.Name,Value = a.Id.ToString()});
            model.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a=>!a.InventoryItem && !a.PurchaseItem)
                .Select(a => new SelectListItem { Text = a.Name + " (" + a.Size + ")", Value = a.ProductCode });
            model.BranchDdl = _unitOfWork.StoreRepository.GetStores()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            return View(model);
        }
        [HttpPost]
        [Manage(Config.Setup.TimedEvent)]

        public ActionResult AddTimedEvent(TimedEventViewModel timeeventVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            timeeventVm.CatDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            timeeventVm.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => !a.InventoryItem && !a.PurchaseItem)
                .Select(a => new SelectListItem { Text = a.Name + " (" + a.Size + ")", Value = a.ProductCode });
            timeeventVm.BranchDdl = _unitOfWork.StoreRepository.GetStores()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            ViewBag.edit = "AddTimedEvent";
            if (ModelState.ContainsKey("FromDate"))
                ModelState["FromDate"].Errors.Clear();
            ModelState["ToDate"].Errors.Clear();
            ModelState["ToHour"].Errors.Clear();
            ModelState["FromHour"].Errors.Clear();
            try
            {
                
                if (!ModelState.IsValid)
                {
                    timeeventVm.ProductsDisplay = string.Join(",", timeeventVm.Products);
                    timeeventVm.DaysDisplay = string.Join(",", timeeventVm.Days);
                    timeeventVm.BranchesDisplay = string.Join(",", timeeventVm.Branches);
                    timeeventVm.CategoriesDisplay = string.Join(",", timeeventVm.Categories);


                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(timeeventVm);
                }
                else
                {

                    TimedEvent time = Mapper.Map<TimedEvent>(timeeventVm);

                    foreach (var timeeventVmBranch in timeeventVm.Branches)
                    {

                        var data = time;
                        data.StoreId = timeeventVmBranch;
                        _unitOfWork.TimedEventRepository.AddTimedEvent(data);
                        _unitOfWork.Complete();
                        if (timeeventVm.Categories != null)
                        {

                            foreach (var timeeventVmCategory in timeeventVm.Categories)
                            {
                                string[] products = _unitOfWork.ProductRepository.GetProducts(timeeventVmCategory).Where(a => a.StoreId == timeeventVmBranch)
                                    .Select(a => a.ProductCode).ToArray();
                                foreach (var product in products)
                                {
                                    _unitOfWork.TimedEventProductsRepository.AddTimedEventProducts(new TimedEventProducts
                                    {
                                        ProductCode = product
                                        ,
                                        StoreId = timeeventVmBranch
                                        ,
                                        TimedEventId = data.Id
                                    });
                                }
                            }
                        }
                        else
                        {
                            foreach (var product in timeeventVm.Products)
                            {
                                _unitOfWork.TimedEventProductsRepository.AddTimedEventProducts(new TimedEventProducts
                                {
                                    ProductCode = product
                                    ,
                                    StoreId = timeeventVmBranch
                                   ,
                                    TimedEventId = data.Id
                                });
                            }
                        }
                        _unitOfWork.Complete();
                    }
                    TempData["Alert"] = new AlertModel("The timed event added successfully", AlertType.Success);
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
            timeeventVm.ProductsDisplay = string.Join(",", timeeventVm.Products);
            timeeventVm.DaysDisplay = string.Join(",", timeeventVm.Days);
            timeeventVm.BranchesDisplay = string.Join(",", timeeventVm.Branches);
            timeeventVm.CategoriesDisplay = string.Join(",", timeeventVm.Categories);
            return View(timeeventVm);


        }
        [HttpGet]
        [Manage(Config.Setup.TimedEvent)]

        public ActionResult UpdateTimedEvent(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("TimedEventList");
            }
            ViewBag.edit = "UpdateTimedEvent";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TimedEventViewModel timeeventVm =
                Mapper.Map<TimedEventViewModel>(_unitOfWork.TimedEventRepository.GetTimedEventById(id, (int)user.StoreId));
            timeeventVm.CatDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            timeeventVm.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => !a.InventoryItem && !a.PurchaseItem)
                .Select(a => new SelectListItem { Text = a.Name + " (" + a.Size + ")", Value = a.ProductCode });
            timeeventVm.BranchDdl = _unitOfWork.StoreRepository.GetStores()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            ViewBag.alert = "<script> $(document).ready(function() {$('#CategoriesArea').css('display', 'none');$('#BranchesArea').css('display', 'none');" +
                            @"});</script>";
            return View("AddTimedEvent", timeeventVm);
        }
        [HttpPost]
        [Manage(Config.Setup.TimedEvent)]

        public ActionResult UpdateTimedEvent(int id, TimedEventViewModel timeeventVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            timeeventVm.CatDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            timeeventVm.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => !a.InventoryItem && !a.PurchaseItem)
                .Select(a => new SelectListItem { Text = a.Name + " (" + a.Size + ")", Value = a.ProductCode });
            timeeventVm.BranchDdl = _unitOfWork.StoreRepository.GetStores()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            ViewBag.edit = "UpdateTimedEvent";
            if (ModelState.ContainsKey("FromDate"))
                ModelState["FromDate"].Errors.Clear();
            ModelState["ToDate"].Errors.Clear();
            ModelState["ToHour"].Errors.Clear();
            ModelState["FromHour"].Errors.Clear();
            try
            {
                
                if (!ModelState.IsValid)
                {
                    timeeventVm.ProductsDisplay = string.Join(",", timeeventVm.Products);
                    timeeventVm.DaysDisplay = string.Join(",", timeeventVm.Days);
                    timeeventVm.BranchesDisplay = string.Join(",", timeeventVm.Branches);
                    timeeventVm.CategoriesDisplay = string.Join(",", timeeventVm.Categories);

                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddTimedEvent", timeeventVm);
                }
                else
                {

                    TimedEvent location = Mapper.Map<TimedEvent>(timeeventVm);
                    _unitOfWork.TimedEventRepository.UpdateTimedEvent(id, location, (int) user.StoreId);

                    _unitOfWork.TimedEventProductsRepository.DeleteTimedEventProducts(location.Id,
                        location.StoreId);

                    if (timeeventVm.Categories != null)
                    {
                        if (timeeventVm.Categories.Length > 0)
                        {
                            _unitOfWork.TimedEventProductsRepository.DeleteTimedEventProducts(location.Id,
                                location.StoreId);
                            _unitOfWork.Complete();
                        }
                        else
                        {
                        }

                        foreach (var timeeventVmCategory in timeeventVm.Categories)
                        {
                            string[] products = _unitOfWork.ProductRepository.GetProducts(timeeventVmCategory)
                                .Where(a => a.StoreId == location.StoreId)
                                .Select(a => a.ProductCode).ToArray();
                            foreach (var product in products)
                            {
                                _unitOfWork.TimedEventProductsRepository.AddTimedEventProducts(new TimedEventProducts
                                {
                                    ProductCode = product,
                                    StoreId = location.StoreId,

                                    TimedEventId = location.Id
                                });
                            }
                        }
                    }
                    else
                    {
                        if (timeeventVm.Products != null)
                        {
                            if (timeeventVm.Products.Length > 0)
                            {
                                _unitOfWork.TimedEventProductsRepository.DeleteTimedEventProducts(location.Id,
                                    location.StoreId);
                                _unitOfWork.Complete();
                            }
                            else
                            {
                            }

                            foreach (var product in timeeventVm.Products)
                            {
                                _unitOfWork.TimedEventProductsRepository.AddTimedEventProducts(new TimedEventProducts
                                {
                                    ProductCode = product,
                                    StoreId = location.StoreId,
                                    TimedEventId = location.Id
                                });
                            }
                        }
                        else { }
                           
                    }

                    _unitOfWork.Complete();



                TempData["Alert"] = new AlertModel("The timed event updated successfully", AlertType.Success);
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


            timeeventVm.ProductsDisplay =
                string.Join(",", timeeventVm.Products ?? new string[0]);
            timeeventVm.DaysDisplay = string.Join(",", timeeventVm.Days);
            timeeventVm.BranchesDisplay = string.Join(",", timeeventVm.Branches);
            timeeventVm.CategoriesDisplay = string.Join(",", timeeventVm.Categories ?? new int[0]);
            return View("AddTimedEvent", timeeventVm);



        }
        [Manage(Config.Setup.TimedEvent)]

        public ActionResult DeleteTimedEvent(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TimedEventRepository.DeleteTimedEvent(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The timed event deleted successfully", AlertType.Success);
                return RedirectToAction("TimedEventList", "Setup");
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

            return RedirectToAction("TimedEventList", "Setup");

        }

        //Floor
        [View(Config.Setup.Floor)]

        public ActionResult FloorList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.FloorRepository.GetFloors((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        //Floor Partial
        [HttpGet]
        [Manage(Config.Setup.Floor)]

        public ActionResult AddFloorPartial()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            FloorViewModel floor = new FloorViewModel();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("FloorList");
            }
            ViewBag.edit = "AddFloor";
            return View(floor);
        }
        [HttpPost]
        [Manage(Config.Setup.Floor)]

        public ActionResult AddFloorPartial(FloorViewModel FloorMv)
        {

            ViewBag.edit = "AddFloor";
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
                    Floor floor = Mapper.Map<Floor>(FloorMv);
                    floor.StoreId = (int)user.StoreId;
                    _unitOfWork.FloorRepository.AddFloor(floor);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The floor added successfully", AlertType.Success);
                    return PartialView("Test");
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

            return PartialView("Test");


        }
        //Add Floor
        [HttpGet]
        [Manage(Config.Setup.Floor)]

        public ActionResult AddFloor()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            FloorViewModel floor = new FloorViewModel();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("FloorList");
            }
            ViewBag.edit = "AddFloor";
            return View(floor);
        }
        [HttpPost]
        [Manage(Config.Setup.Floor)]

        public ActionResult AddFloor(FloorViewModel FloorMv)
        {
            
            ViewBag.edit = "AddFloor";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(FloorMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Floor floor = Mapper.Map<Floor>(FloorMv);
                    floor.StoreId = (int)user.StoreId;
                    _unitOfWork.FloorRepository.AddFloor(floor);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The floor added successfully", AlertType.Success);
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

            return View(FloorMv);


        }
        [HttpGet]
        [Manage(Config.Setup.Floor)]

        public ActionResult UpdateFloor(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("FloorList");
            }
            ViewBag.edit = "UpdateFloor";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            FloorViewModel floorMv =
                Mapper.Map<FloorViewModel>(_unitOfWork.FloorRepository.GetFloorById(id, (int)user.StoreId));
            return View("AddFloor", floorMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Floor)]

        public ActionResult UpdateFloor(int id, FloorViewModel FloorMv)
        {
            ViewBag.edit = "UpdateFloor";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddFloor",FloorMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Floor floor = Mapper.Map<Floor>(FloorMv);
                    _unitOfWork.FloorRepository.UpdateFloor(id, floor, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The floor updated successfully", AlertType.Success);
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

            return View("AddFloor", FloorMv);


        }

        [Manage(Config.Setup.Floor)]

        public ActionResult DeleteFloor(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.FloorRepository.DeleteFloor(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The floor deleted successfully", AlertType.Success);
                return RedirectToAction("FloorList", "Setup");
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

            return RedirectToAction("FloorList", "Setup");

        }

        //DineTale
        [View(Config.Setup.DineTable)]

        public ActionResult DineTableList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DineTableRepository.GetDineTables((int)user.StoreId).Select(a => new DineTableListModelView { Id = a.Id, DineTableNumber = a.DineTableNumber, FloorNumber =a.Floor.FloorNumber }).OrderByDescending(a => a.Id));
        }

        [Manage(Config.Setup.DineTable)]

        public ActionResult AddDineTable()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DineTableViewModel dinetable  = new DineTableViewModel();
            dinetable.FloorDdl = _unitOfWork.FloorRepository.GetFloors((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FloorNumber }).AsEnumerable();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("DineTableList");
            }
            ViewBag.edit = "AddDineTable";
            return View(dinetable);
        }

        [HttpPost]
        [Manage(Config.Setup.DineTable)]

        public ActionResult AddDineTable(DineTableViewModel DineTableVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "AddDineTable";
            DineTableVm.FloorDdl = _unitOfWork.FloorRepository.GetFloors((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FloorNumber }).AsEnumerable();
            try
            {
                
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(DineTableVm);
                }
                else
                {
                    DineTableVm.StoreId = (int)user.StoreId;
                    DineTable DineTable = Mapper.Map<DineTable>(DineTableVm);
                    _unitOfWork.DineTableRepository.AddDineTable(DineTable);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The dine table added successfully", AlertType.Success);
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

            return View(DineTableVm);



        }

        [HttpGet]
        [Manage(Config.Setup.DineTable)]

        public ActionResult UpdateDineTable(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("DineTableList");
            }
            ViewBag.edit = "UpdateDineTable";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DineTableViewModel DineTableMv = Mapper.Map<DineTableViewModel>(_unitOfWork.DineTableRepository.GetDineTableById(id,(int)user.StoreId));
            DineTableMv.FloorDdl = _unitOfWork.FloorRepository.GetFloors((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FloorNumber }).AsEnumerable();
            return View("AddDineTable", DineTableMv);
        }
        [HttpPost]
        [Manage(Config.Setup.DineTable)]

        public ActionResult UpdateDineTable(int id, DineTableViewModel DineTableVm,int storeId)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DineTableVm.FloorDdl = _unitOfWork.FloorRepository.GetFloors((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FloorNumber }).AsEnumerable();
            ViewBag.edit = "UpdateDineTable";
            try
            {
                if (!ModelState.IsValid)
                {

                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddDineTable", DineTableVm);
                }
                else
                {
                    DineTable DineTable = Mapper.Map<DineTable>(DineTableVm);
                   
                    _unitOfWork.DineTableRepository.UpdateDineTable(id, DineTable, Convert.ToInt32(user.StoreId));
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The dine table updated successfully", AlertType.Success);
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

            return View("AddDineTable", DineTableVm);


        }
        [Manage(Config.Setup.DineTable)]

        public ActionResult DeleteDineTable(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.DineTableRepository.DeleteDineTable(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The dine table deleted successfully", AlertType.Success);
                return RedirectToAction("DineTableList", "Setup");
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

            return RedirectToAction("DineTableList", "Setup");


        }
        public JsonResult GetDepartmentDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<DepartmentViewModel[]>(_unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public JsonResult GetDesignationDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<DesignationViewModel[]>(_unitOfWork.DesignationRepository.GetDesignations((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //public JsonResult GetDesignationDdl()
        //{
        //    try
        //    {
        //        return Json(Mapper.Map<DesignationViewModel[]>(_unitOfWork.DesignationRepository.GetApiDesignations()), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}
        public JsonResult GetStateDdl()
        {
            try
            {
                return Json(Mapper.Map<StateModelView[]>(_unitOfWork.StateRepository.GetStates()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public JsonResult GetSizeDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<SizeViewModel[]>(_unitOfWork.SizeRepository.GetSizes((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public JsonResult GetFloorDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<FloorViewModel[]>(_unitOfWork.FloorRepository.GetFloors((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public JsonResult GetSupplierDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<SupplierModelView[]>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S",(int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ActionResult RolesList()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult AddRole()
        {
            RoleViewModel role = new RoleViewModel();
            role.RoleSecurityRightViewModels = _unitOfWork.SecurityObjectRepository.GetSecurityObjects().Select(a => new RoleSecurityRightViewModel
            {
                Manage = false,
                Module = a.Module,
                SecurityObject = a.Name,
                SecurityObjectId = a.SecurityObjectId
              ,
                View = true
            }).ToList();
            return View(role);
        }
        [HttpPost]
        public ActionResult AddRole(RoleViewModel role)
        {
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
                    if (RoleManager.RoleExists(role.Name))
                    {
                        ModelState.AddModelError("Role Already Exists", "");
                        return View(role);
                    }
                    else
                    {
                        var userId = this.HttpContext.User.Identity.GetUserId();
                        var user = UserManager.FindById(userId);
                        var roleAdd = new ApplicationRole
                        {
                            Name = role.Name,
                            StoreId = user.StoreId,
                            CreatedOn = DateTime.Now,
                            UpdatedOn = DateTime.Now,
                            CreatedById = userId,
                            UpdatedById = userId
                        };
                        RoleManager.Create(roleAdd);
                        foreach (var roleRoleSecurityRightViewModel in role.RoleSecurityRightViewModels)
                        {
                            _unitOfWork.SecurityRightRepository.AddSecurityRight(new SecurityRight
                            {
                                IdentityUserRoleId = roleAdd.Id,
                                Manage = roleRoleSecurityRightViewModel.Manage,
                                SecurityObjectId = roleRoleSecurityRightViewModel.SecurityObjectId,
                                View = roleRoleSecurityRightViewModel.View,
                                StoreId = (int)user.StoreId
                            });
                        }
                        _unitOfWork.Complete();
                    }
                    TempData["Alert"] = new AlertModel("The role added successfully", AlertType.Success);
                    return RedirectToAction("RolesList");
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

            return RedirectToAction("RolesList", "Setup");

        }
        public ActionResult UpdateRole(string id)
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            var role = RoleManager.Roles.Include(a=>a.SecurityRights).Include(a=>a.SecurityRights.Select(g=>g.SecurityObject)).Select(a => new RoleViewModel
            {
               
                StoreId = a.StoreId,
                Id = a.Id,
                Name = a.Name,
                
                CreatedBy = a.CreatedById,
                CreatedOn = a.CreatedOn,
                RoleSecurityRightViewModels = a.SecurityRights.Select(b=>new RoleSecurityRightViewModel
                {
                    Manage = b.Manage,
                    Module = b.SecurityObject.Module,
                    SecurityObject = b.SecurityObject.Name,
                    SecurityObjectId = b.SecurityObjectId
                    ,
                    View = b.View
                }).ToList()
                


            }).FirstOrDefault(a => a.StoreId == (int)user.StoreId && a.Id == id);
          
            return View("AddRole", role);
        }
        [HttpPost]
        public ActionResult UpdateRole(string id, RoleViewModel role)
        {
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
                   
                        var userId = this.HttpContext.User.Identity.GetUserId();
                        var user = UserManager.FindById(userId);
                    _unitOfWork.SecurityRightRepository.DeleteSecurityRightbyRole(id, user.StoreId);
                    _unitOfWork.Complete();
                    ApplicationRole roleUpdate =new ApplicationRole
                    {
                        Id = id,
                        Name = role.Name,
                        StoreId = user.StoreId,
                        CreatedOn = Convert.ToDateTime(role.CreatedOn),
                        CreatedById = role.CreatedBy,
                        UpdatedOn = DateTime.Now,
                        UpdatedById = userId,
                    };
                    _unitOfWork.UserRepository.UpdateRole(roleUpdate);
                    foreach (var roleRoleSecurityRightViewModel in role.RoleSecurityRightViewModels)
                    {
                        _unitOfWork.SecurityRightRepository.AddSecurityRight(new SecurityRight
                        {
                            IdentityUserRoleId = roleUpdate.Id,
                            Manage = roleRoleSecurityRightViewModel.Manage,
                            SecurityObjectId = roleRoleSecurityRightViewModel.SecurityObjectId,
                            View = roleRoleSecurityRightViewModel.View,
                            StoreId = (int)user.StoreId
                        });
                    }
                    _unitOfWork.Complete();
                        //RoleManager.Roles.(roleUpdate);
                    
                    TempData["Alert"] = new AlertModel("The role updated successfully", AlertType.Success);
                    return RedirectToAction("RolesList");
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

            return RedirectToAction("UserList", "User");

        }

        public ActionResult DeleteRole(string id)
        {
            try
            {
                var userId = this.HttpContext.User.Identity.GetUserId();
                var user = UserManager.FindById(userId);
                RoleManager.Delete(RoleManager.Roles.FirstOrDefault(a => a.Id == id && a.StoreId == (int)user.StoreId));
                TempData["Alert"] = new AlertModel("The role deleted successfully", AlertType.Success);
                return RedirectToAction("RolesList");
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

            return RedirectToAction("RolesList", "Setup");


        }
        //Assign Role

        [HttpGet]
        public ActionResult AssignRolesToUsers()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

            AssignRoleViewModel asignRole = new AssignRoleViewModel();
            asignRole.Userlist = _unitOfWork.UserRepository.GetUsers((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.UserName, Value = a.Id });
            asignRole.UserRolesList = RoleManager.Roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Name });

            return View(asignRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRolesToUsers(AssignRoleViewModel _assignRole)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _assignRole.Userlist = _unitOfWork.UserRepository.GetUsers((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.UserName, Value = a.Id });
            _assignRole.UserRolesList = RoleManager.Roles.Select(a => new SelectListItem { Text = a.Name, Value = a.Name });
            if (ModelState.IsValid)
            {
                if (!UserManager.IsInRole(_assignRole.UserName, _assignRole.RoleName))
                {

                    var userRoles = UserManager.GetRoles(userid);
                    UserManager.RemoveFromRoles(userid, userRoles.ToArray());
                    UserManager.AddToRole(_assignRole.UserName, _assignRole.RoleName);
                    TempData["Alert"] = new AlertModel("Username added to role successfully !", AlertType.Success);
                    _unitOfWork.Complete();
                }
                else
                {
                    TempData["Alert"] = new AlertModel("Username already exist in role!", AlertType.Information);

                    return View(_assignRole);
                }



            }

            return RedirectToAction("RolesList", "Setup");
        }


        //POSTerminal
        [View(Config.Setup.PosTerminal)]

        public ActionResult POSTerminalList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<POSTerminalListModelView[]>(_unitOfWork.POSTerminalRepository.GetPOSTerminals((int)user.StoreId)).OrderByDescending(a => a.POSTerminalId));
        }
        [Manage(Config.Setup.PosTerminal)]

        public ActionResult AddPOSTerminal()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            POSTerminalViewModel POSTerminal = new POSTerminalViewModel();
            POSTerminal.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("POSTerminalList");
            }
            ViewBag.edit = "AddPOSTerminal";
            return View(POSTerminal);
        }

        [HttpPost]
        [Manage(Config.Setup.PosTerminal)]

        public ActionResult AddPOSTerminal(POSTerminalViewModel POSTerminalVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "AddPOSTerminal";
            POSTerminalVm.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            try
            {
                ;
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(POSTerminalVm);
                }
                else
                {
                    POSTerminalVm.StoreId = (int)user.StoreId;
                    POSTerminal POSTerminal = Mapper.Map<POSTerminal>(POSTerminalVm);
                    _unitOfWork.POSTerminalRepository.AddPOSTerminal(POSTerminal);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The posterminal added successfully", AlertType.Success);
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

            return View(POSTerminalVm);


        }

        [HttpGet]
        [Manage(Config.Setup.PosTerminal)]

        public ActionResult UpdatePOSTerminal(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("POSTerminalList");
            }
            ViewBag.edit = "UpdatePOSTerminal";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            POSTerminalViewModel POSTerminalMv = Mapper.Map<POSTerminalViewModel>(_unitOfWork.POSTerminalRepository.GetPOSTerminalById(id, (int)user.StoreId));
            POSTerminalMv.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddPOSTerminal", POSTerminalMv);
        }
        [HttpPost]
        [Manage(Config.Setup.PosTerminal)]

        public ActionResult UpdatePOSTerminal(int id, POSTerminalViewModel POSTerminalVm, int storeId)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "UpdatePOSTerminal";
            POSTerminalVm.SectionDdl = _unitOfWork.SectionRepository.GetSections((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.SectionId.ToString(), Text = a.Name }).AsEnumerable();
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddPOSTerminal", POSTerminalVm);
                }
                else
                {
                    POSTerminal POSTerminal = Mapper.Map<POSTerminal>(POSTerminalVm);
                    
                    _unitOfWork.POSTerminalRepository.UpdatePOSTerminal(id, POSTerminal, Convert.ToInt32(user.StoreId));
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The posterminal updated successfully", AlertType.Success);
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

            return View("AddPOSTerminal", POSTerminalVm);

        }
        [Manage(Config.Setup.PosTerminal)]

        public ActionResult DeletePOSTerminal(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.POSTerminalRepository.DeletePOSTerminal(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The posterminal deleted successfully", AlertType.Success);
                return RedirectToAction("POSTerminalList", "Setup");
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

            return RedirectToAction("POSTerminalList", "Setup");


        }

        //Shift
        [View(Config.Setup.Shift)]

        public ActionResult ShiftList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ShiftRepository.GetShifts((int)user.StoreId).OrderByDescending(a => a.ShiftId));
        }
        [HttpGet]
        [Manage(Config.Setup.Shift)]

        public ActionResult AddShift()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ShiftViewModel Shift = new ShiftViewModel();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ShiftList");
            }
            ViewBag.edit = "AddShift";
            return View(Shift);
        }
        [HttpPost]
        [Manage(Config.Setup.Shift)]

        public ActionResult AddShift(ShiftViewModel ShiftMv)
        {

            ViewBag.edit = "AddShift";
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                    return View(ShiftMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Shift Shift = Mapper.Map<Shift>(ShiftMv);
                    Shift.StoreId = Convert.ToInt32(user.StoreId);
                    _unitOfWork.ShiftRepository.AddShift(Shift);
                    _unitOfWork.Complete();
                    TempData["Alert"]= new AlertModel("The Shift added successfully", AlertType.Success);
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

            return View(ShiftMv);


        }
        [HttpGet]
        [Manage(Config.Setup.Shift)]

        public ActionResult UpdateShift(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("ShiftList");
            }
            ViewBag.edit = "UpdateShift";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ShiftViewModel ShiftMv =
                Mapper.Map<ShiftViewModel>(_unitOfWork.ShiftRepository.GetShiftById(id, (int)user.StoreId));
            return View("AddShift", ShiftMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Shift)]

        public ActionResult UpdateShift(int id, ShiftViewModel ShiftMv)
        {
            ViewBag.edit = "UpdateShift";
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again", AlertType.Error);
                    return View("AddShift", ShiftMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Shift Shift = Mapper.Map<Shift>(ShiftMv);
                    _unitOfWork.ShiftRepository.UpdateShift(id, Shift, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The Shift Updated successfully", AlertType.Success);
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

            return View("AddShift", ShiftMv);



        }
        [Manage(Config.Setup.Shift)]

        public ActionResult DeleteShift(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.ShiftRepository.DeleteShift(id, (int) user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Shift Deleted successfully", AlertType.Success);
                return RedirectToAction("ShiftList", "Setup");
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
            return RedirectToAction("ShiftList", "Setup");
        }

        public ActionResult TillOperationDetails(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var data = _unitOfWork.TillOperationRepository.GetTillOperationsById(id, user.StoreId);
            return View(data);
        }

        [View(Config.Setup.TillOperation)]

        public ActionResult TillOperationList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            
                return View(_unitOfWork.TillOperationRepository.GetTillOperations((int)user.StoreId).Select(a => new TillOperationListModelView
                {

                    Id = a.Id,
                    ShiftName = a.ShiftId!=null?a.Shift.Name:"",
                    OpeningAmount = a.OpeningAmount,
                    OperationDate = a.OperationDate,

                    PhysicalAmount = a.PhysicalAmount,
                    Remarks = a.Remarks,
                    Status = a.Status,
                    SystemAmount = a.SystemAmount,
                    TillOperationType = a.TillOperationType
                }));
            }
            
              
            
           
           
        

        [HttpGet]
        [Manage(Config.Setup.TillOperation)]

        public ActionResult AddTillOperation()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TillOperationViewModel tillMv = new TillOperationViewModel();
            tillMv.ShiftDdl = _unitOfWork.ShiftRepository.GetShifts((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.ShiftId.ToString(), Text = a.Name }).AsEnumerable();
            ViewBag.edit = "AddTillOperation";
            return View(tillMv);
        }
        [HttpPost]
        [Manage(Config.Setup.TillOperation)]

        public ActionResult AddTillOperation(TillOperationViewModel tillMv)
        {
            ViewBag.edit = "AddTillOperation";
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
                    TillOperation to = Mapper.Map<TillOperation>(tillMv);
                    to.StoreId = (int)user.StoreId;
                    to.ApplicationUserId = userid;
                    _unitOfWork.TillOperationRepository.AddTillOperation(to);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The tillOperation updated successfully", AlertType.Success);
                    return RedirectToAction("TillOperationList", "Setup");
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

            return RedirectToAction("TillOperationList", "Setup");


        }
        [HttpGet]
        [Manage(Config.Setup.TillOperation)]

        public ActionResult UpdateTillOperation(int id)
        {
            ViewBag.edit = "UpdateTillOperation";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TillOperationViewModel tillVm = Mapper.Map<TillOperationViewModel>(_unitOfWork.TillOperationRepository.GetTillOperationsById(id, (int)user.StoreId));
            return View("AddTillOperation",tillVm);
        }

        [HttpPost]
        [Manage(Config.Setup.TillOperation)]

        public ActionResult UpdateTillOperation(int id,TillOperationViewModel tillVm)
        {
            ViewBag.edit = "UpdateShift";
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
                    TillOperation to = Mapper.Map<TillOperation>(tillVm);
                    _unitOfWork.TillOperationRepository.UpdateTillOperations(id, (int)user.StoreId, to);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The tilloperation updated successfully", AlertType.Success);
                    return RedirectToAction("TillOperationList", "Setup");
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

            return RedirectToAction("TillOperationList", "Setup");


        }
        [Manage(Config.Setup.TillOperation)]

        public ActionResult DeleteTillOperation(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TillOperationRepository.DeleteTillOperations(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The tilloperation deleted successfully", AlertType.Success);
                return RedirectToAction("TillOperationList", "Setup");
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

            return RedirectToAction("TillOperationList", "Setup");

        }

        //Size
        [View(Config.Setup.Size)]

        public ActionResult SizeList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.SizeRepository.GetSizes((int)user.StoreId).OrderByDescending(a => a.Id));
        }
        //Size Partial
        [HttpGet]
        [Manage(Config.Setup.Size)]

        public ActionResult AddSizePartial()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            SizeViewModel size = new SizeViewModel();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SizeList");
            }
            ViewBag.edit = "AddSize";
            return View(size);
        }
        [HttpPost]
        [Manage(Config.Setup.Size)]

        public ActionResult AddSizePartial(SizeViewModel SizeMv)
        {

            ViewBag.edit = "AddSize";
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
                    Size size = Mapper.Map<Size>(SizeMv);
                    size.StoreId = (int)user.StoreId;
                    _unitOfWork.SizeRepository.AddSize(size);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The size added successfully", AlertType.Success);
                    return PartialView("Test");
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

            return PartialView("Test");


        }
        //Add Size
        [HttpGet]
        [Manage(Config.Setup.Size)]

        public ActionResult AddSize()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            SizeViewModel size = new SizeViewModel();
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SizeList");
            }
            ViewBag.edit = "AddSize";
            return View(size);
        }
        [HttpPost]
        [Manage(Config.Setup.Size)]

        public ActionResult AddSize(SizeViewModel SizeMv)
        {

            ViewBag.edit = "AddSize";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(SizeMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Size size = Mapper.Map<Size>(SizeMv);
                    size.StoreId = (int)user.StoreId;
                    _unitOfWork.SizeRepository.AddSize(size);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The size added successfully", AlertType.Success);
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

            return View(SizeMv);


        }
        [HttpGet]
        [Manage(Config.Setup.Size)]

        public ActionResult UpdateSize(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("SizeList");
            }
            ViewBag.edit = "UpdateSize";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            SizeViewModel sizeMv =
                Mapper.Map<SizeViewModel>(_unitOfWork.SizeRepository.GetSizeById(id, (int)user.StoreId));
            return View("AddSize", sizeMv);
        }
        [HttpPost]
        [Manage(Config.Setup.Size)]

        public ActionResult UpdateSize(int id, SizeViewModel SizeMv)
        {
            ViewBag.edit = "UpdateSize";
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddSize", SizeMv);
                }
                else
                {
                    var userid = User.Identity.GetUserId();
                    var user = UserManager.FindById(userid);
                    Size size = Mapper.Map<Size>(SizeMv);
                    _unitOfWork.SizeRepository.UpdateSize(id, size, (int)user.StoreId);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The size updated successfully", AlertType.Success);
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

            return View("AddSize", SizeMv);


        }
        [Manage(Config.Setup.Size)]

        public ActionResult DeleteSize(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.SizeRepository.DeleteSize(id, (int)user.StoreId);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The size deleted successfully", AlertType.Success);
                return RedirectToAction("SizeList", "Setup");
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

            return RedirectToAction("SizeList", "Setup");

        }

        //Warehouse
        [View(Config.Setup.WareHouse)]

        public ActionResult WarehouseList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);

            return View(_unitOfWork.WarehouseRepository.GetWarehouses().Select(a => new WarehouseListModelView { Id = a.Id, Name = a.Name, ClientName = a.Client.Name }).OrderByDescending(a => a.Id));
        }
        [Manage(Config.Setup.WareHouse)]

        public ActionResult AddWarehouse()
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("WarehouseList");
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
           
            
            WarehouseViewModel warehouse = new WarehouseViewModel();
            warehouse.ClientDdl = _unitOfWork.ClientRepository.GetClients()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name,Selected = a.Id ==a.Id}).AsEnumerable();
            ViewBag.edit = "AddWarehouse";
            return View(warehouse);
        }

        [HttpPost]
        [Manage(Config.Setup.WareHouse)]

        public ActionResult AddWarehouse(WarehouseViewModel warehouseVm)
        {
            ViewBag.edit = "AddWarehouse";
            warehouseVm.ClientDdl = _unitOfWork.ClientRepository.GetClients()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View(warehouseVm);
                }
                else
                {
                    Warehouse warehouse = Mapper.Map<Warehouse>(warehouseVm);
                    _unitOfWork.WarehouseRepository.AddWarehouse(warehouse);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The warehouse added successfully", AlertType.Success);
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

            return View(warehouseVm);


        }

        [HttpGet]
        [Manage(Config.Setup.WareHouse)]

        public ActionResult UpdateWarehouse(int id)
        {
            var isAjax = Request.IsAjaxRequest();
            if (!isAjax)
            {
                return RedirectToAction("WarehouseList");
            }
            ViewBag.edit = "UpdateWarehouse";
            WarehouseViewModel warehouseMv = Mapper.Map<WarehouseViewModel>(_unitOfWork.WarehouseRepository.GetWarehouse(id));
            warehouseMv.ClientDdl = _unitOfWork.ClientRepository.GetClients()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddWarehouse", warehouseMv);
        }
        [HttpPost]
        [Manage(Config.Setup.WareHouse)]

        public ActionResult UpdateWarehouse(int id, WarehouseViewModel warehouseVm)
        {
            ViewBag.edit = "UpdateWarehouse";
            warehouseVm.ClientDdl = _unitOfWork.ClientRepository.GetClients()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    TempData["Alert"] = new AlertModel("ModelState Failure, try again. " + message, AlertType.Error);
                    return View("AddWarehouse", warehouseVm);
                }
                else
                {
                    Warehouse warehouse = Mapper.Map<Warehouse>(warehouseVm);
                    _unitOfWork.WarehouseRepository.UpdateWarehouse(id, warehouse);
                    _unitOfWork.Complete();
                    TempData["Alert"] = new AlertModel("The warehouse updated successfully", AlertType.Success);
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

            return View("AddWarehouse", warehouseVm);

        }
        [Manage(Config.Setup.WareHouse)]

        public ActionResult DeleteWarehouse(int id)
        {
            try
            {
                _unitOfWork.WarehouseRepository.DeleteWarehouse(id);
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The warehouse deleted successfully", AlertType.Success);
                return RedirectToAction("WarehouseList", "Setup");
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

            return RedirectToAction("WarehouseList", "Setup");

        }

        public JsonResult GetClientDdl()
        {
            try
            {
                return Json(Mapper.Map<ClientViewModel[]>(_unitOfWork.ClientRepository.GetClients()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}