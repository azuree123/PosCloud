using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using POSApp.Core;
using POSApp.Core.Dtos;
using POSApp.Core.Models;
using POSApp.Core.Shared;
using POSApp.Core.ViewModels;
using POSApp.Services;

namespace POSApp.Controllers
{
    [Authorize]
    public class PointOfSaleController : LanguageController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public PointOfSaleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: PointOfSale
        public ActionResult Index(int hold=0,bool IsEmpty=false)
        {
            ViewBag.alert = TempData["alert"];
            var products = new PosScreen();
            products.Hold = hold;
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            products.DineTables = _unitOfWork.DineTableRepository.GetDineTables(Convert.ToInt32(user.StoreId)).Select(
                a => new SelectListItem
                {
                    Text = a.DineTableNumber,
                    Value = a.Id.ToString()
                }).ToList();
            if (!_unitOfWork.TillOperationRepository.CheckTillOpened(userid, Convert.ToInt32(user.StoreId)))
            {
                return RedirectToAction("OpenTill", "PointOfSale", new {returnUrl = @Request.Url.AbsoluteUri});
            }
            else
            {
                products.SessionCode = _unitOfWork.TillOperationRepository
                    .GetOpenedTill(userid, Convert.ToInt32(user.StoreId)).SessionCode;
            }
            products.PosCategories = Mapper.Map<List<PosCategory>>(
                _unitOfWork.ProductCategoryRepository.GetProductCategories(Convert.ToInt32(user.StoreId)));
            products.PosProducts = Mapper.Map<List<PosProducts>>(
                _unitOfWork.ProductRepository.GetSaleProducts(products.PosCategories.FirstOrDefault().CategoryId, Convert.ToInt32(user.StoreId)));
            products.Customers = _unitOfWork.BusinessPartnerRepository
                .GetBusinessPartners("C", Convert.ToInt32(user.StoreId)).Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }).ToList();
            products.PosHolds = _unitOfWork.TransMasterRepository.GetHoldTransactions(Convert.ToInt32(user.StoreId))
                .Select(a => new PosHold
                {
                    Id = a.Id,
                    Description = a.TransDate.ToString("R") +" ("+a.BusinessPartner.Name+") ",
                    Ref = a.TransRef
                }).ToList();
            switch (hold)
            {

                case 0:
                    if (products.Customers.Where(a => a.Text == "Walk-in Customer").Any())
                    {
                        var temp = products.Customers.Where(a => a.Text == "Walk-in Customer").FirstOrDefault();
                        temp.Selected = true;
                    }
                    else { }

                    break;
                default:
                    var holdTrans = _unitOfWork.TransMasterRepository.GetHoldTransaction(hold,
                        Convert.ToInt32(user.StoreId));
                    if (holdTrans.DineTableId!=null)
                    {
                        var selectedTable = products.DineTables.FirstOrDefault(a => a.Value == holdTrans.DineTableId.ToString());
                        selectedTable.Selected = true;
                    }
                    products.HoldRef = holdTrans.TransRef;
                    List<RootObject> objects = holdTrans.TransDetails.Select(a => new RootObject
                    {
                        id = a.Product.Id.ToString(),
                        item_id = a.Product.Id.ToString(),
                        label = a.Product.Name + " (" + a.Product.ProductCode + ")",
                        row =
                        new Row
                        {
                            id = a.Product.Id.ToString(),
                            code = a.Product.ProductCode,
                            name = a.Product.Name+" ("+a.Product.Size+")",
                            category_id = a.Product.CategoryId.ToString(),
                            price = a.UnitPrice.ToString(),
                            image = "/Pos/notfound_placeholder.svg",
                            tax = a.Tax.ToString(),
                            tax_method = "0",
                            quantity = "0",
                            barcode_symbology = a.Product.Barcode,
                            type = a.Product.Type,
                            alert_quantity = "0",
                            store_price = a.Product.CostPrice.ToString(),
                            qty = Convert.ToInt32(a.Quantity),
                            comment = "",
                            discount = a.Discount.ToString(),
                            real_unit_price = a.Product.UnitPrice.ToString(),
                            unit_price = a.UnitPrice.ToString()
                        },
                        combo_items = false
            }).ToList();
                    JavaScriptSerializer json = new JavaScriptSerializer();
                    string check = json.Serialize(objects.ToArray());
                    Response.Write("<script>localStorage.setItem('spos_customer'," + holdTrans.BusinessPartnerId+ ");" +
                                   "localStorage.setItem('spositems', '"+ check.ToString() + "');" +
                                   "localStorage.setItem('spos_tax', '0%');" +
                                   "localStorage.setItem('spos_discount', "+holdTrans.Discount+");</script>");

                    break;
            }

            if (IsEmpty)
            {
                
                Response.Write("<script>localStorage.removeItem('spositems');</script>");
            }
          

            return View(products);
        }
        [HttpPost]
        public ActionResult Index(string spos_token,int? table_id, int customer_id,string hold_ref,string code
            ,int[] product_id,
            string[] item_comment,string[] product_code,string[] product_name,decimal[] real_unit_price,decimal[] product_discount,
            decimal[] item_was_ordered, decimal[] quantity,
            string spos_note,string paid_by,
            string cc_no,string paying_gift_card_no,string cc_holder,string cheque_no,string cc_month,string cc_year, string cc_type,
            string cc_cvv2,string balance,string payment_note,int customer,string order_tax,decimal order_discount,string count,
            int did,int eid,int total_items,int total_quantity,string suspend, bool delete_id=true, decimal amount=0, decimal balance_amount=0)
        {
            try
            {
               
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
               
                _unitOfWork.TransMasterRepository.DeleteHold(did, Convert.ToInt32(user.StoreId));
                var savePo = new TransMaster();

                savePo.StoreId = Convert.ToInt32(user.StoreId);
                savePo.BusinessPartnerId = customer_id;
                savePo.TotalPrice = amount-balance_amount;
                savePo.Discount = order_discount;
                savePo.PaymentMethod = paid_by;
                savePo.Tax = Convert.ToDecimal(order_tax.Replace("%",String.Empty));
                savePo.Discount = order_discount;
                savePo.SessionCode = 1;
                savePo.Type = "INV";
                savePo.TransRef = hold_ref;
                savePo.DineTableId = table_id;
                savePo.SessionCode = eid;
                if (delete_id)
                {
                    if (balance_amount < 0 && _unitOfWork.BusinessPartnerRepository.IsWalkIn(customer_id, Convert.ToInt32(user.StoreId)))
                    {
                        TempData["alert"] = @"<div class='col-lg-12 alerts'>
                        <div class='alert alert-danger alert-dismissable'>
                        <button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>
                        <h4><i class='icon fa fa-ban'></i> Error</h4>
                        Please select customer for due payment. </div>
                        </div>";
                        if (did != 0)
                        {
                            return RedirectToAction("Index", "PointOfSale",new{hold=did});

                        }
                        return RedirectToAction("Index", "PointOfSale");
                    }
                    int TransId = _unitOfWork.AppCountersRepository.GetId("Invoice");
                    savePo.TransCode = "INV-" + "C-" + TransId.ToString() + "-" + Convert.ToInt32(user.StoreId);
                    savePo.TransStatus = "Complete";
                }
                else
                {
                    int TransId = _unitOfWork.AppCountersRepository.GetId("HoldInvoice");
                    savePo.TransCode = "HLDINV-" + "C-" + TransId.ToString() + "-" + Convert.ToInt32(user.StoreId);
                    savePo.TransStatus = "Hold";
                    
                }

                savePo.TransDate = DateTime.Now;
                savePo.TransMasterPaymentMethods.Add(new TransMasterPaymentMethod
                {
                    Amount = Convert.ToDouble(amount),
                    StoreId = Convert.ToInt32(user.StoreId),
                    Method = paid_by

                });
                for (int i = 0; i < product_id.Length; i++)
                {
                    savePo.TransDetails.Add(new TransDetail
                    {
                        Discount = product_discount[i],
                        ProductCode = product_code[i],
                        UnitPrice = real_unit_price[i],
                        StoreId = Convert.ToInt32(user.StoreId),
                        Quantity = quantity[i],
                        Tax = 0,
                        Balance = 0,
                        Waste = false,
                    });
                }

                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);

                _unitOfWork.Complete();
                if (delete_id)
                {
                    return RedirectToAction("PrintView", "PointOfSale", new { id = savePo.Id });
                }
                else
                {
                    TempData["alert"] = @"<div class='col-lg-12 alerts'>
                        <div class='alert alert-success alert-dismissable'>
                        <button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>
                        <h4><i class='icon fa fa-check'></i> Success</h4>
                        Invoice added to hold. </div>
                        </div>";
                    return RedirectToAction("Index", "PointOfSale",new{ IsEmpty = true});
                }
            }

            catch (Exception e)
            {
                return RedirectToAction("Index", "PointOfSale");
            }
           
          
        }

        public ActionResult GetProducts(int id = 0, string group = "")
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var products=new List<PosProducts>();
            if (id == 0 && string.IsNullOrEmpty(group))
            {
                products = Mapper.Map<List<PosProducts>>(
                    _unitOfWork.ProductRepository.GetAllProducts(Convert.ToInt32(user.StoreId)).Where(a=>!a.PurchaseItem && !a.InventoryItem).Take(5));
            }
            else
            {
                if (string.IsNullOrEmpty(group)) { } else { }
                if (id == 0) { }
                else
                {

                }
            }
            return View(products);
        }
        public ActionResult ajaxproducts(int category_id, int tcp)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var products = new List<PosProducts>();

            products = Mapper.Map<List<PosProducts>>(
                _unitOfWork.ProductRepository.GetSaleProducts(category_id, Convert.ToInt32(user.StoreId)));

            return View("GetProducts", products);
        }

        public ActionResult GetCategories()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var products = new List<PosCategory>();

            products = Mapper.Map<List<PosCategory>>(
                _unitOfWork.ProductCategoryRepository.GetProductCategories(Convert.ToInt32(user.StoreId)));

            return View(products);
        }

        public JsonResult get_product(string code)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductSyncViewModel productVm = Mapper.Map<ProductSyncViewModel>(_unitOfWork.ProductRepository.GetProductByCode(code, Convert.ToInt32(user.StoreId)));
                RootObject root=new RootObject();
                root.id = productVm.Id.ToString();
                root.item_id = productVm.Id.ToString();
                root.label = productVm.Name + " (" + productVm.ProductCode + ")";
                root.row=new Row
                {
                    id = productVm.Id.ToString(),
                    code = productVm.ProductCode,
                    name = productVm.Name + " (" + productVm.Size + ")",
                    category_id = productVm.CategoryId.ToString(),
                    price = productVm.UnitPrice.ToString(),
                   
                    tax = productVm.TaxId==null?"0":_unitOfWork.TaxRepository.GetTaxById((int)productVm.TaxId,(int)productVm.StoreId).Rate.ToString(),
                    tax_method = "0",
                    quantity = "0",
                    barcode_symbology = productVm.Barcode,
                    type = productVm.Type,
                    alert_quantity = "0",
                    store_price = productVm.CostPrice.ToString(),
                    qty = 1,
                    comment = "",
                    discount = "0",
                    real_unit_price = productVm.UnitPrice.ToString(),
                    unit_price = productVm.UnitPrice.ToString()
                };
                root.combo_items = false;
                return Json(root, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private string ImageReturn(string image)
        {
            if (string.IsNullOrEmpty(image))
            {
                return "/Pos/notfound_placeholder.svg";
            }
            else
            {
                return string.Format("data:image/jpg;base64,{0}", image);
            }
        }
        public JsonResult suggestions(string term)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                List<ProductSyncViewModel> products = Mapper.Map<List<ProductSyncViewModel>>(_unitOfWork.ProductRepository.GetSaleProductsQuery(Convert.ToInt32(user.StoreId),term));
                if (products.Count > 0)
                {
                    List<RootObject> objects = new List<RootObject>();
                    foreach (var productVm in products)
                {
                        RootObject root = new RootObject();
                    root.id = productVm.Id.ToString();
                    root.item_id = productVm.Id.ToString();
                    root.label = productVm.Name + " (" + productVm.ProductCode + ")";
                    root.row = new Row
                    {
                        id = productVm.Id.ToString(),
                        code = productVm.ProductCode,
                        name = productVm.Name + " (" + productVm.Size + ")",
                        category_id = productVm.CategoryId.ToString(),
                        price = productVm.UnitPrice.ToString(),
                        image = "/Pos/notfound_placeholder.svg",
                        tax = productVm.TaxId == null ? "0" : _unitOfWork.TaxRepository.GetTaxById((int)productVm.TaxId, (int)productVm.StoreId).Rate.ToString(),
                        tax_method = "0",
                        quantity = "0",
                        barcode_symbology = productVm.Barcode,
                        type = productVm.Type,
                        alert_quantity = "0",
                        store_price = productVm.CostPrice.ToString(),
                        qty = 1,
                        comment = "",
                        discount = "0",
                        real_unit_price = productVm.UnitPrice.ToString(),
                        unit_price = productVm.UnitPrice.ToString()
                    };
                    root.combo_items = false;
                    objects.Add(root);

                }
                    return Json(objects, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    List<NoRootObject> objects = new List<NoRootObject>();
                        objects.Add(new NoRootObject
                        {
                            id = 0,
                            label = "No match found",
                            value = term
                        });
                return Json(objects, "application/json",
                    Encoding.UTF8,
                    JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
        [HttpPost]
        public ActionResult AddCustomer(string name,string email,string phone)
        {

            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                BusinessPartner customer = new BusinessPartner();
                customer.Name = name;
                customer.Email = email;
                customer.PhoneNumber = phone;
                customer.StoreId = (int)user.StoreId;
                customer.Type = "C";
                customer.Birthday = DateTime.Now;
                _unitOfWork.BusinessPartnerRepository.AddBusinessPartner(customer);
                _unitOfWork.Complete();


                return Json(new PosCustomer { Msg = "Error!", Status = "success", Text = customer.Name, Value = customer.Id.ToString() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new PosCustomer { Msg = "Error!", Status = "error", Text = "", Value = "" }, JsonRequestBehavior.AllowGet);

            }


        }
        public ActionResult PrintView(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.TransMasterRepository.GetSaleTransMaster(id,Convert.ToInt32(user.StoreId)));
        }

        public ActionResult CustomerDisplay()
        {
            return View();
        }

        public ActionResult OpenTill(string returnUrl = "")
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!_unitOfWork.TillOperationRepository.CheckTillOpened(userid, Convert.ToInt32(user.StoreId)))
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            else
            {
                return RedirectToAction("Index","PointOfSale");
            }
        }
        [HttpPost]
        public ActionResult OpenTill(decimal cash_in_hand, string returnUrl = "")
        {
            var userid = User.Identity.GetUserId();
            var users = UserManager.FindById(userid);
            var user = _unitOfWork.UserRepository.GetUserById(userid,users.StoreId);
            if (!_unitOfWork.TillOperationRepository.CheckTillOpened(userid, Convert.ToInt32(user.StoreId)))
            {
                TillOperation data = new TillOperation
                {
                    StoreId = Convert.ToInt32(user.StoreId),
                    ApplicationUserId = userid,
                    OpeningAmount = cash_in_hand,
                    SystemAmount = 0,
                    PhysicalAmount = 0,
                    OperationDate = DateTime.Now,
                    Remarks = "",
                    Status = false,
                    TillOperationType = "Open",
                    SessionCode =
                        _unitOfWork.TillOperationRepository.GetTillSessionCode(userid, Convert.ToInt32(user.StoreId)),
                    ShiftId = _unitOfWork.EmployeeRepository.GetEmployeeById(user.EmployeeId, (int)user.StoreId).ShiftId

                };
                _unitOfWork.TillOperationRepository.AddTillOperation(data);
                _unitOfWork.Complete();
            }

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(returnUrl);
        }

        public ActionResult CloseTill()
        {
          
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return View(_unitOfWork.TillOperationRepository.GetOpenedTill(userid, Convert.ToInt32(user.StoreId)));
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        [HttpPost]
        public ActionResult CloseTill(TillOperation operation)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            operation.TillOperationType = "Close";
            operation.SystemAmount = _unitOfWork.TransMasterRepository.GetSaleInvoicesTotalBySessionCode(userid,operation.SessionCode,operation.StoreId,operation.OperationDate,DateTime.Now);
            _unitOfWork.TillOperationRepository.UpdateTillOperations(operation.Id,operation.StoreId,operation);
            _unitOfWork.Complete();
            return RedirectToAction("LogOut", "Account");
        }

        public ActionResult OpenedBills()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return View(_unitOfWork.TransMasterRepository.GetHoldTransactions(Convert.ToInt32(user.StoreId)).ToList());
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult DeleteBill(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteHold(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("Hold order deleted successfully", AlertType.Success);
                return RedirectToAction("OpenedBills","PointOfSale");
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

            return RedirectToAction("OpenedBills","PointOfSale");

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