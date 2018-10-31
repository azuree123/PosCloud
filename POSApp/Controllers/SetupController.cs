using System;
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
    public class SetupController : Controller
    {
        private ApplicationUserManager _userManager;
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
        public ActionResult AddDepartmentPartial()
        {
            ViewBag.edit = "AddDepartmentPartial";
            return View();
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
                Department department = Mapper.Map<Department>(departmentVm);
                _unitOfWork.DepartmentRepository.AddDepartment(department);
                _unitOfWork.Complete();
                return PartialView("Error");
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
        //        return PartialView("Error");
        //    }

        //}




        public ActionResult AddStatePartial()
        {
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
                return PartialView("Error");
            }

        }




        public ActionResult AddSupplierPartial()
        {
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
                Supplier supplier = Mapper.Map<Supplier>(suppliervm);
                _unitOfWork.SupplierRepository.AddSupplier(supplier);
                _unitOfWork.Complete();
                return PartialView("Error");
            }

        }




        //public ActionResult AddEmployeePartial()
        //{
        //    ViewBag.edit = "AddEmployeePartial";
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddEmployeePartial(EmployeeModelView employeevm)
        //{
        //    ViewBag.edit = "AddEmployeePartial";
        //    if (!ModelState.IsValid)
        //    {
        //        return View(employeevm);
        //    }
        //    else
        //    {
        //        var userid = User.Identity.GetUserId();
        //        var user = UserManager.FindById(userid);
        //        employeevm.StoreId = user.StoreId;
        //        Employee employee = Mapper.Map<Employee>(employeevm);
        //        _unitOfWork.EmployeeRepository.AddEmployee(employee);
        //        _unitOfWork.Complete();
        //        return PartialView("Error");
        //    }

        //}







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
            return View("EmployeeList", _unitOfWork.EmployeeRepository.GetEmployees());
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            EmployeeModelView employee = new EmployeeModelView();
            employee.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments()
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
                employeeMv.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments()
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                return View(employeeMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                employeeMv.StoreId = user.StoreId;
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
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            EmployeeModelView employeeMv = Mapper.Map<EmployeeModelView>(_unitOfWork.EmployeeRepository.GetEmployeeById(id, Convert.ToInt32(user.StoreId)));
            employeeMv.DepartmentDdl = _unitOfWork.DepartmentRepository.GetDepartments()
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
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.EmployeeRepository.UpdateEmployee(id,employee,Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                return RedirectToAction("EmployeeList","Setup");
            }
            
        }
        public ActionResult DeleteEmployee(int id, int storeid)
        {
            _unitOfWork.EmployeeRepository.DeleteEmployee(id, storeid);
            _unitOfWork.Complete();
            return RedirectToAction("EmployeeList","Setup");
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
                var userid= User.Identity.GetUserId();
                var user =  UserManager.FindById(userid);
                customerMv.StoreId = user.StoreId;
                BusinessPartner customer = Mapper.Map<BusinessPartner>(customerMv);
                customer.Type = "C";
                _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(customer);
                _unitOfWork.Complete();
                return RedirectToAction("CustomerList","Setup");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateCustomer(int id)
        {
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
            if (!ModelState.IsValid)
            {
                return View("AddCustomer",customerMv);
            }
            else
            {
                BusinessPartner customer = Mapper.Map<BusinessPartner>(customerMv);
                customer.Type = "C";
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.BusinessPartnerRepository.UpdateBusinessPartner(id, Convert.ToInt32(user.StoreId), customer);
                _unitOfWork.Complete();
                return RedirectToAction("CustomerList", "Setup");
            }
            
        }
        public ActionResult DeleteCustomer(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.BusinessPartnerRepository.DeleteBusinessPartner(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("CustomerList","Setup");
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
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                supplierMv.StoreId = user.StoreId;
               
                BusinessPartner supplier = Mapper.Map<BusinessPartner>(supplierMv);
                supplier.Birthday=DateTime.Now;
                supplier.Type = "S";
                _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(supplier);
                _unitOfWork.Complete();
                return RedirectToAction("SupplierList","Setup");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateSupplier(int id)
        {
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
            if (!ModelState.IsValid)
            {
                return View("AddSupplier", supplierVm);
            }

            {
                BusinessPartner supplier = Mapper.Map<BusinessPartner>(supplierVm);
                supplier.Type = "S";
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.BusinessPartnerRepository.UpdateBusinessPartner(id,Convert.ToInt32(user.StoreId),supplier);
                _unitOfWork.Complete();
                return RedirectToAction("SupplierList", "Setup");
            }
           
        }
        public ActionResult DeleteSupplier(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.BusinessPartnerRepository.DeleteBusinessPartner(id, Convert.ToInt32(user.StoreId));
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

        public ActionResult DiscountList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.DiscountRepository.GetDiscounts((int)user.StoreId));
        }
        [HttpGet]
        public ActionResult AddDiscount()
        {
            ViewBag.edit = "AddDiscount";
            return View();
        }
        [HttpPost]
        public ActionResult AddDiscount(DiscountViewModel dicountMv)
        {
            
            ViewBag.edit = "AddDiscount";
            if (!ModelState.IsValid)
            {
                return View(dicountMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                Discount discount = Mapper.Map<Discount>(dicountMv);
                discount.StoreId = (int)user.StoreId;
                _unitOfWork.DiscountRepository.AddDiscount(discount);
                _unitOfWork.Complete();
                return RedirectToAction("DiscountList", "Setup");
            }

        }
        [HttpGet]
        public ActionResult UpdateDiscount(int id)
        {
            ViewBag.edit = "UpdateDiscount";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DiscountViewModel discountMv =
                Mapper.Map<DiscountViewModel>(_unitOfWork.DiscountRepository.GetDiscountById(id, (int)user.StoreId));
            return View("AddDiscount", discountMv);
        }
        [HttpPost]
        public ActionResult UpdateDiscount(int id, DiscountViewModel discountMv)
        {
            
            ViewBag.edit = "UpdateDiscount";
            if (!ModelState.IsValid)
            {
                return View("AddDiscount", discountMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                Discount discount = Mapper.Map<Discount>(discountMv);
                _unitOfWork.DiscountRepository.UpdateDiscount(id, discount, (int)user.StoreId);
                _unitOfWork.Complete();
                return RedirectToAction("DiscountList", "Setup");
            }

        }
        public ActionResult DeleteDiscount(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.DiscountRepository.DeleteDiscount(id, (int)user.StoreId);
            _unitOfWork.Complete();
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
            ViewBag.edit = "AddTax";
            return View();
        }
        [HttpPost]
        public ActionResult AddTax(TaxViewModel taxMv)
        {

            ViewBag.edit = "AddTax";
            if (!ModelState.IsValid)
            {
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
                return RedirectToAction("TaxList", "Setup");
            }

        }
        [HttpGet]
        public ActionResult UpdateTax(int id)
        {
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
            if (!ModelState.IsValid)
            {
                return View("AddTax", taxMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                Tax tax = Mapper.Map<Tax>(taxMv);
                _unitOfWork.TaxRepository.UpdateTax(id, tax, (int)user.StoreId);
                _unitOfWork.Complete();
                return RedirectToAction("TaxList", "Setup");
            }

        }
        public ActionResult DeleteTax(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.TaxRepository.DeleteTax(id, (int)user.StoreId);
            _unitOfWork.Complete();
            return RedirectToAction("TaxList", "Setup");
        }

        public ActionResult CouponList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.CouponRepository.GetCoupons((int)user.StoreId));
        }
        [HttpGet]
        public ActionResult AddCoupon()
        {
            ViewBag.edit = "AddCoupon";
            return View();
        }
        [HttpPost]
        public ActionResult AddCoupon(CouponModelView couponMv)
        {
            couponMv.Days = string.Join(",", couponMv.tempDays);
            ViewBag.edit = "AddCoupon";
            if (!ModelState.IsValid)
            {
                return View(couponMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                Coupon location = Mapper.Map<Coupon>(couponMv);
                location.StoreId= (int)user.StoreId;
                _unitOfWork.CouponRepository.AddCoupon(location);
                _unitOfWork.Complete();
                return RedirectToAction("CouponList", "Setup");
            }

        }
        [HttpGet]
        public ActionResult UpdateCoupon(int id)
        {
            ViewBag.edit = "UpdateCoupon";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            CouponModelView couponMv =
                Mapper.Map<CouponModelView>(_unitOfWork.CouponRepository.GetCouponById(id, (int)user.StoreId));
            couponMv.tempDays = couponMv.Days.Split(',');
            return View("AddCoupon", couponMv);
        }
        [HttpPost]
        public ActionResult UpdateCoupon(int id, CouponModelView couponMv)
        {
            couponMv.Days = string.Join(",", couponMv.tempDays);
            ViewBag.edit = "UpdateCoupon";
            if (!ModelState.IsValid)
            {
                return View("AddCoupon", couponMv);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                Coupon location = Mapper.Map<Coupon>(couponMv);
                _unitOfWork.CouponRepository.UpdateCoupon(id, location, (int)user.StoreId);
                _unitOfWork.Complete();
                return RedirectToAction("CouponList", "Setup");
            }

        }
        public ActionResult DeleteCoupon(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.CouponRepository.DeleteCoupon(id, (int)user.StoreId);
            _unitOfWork.Complete();
            return RedirectToAction("CouponList", "Setup");
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
            ViewBag.edit = "AddClient";
            return View();
        }
        [HttpPost]
        public ActionResult AddClient(Client clientMv, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddClient";
            if (!ModelState.IsValid)
            {
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
                _unitOfWork.ClientRepository.AddClient(clientMv);
                _unitOfWork.Complete();
                return RedirectToAction("ClientList", "Setup");
            }

        }
        [HttpGet]
        public ActionResult UpdateClient(int id)
        {
            ViewBag.edit = "UpdateClient";
            
            Client clientMv =
                (_unitOfWork.ClientRepository.GetClient(id));
            return View("AddClient", clientMv);
        }
        [HttpPost]
        public ActionResult UpdateClient(int id, Client clientMv, HttpPostedFileBase file)
        {
            ViewBag.edit = "UpdateClient";
            if (!ModelState.IsValid)
            {
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
                _unitOfWork.ClientRepository.UpdateClient(id, clientMv);
                _unitOfWork.Complete();
                return RedirectToAction("ClientList", "Setup");
            }

        }
        public ActionResult DeleteClient(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ClientRepository.DeleteClient(id);
            _unitOfWork.Complete();
            return RedirectToAction("ClientList", "Setup");
        }
        public JsonResult GetDepartmentDdl()
        {
            try
            {
                return Json(Mapper.Map<DepartmentViewModel[]>(_unitOfWork.DepartmentRepository.GetDepartments()), JsonRequestBehavior.AllowGet);
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
        public JsonResult GetSupplierDdl()
        {
            try
            {
                return Json(Mapper.Map<SupplierModelView[]>(_unitOfWork.SupplierRepository.GetSuppliers()), JsonRequestBehavior.AllowGet);
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


    }
}