using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Dtos;
using POSApp.Core.Models;
using POSApp.Core.Shared;
using POSApp.Core.ViewModels;

namespace POSApp.Controllers
{
    [Authorize]
    public class PointOfSaleController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public PointOfSaleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: PointOfSale
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var products = new PosScreen();

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
            if (products.Customers.Where(a => a.Text == "Walk-in Customer").Any())
            {
            var temp=products.Customers.Where(a => a.Text == "Walk-in Customer").FirstOrDefault();
            temp.Selected = true;
            }
            else { }
            return View(products);
        }
        [HttpPost]
        public ActionResult Index(string spos_token,int customer_id,string hold_ref,string code
            ,int[] product_id,
            string[] item_comment,string[] product_code,string[] product_name,decimal[] real_unit_price,decimal[] product_discount,
            decimal[] item_was_ordered, decimal[] quantity,
            string spos_note,decimal amount,decimal balance_amount,string paid_by,
            string cc_no,string paying_gift_card_no,string cc_holder,string cheque_no,string cc_month,string cc_year, string cc_type,
            string cc_cvv2,string balance,string payment_note,int customer,string order_tax,decimal order_discount,string count,
            int did,int eid,int total_items,int total_quantity,string suspend, bool delete_id=true)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                int TransId = _unitOfWork.AppCountersRepository.GetId("Purchase");

                var savePo = new TransMaster();
                savePo.TransCode = "INV-" + "C-" + TransId.ToString() + "-" + Convert.ToInt32(user.StoreId);
                savePo.StoreId = Convert.ToInt32(user.StoreId);
                savePo.BusinessPartnerId = customer_id;
                savePo.TotalPrice = amount;
                savePo.Discount = order_discount;
                savePo.PaymentMethod = paid_by;
                savePo.SessionCode = 1;
                savePo.Type = "INV";
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
                        Waste = false
                    });
                }

                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);

                _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return View();
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
                    name = productVm.Name,
                    category_id = productVm.CategoryId.ToString(),
                    price = productVm.UnitPrice.ToString(),
                    image = ImageReturn(productVm.Image),
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
                        name = productVm.Name,
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
        public ActionResult PrintView()
        {
            return View();
        }

        public ActionResult CustomerDisplay()
        {
            return View();
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