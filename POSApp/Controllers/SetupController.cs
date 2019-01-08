using System;
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

namespace POSApp.Controllers
{
    [Authorize]
    public class SetupController : Controller
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
        public ActionResult DepartmentList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId));
        }
        public ActionResult AddDepartmentPartial()
        {
            var isAjax = Request.IsAjaxRequest();
            if (isAjax)
            {
            ViewBag.edit = "AddDepartmentPartial";
            return View();
            }
            else
            {
                return RedirectToAction("DepartmentList");
            }
        }
        [HttpPost]
        public ActionResult AddDepartmentPartial(DepartmentViewModel departmentVm)
        {
            ViewBag.edit = "AddDepartmentPartial";
            if (!ModelState.IsValid)
            {
                return View(departmentVm);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                Department department = Mapper.Map<Department>(departmentVm);
                department.StoreId = (int) user.StoreId;
                _unitOfWork.DepartmentRepository.AddDepartment(department);
                _unitOfWork.Complete();
                return PartialView("Test");
            }

        }


        public ActionResult AddDesignationPartial()
        {
          
            ViewBag.edit = "AddDesignationPartial";
            return View();
        }
        //[HttpPost]
        //public ActionResult AddDesignationPartial(DesignationViewModel designationVm)
        //{
        //    ViewBag.edit = "AddDesignationPartial";
        //    if (!ModelState.IsValid)
        //    {
        //        return View(designationVm);
        //    }
        //    else
        //    {
        //        Designation designation = Mapper.Map<Designation>(designationVm);
        //        _unitOfWork.DesignationRepository.AddDesignation(designation);
        //        _unitOfWork.Complete();
        //        return PartialView("Test");
        //    }

        //}




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

        //public ActionResult DesignationList()
        //{
        //    return View(_unitOfWork.DesignationRepository.GetDesignations());
        //}
        [HttpGet]
        public ActionResult AddDesignation()
        {
            ViewBag.edit = "AddDesignation";
            return View();
        }
        // [HttpPost]
        //public ActionResult AddDesignation(DesignationViewModel designationVm)
        //{
        //    ViewBag.edit = "AddDesignation";
        //    if (!ModelState.IsValid)
        //    {
        //        return View(designationVm);
        //    }

        //    Designation designation = Mapper.Map<Designation>(designationVm);
        //    _unitOfWork.DesignationRepository.AddDesignation(designation);
        //    _unitOfWork.Complete();
        //    return RedirectToAction("DesignationList", "Setup");
        //}

        //public ActionResult UpdateDesignation(int id)
        //{
        //    ViewBag.edit = "UpdateDesignation";
        //    DesignationViewModel designationVm =
        //        Mapper.Map<DesignationViewModel>(_unitOfWork.DesignationRepository.GetDesignationById(id));
        //    return View("AddDesignation",designationVm);
        //}
        //[HttpPost]
        //public ActionResult UpdateDesignation(int id, DesignationViewModel designationVm)
        //{
        //    ViewBag.edit = "UpdateDesignation";
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("AddDesignation", designationVm);
        //    }
        //    else
        //    {
        //        Designation designation = Mapper.Map<Designation>(designationVm);
        //        _unitOfWork.DesignationRepository.UpdateDesignation(id,designation);
        //        _unitOfWork.Complete();
        //        return RedirectToAction("DesignationList", "Setup");
        //    }
        //}
        //public ActionResult DeleteDesignation(int id)
        //{
        //    _unitOfWork.DesignationRepository.DeleteDesignation(id);
        //    _unitOfWork.Complete();
        //    return RedirectToAction("DesignationList","Setup");
        //}
        [HttpGet]
        public ActionResult EmployeeList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View("EmployeeList", _unitOfWork.EmployeeRepository.GetEmployees((int)user.StoreId));
        }
        //Add partial
        [HttpGet]
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

            ViewBag.edit = "AddEmployeePartial";
            return View(employee);
        }
        [HttpPost]
        public ActionResult AddEmployeePartial(EmployeeModelView employeeMv)
        {
            ViewBag.edit = "AddEmployeePartial";
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
        //Add Employee
        [HttpGet]
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
            
            ViewBag.edit = "AddEmployee";
            return View(employee);
        }
        [HttpPost]
        public ActionResult AddEmployee(EmployeeModelView employeeMv)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "AddEmployee";
            employeeMv.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
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
           
            return View("AddEmployee",employeeMv);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(int id, EmployeeModelView employeeMv)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.edit = "UpdateEmployee";
            employeeMv.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
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

        public ActionResult CustomerList()
        {

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<CustomerModelView[]>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartners("C",(int)user.StoreId)));
        }
        [HttpGet]
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

        public ActionResult SupplierList()
        {
        
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<SupplierModelView[]>(_unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId)));
        }
        [HttpGet]
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

        public ActionResult StateList()
        {
            return View(_unitOfWork.StateRepository.GetStates());
        }
        [HttpGet]
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

        public ActionResult LocationList()
        {
            return View(_unitOfWork.LocationRepository.GetLocations());
        }
        [HttpGet]
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

        public ActionResult DiscountList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DiscountRepository.GetDiscounts((int)user.StoreId));
        }
        [HttpGet]
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

        public ActionResult TaxList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.TaxRepository.GetTaxes((int)user.StoreId));
        }
        [HttpGet]
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
        public ActionResult UnitList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.UnitRepository.GetUnit((int)user.StoreId));
        }
        //Unit Partial
        [HttpGet]
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
        public ActionResult ClientList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ClientRepository.GetClients());
        }
        [HttpGet]
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

        public ActionResult TimedEventList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.TimedEventRepository.GetTimedEvents((int)user.StoreId));
        }
        [HttpGet]
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
            model.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.ProductCode });
            model.BranchDdl = _unitOfWork.StoreRepository.GetStores()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            return View(model);
        }
        [HttpPost]
        public ActionResult AddTimedEvent(TimedEventViewModel timeeventVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            timeeventVm.CatDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            timeeventVm.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.ProductCode });
            timeeventVm.BranchDdl = _unitOfWork.StoreRepository.GetStores()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            ViewBag.edit = "AddTimedEvent";
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
            timeeventVm.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.ProductCode });
            timeeventVm.BranchDdl = _unitOfWork.StoreRepository.GetStores()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            ViewBag.alert = "<script> $(document).ready(function() {$('#CategoriesArea').css('display', 'none');$('#BranchesArea').css('display', 'none');" +
                            @"});</script>";
            return View("AddTimedEvent", timeeventVm);
        }
        [HttpPost]
        public ActionResult UpdateTimedEvent(int id, TimedEventViewModel timeeventVm)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            timeeventVm.CatDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            timeeventVm.ProductDdl = _unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId)
                .Select(a => new SelectListItem { Text = a.Name, Value = a.ProductCode });
            timeeventVm.BranchDdl = _unitOfWork.StoreRepository.GetStores()
                .Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
            ViewBag.edit = "UpdateTimedEvent";
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
                    return View("AddTimedEvent", timeeventVm);
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

        public ActionResult FloorList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.FloorRepository.GetFloors((int)user.StoreId));
        }
        //Floor Partial
        [HttpGet]
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

        public ActionResult DineTableList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DineTableRepository.GetDineTables((int)user.StoreId).Select(a => new DineTableListModelView { Id = a.Id, DineTableNumber = a.DineTableNumber, FloorNumber =a.Floor.FloorNumber }));
        }
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
            RoleViewModel role=new RoleViewModel();
            role.RoleSecurityRightViewModels = _unitOfWork.SecurityObjectRepository.GetSecurityObjects().Select(a=>new RoleSecurityRightViewModel
            {
                Manage = false,
                Module = a.Module,
                SecurityObject = a.Name,
                SecurityObjectId = a.SecurityObjectId
              ,View = true
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
            return View("AddRole", RoleManager.Roles.Select(a=>new RoleViewModel{StoreId = a.StoreId,
                Id = a.Id,Name = a.Name,CreatedBy = a.CreatedById,CreatedOn = a.CreatedOn}).FirstOrDefault(a=>a.StoreId==(int)user.StoreId && a.Id==id));
        }
        [HttpPost]
        public ActionResult UpdateRole(string id,RoleViewModel role)
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
                        return View("AddRole", role);
                    }
                    else
                    {
                        var userId = this.HttpContext.User.Identity.GetUserId();
                        var user = UserManager.FindById(userId);

                        RoleManager.Update(new ApplicationRole
                        {
                            Id = id,
                            Name = role.Name,
                            StoreId = user.StoreId,
                            CreatedOn = Convert.ToDateTime(role.CreatedOn),
                            CreatedById = role.CreatedBy,
                            UpdatedOn = DateTime.Now,
                            UpdatedById = userId
                        });
                    }
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

            return RedirectToAction("RolesList", "Setup");

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

        //POSTerminal

        public ActionResult POSTerminalList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<POSTerminalListModelView[]>(_unitOfWork.POSTerminalRepository.GetPOSTerminals((int)user.StoreId)));
        }
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

        public ActionResult ShiftList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ShiftRepository.GetShifts((int)user.StoreId));
        }
        [HttpGet]
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

        public ActionResult TillOperationList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.TillOperationRepository.GetTillOperations((int)user.StoreId).Select(a => new TillOperationListModelView
            {
                Id = a.Id, ShiftName = a.Shift.Name,OpeningAmount = a.OpeningAmount,OperationDate = a.OperationDate
               ,PhysicalAmount = a.PhysicalAmount,Remarks = a.Remarks,Status = a.Status,SystemAmount = a.SystemAmount,
                TillOperationType = a.TillOperationType
            })); 
        }

        [HttpGet]
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
        public ActionResult UpdateTillOperation(int id)
        {
            ViewBag.edit = "UpdateTillOperation";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            TillOperationViewModel tillVm = Mapper.Map<TillOperationViewModel>(_unitOfWork.TillOperationRepository.GetTillOperationsById(id, (int)user.StoreId));
            return View("AddTillOperation",tillVm);
        }

        [HttpPost]
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

    }
}