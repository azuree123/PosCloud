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
    public class ProductsController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        public ProductsController()
        {
            
        }
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Products
        public ActionResult ProductsList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a=>a.Type=="Product").ToList());
        }

        public ActionResult AddProduct()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCreateViewModel product = new ProductCreateViewModel();
            product.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a=>a.Type=="Product")
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            product.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers()
                .Select(a => new SelectListItem {Value = a.Id.ToString(), Text = a.Name}).AsEnumerable();
            product.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            product.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            ViewBag.edit = "AddProduct";
           
            return View(product);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductCreateViewModel productVm, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddProduct";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!ModelState.IsValid)
            {
                productVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type == "Product")
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                productVm.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers()
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                productVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                productVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                return View(productVm);
            }
            else 
            if (file !=null && file.ContentLength > 0)
            {
                
                try
                {
                    //string path = Server.MapPath("~/Images/Data/Product/" + file.FileName);
                    //if (System.IO.File.Exists(path))
                    //{
                    //    ViewBag.Message = "Image Already Exists!";
                    //}
                    //else
                    //{
                    //    file.SaveAs(path);
                    //}
                    productVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                    file.InputStream.Read(productVm.Image, 0, file.ContentLength);
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }
                
            }
            {
                
                productVm.StoreId = user.StoreId;
                Product product = Mapper.Map<Product>(productVm);
                product.Type = "Product";
                _unitOfWork.ProductRepository.AddProduct(product);
                _unitOfWork.Complete();
                return RedirectToAction("ProductsList", "Products");
            }
            
        }
        public ActionResult UpdateProduct(int id)
        {
            ViewBag.edit = "UpdateProduct";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCreateViewModel productVm = Mapper.Map<ProductCreateViewModel>(_unitOfWork.ProductRepository.GetProductById(id,Convert.ToInt32(user.StoreId)));
            productVm.CategoryDdl = _unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId).Where(a => a.Type == "Product")
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.SupplierDdl = _unitOfWork.SupplierRepository.GetSuppliers()
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            productVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddProduct", productVm);
        }
        [HttpPost]
        public ActionResult UpdateProduct(int id, ProductCreateViewModel productVm,HttpPostedFileBase file)
        {
            ViewBag.edit = "UpdateProduct";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!ModelState.IsValid)
            {
                productVm.UnitDdl = _unitOfWork.UnitRepository.GetUnit((int)user.StoreId)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                productVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                return View("AddProduct", productVm);
            }
            else if (file != null && file.ContentLength > 0)
            {
                try
                {
                    productVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                    file.InputStream.Read(productVm.Image, 0, file.ContentLength);

                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }
            }

            {
                Product product = Mapper.Map<Product>(productVm);
                product.Type = "Product";
                _unitOfWork.ProductRepository.UpdateProduct(id,Convert.ToInt32(user.StoreId),product);
                _unitOfWork.Complete();
                return RedirectToAction("ProductsList", "Products");
            }

        }
        public ActionResult DeleteProduct(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ProductRepository.DeleteProduct(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ProductsList", "Products");
        }

        public ActionResult ProductCategoryList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId));
        }
        public ActionResult AddProductCategoryPartial()
        {
            ViewBag.edit = "AddProductCategoryPartial";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            });
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategoryPartial(ProductCategoryViewModel productcategoryvm)
        {
            ViewBag.edit = "AddProductCategoryPartial";
            if (!ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Name
                });
                return View(productcategoryvm);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                productcategoryvm.StoreId = user.StoreId;
                ProductCategory productcategory = Mapper.Map<ProductCategory>(productcategoryvm);
                productcategory.Type = "Product";
                _unitOfWork.ProductCategoryRepository.AddProductCategory(productcategory);
                _unitOfWork.Complete();
                return PartialView("Error");
            }

        }

        public ActionResult AddProductCategory()
        {
            ViewBag.edit = "AddProductCategory";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int) user.StoreId).Select(a=> new SelectListItem
            {
                Text = a.Name,Value = a.Name
            });
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategory(ProductCategoryViewModel productCategory, HttpPostedFileBase file)
        {
            ViewBag.edit = "AddProductCategory";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!ModelState.IsValid)
            {
                ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Name
                });
                return View(productCategory);
            }
            else
            if (file != null && file.ContentLength > 0)
            {

                try
                {
                    productCategory.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                    file.InputStream.Read(productCategory.Image, 0, file.ContentLength);
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }

            }
            {
                
                productCategory.StoreId = user.StoreId;
                productCategory.Type = "Product";
                ProductCategory category = Mapper.Map<ProductCategory>(productCategory);
                _unitOfWork.ProductCategoryRepository.AddProductCategory(category);
                _unitOfWork.Complete();
                return RedirectToAction("ProductCategoryList");
            }
        }
        public ActionResult UpdateProductCategory(int id)
        {
            ViewBag.edit = "UpdateProductCategory";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCategoryViewModel product =
                Mapper.Map<ProductCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id,Convert.ToInt32(user.StoreId)));
       
            ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Name
            });
            return View("AddProductCategory", product);
        }
        [HttpPost]
        public ActionResult UpdateProductCategory(int id, ProductCategoryViewModel productCategoryVm, HttpPostedFileBase file)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.edit = "UpdateProductCategory";
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
      
                ViewBag.ddl = _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId).Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Name
                });
                ProductCategoryViewModel product =
                    Mapper.Map<ProductCategoryViewModel>(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id,Convert.ToInt32(user.StoreId)));
                return View("AddProductCategory", product);
            }
            else
            if (file != null && file.ContentLength > 0)
            {

                try
                {
                    productCategoryVm.Image = new byte[file.ContentLength]; // file1 to store image in binary formate  
                    file.InputStream.Read(productCategoryVm.Image, 0, file.ContentLength);
                }
                catch (Exception e)
                {
                    ViewBag.Message = "ERROR:" + e.Message.ToString();
                }

            }
            {
                productCategoryVm.Type = "Product";
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductCategory category = Mapper.Map<ProductCategory>(productCategoryVm);
                _unitOfWork.ProductCategoryRepository.UpdateProductCategory(id,Convert.ToInt32(user.StoreId),category);
                _unitOfWork.Complete();
                return RedirectToAction("ProductCategoryList");
            }
        }

        public ActionResult DeleteProductCategory(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ProductCategoryRepository.DeleteProductCategory(id,Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ProductCategoryList", "Products");
        }

        public JsonResult GetProductCategoryDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<ProductCategoryViewModel[]>(_unitOfWork.ProductCategoryRepository.GetProductCategories((int)user.StoreId)).Where(a => a.Type == "Product"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult ProductCategoryGroupList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId));
        }
        public ActionResult AddProductCategoryGroupPartial()
        {
            ViewBag.edit = "AddProductCategoryGroupPartial";
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategoryGroupPartial(ProductCategoryGroupViewModel ProductCategoryGroupvm)
        {
            ViewBag.edit = "AddProductCategoryGroupPartial";
            if (!ModelState.IsValid)
            {
                return View(ProductCategoryGroupvm);
            }
            else
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductCategoryGroupvm.StoreId = (int)user.StoreId;
                ProductCategoryGroup ProductCategoryGroup = Mapper.Map<ProductCategoryGroup>(ProductCategoryGroupvm);
               
                _unitOfWork.ProductCategoryGroupRepository.AddProductCategoryGroup(ProductCategoryGroup);
                _unitOfWork.Complete();
                return PartialView("Error");
            }

        }

        public ActionResult AddProductCategoryGroup()
        {
            ViewBag.edit = "AddProductCategoryGroup";
            return View();
        }
        [HttpPost]
        public ActionResult AddProductCategoryGroup(ProductCategoryGroupViewModel productCategoryGroup)
        {
            ViewBag.edit = "AddProductCategoryGroup";
            if (!ModelState.IsValid)
            {
                return View(productCategoryGroup);
            }
            else
           
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                productCategoryGroup.StoreId =(int) user.StoreId;
               
                ProductCategoryGroup category = Mapper.Map<ProductCategoryGroup>(productCategoryGroup);
                _unitOfWork.ProductCategoryGroupRepository.AddProductCategoryGroup(category);
                _unitOfWork.Complete();
                return RedirectToAction("ProductCategoryGroupList");
            }
        }
        public ActionResult UpdateProductCategoryGroup(int id)
        {
            ViewBag.edit = "UpdateProductCategoryGroup";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ProductCategoryGroupViewModel product =
                Mapper.Map<ProductCategoryGroupViewModel>(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroup(id,(int)user.StoreId));
            return View("AddProductCategoryGroup", product);
        }
        [HttpPost]
        public ActionResult UpdateProductCategoryGroup(int id, ProductCategoryGroupViewModel productCategoryGroupVm)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.edit = "UpdateProductCategoryGroup";
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductCategoryGroupViewModel product =
                    Mapper.Map<ProductCategoryGroupViewModel>(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroup(id, (int)user.StoreId));
                return View("AddProductCategoryGroup", product);
            }
            else
            {
                
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductCategoryGroup category = Mapper.Map<ProductCategoryGroup>(productCategoryGroupVm);
                _unitOfWork.ProductCategoryGroupRepository.UpdateProductCategoryGroup(id, Convert.ToInt32(user.StoreId), category);
                _unitOfWork.Complete();
                return RedirectToAction("ProductCategoryGroupList");
            }
        }

        public ActionResult DeleteProductCategoryGroup(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ProductCategoryGroupRepository.DeleteProductCategoryGroup(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ProductCategoryGroupList", "Products");
        }

        //ModifierOption
        public ActionResult ModifierOptionList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ModifierOptionRepository.GetModifierOptions((int)user.StoreId).ToList());
        }

        public ActionResult AddModifierOption()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ModifierOptionViewModel modifieroption = new ModifierOptionViewModel();
            modifieroption.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            ViewBag.edit = "AddModifierOption";

            return View(modifieroption);
        }
        [HttpPost]
        public ActionResult AddModifierOption(ModifierOptionViewModel modifieroptionVm)
        {
            ViewBag.edit = "AddModifierOption";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!ModelState.IsValid)
            {
               
                modifieroptionVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                return View(modifieroptionVm);
            }
            else
            
            {

                modifieroptionVm.StoreId = user.StoreId;
                ModifierOption modifieroption = Mapper.Map<ModifierOption>(modifieroptionVm);
                
                _unitOfWork.ModifierOptionRepository.AddModifierOption(modifieroption);
                _unitOfWork.Complete();
                return RedirectToAction("ModifierOptionList", "Products");
            }

        }
        public ActionResult UpdateModifierOption(int id)
        {
            ViewBag.edit = "UpdateModifierOption";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ModifierOptionViewModel modifieroptionVm = Mapper.Map<ModifierOptionViewModel>(_unitOfWork.ModifierOptionRepository.GetModifierOptionsById(id, Convert.ToInt32(user.StoreId)));
           
            modifieroptionVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
            return View("AddModifierOption", modifieroptionVm);
        }
        [HttpPost]
        public ActionResult UpdateModifierOption(int id, ModifierOptionViewModel modifieroptionVm)
        {
            ViewBag.edit = "UpdateModifierOption";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            if (!ModelState.IsValid)
            {
               
                modifieroptionVm.TaxDdl = _unitOfWork.TaxRepository.GetTaxes((int)user.StoreId)
                    .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).AsEnumerable();
                return View("AddModifierOption", modifieroptionVm);
            }
            else 
            

            {
                ModifierOption modifieroption = Mapper.Map<ModifierOption>(modifieroptionVm);
                
                _unitOfWork.ModifierOptionRepository.UpdateModifierOptions(id, Convert.ToInt32(user.StoreId), modifieroption);
                _unitOfWork.Complete();
                return RedirectToAction("ModifierOptionList", "Products");
            }

        }
        public ActionResult DeleteModifierOption(int id, int storeid)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ModifierOptionRepository.DeleteModifierOptions(id, Convert.ToInt32(user.StoreId));
            _unitOfWork.Complete();
            return RedirectToAction("ModifierOptionList", "Products");
        }
        public JsonResult GetProductCategoryGroupDdl()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                return Json(Mapper.Map<ProductCategoryGroupViewModel[]>(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups((int)user.StoreId)), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public ActionResult ModifierList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ModifierRepository.GetModifiers((int)user.StoreId));
        }
        [HttpGet]
        public ActionResult AddModifier()
        {
            ViewBag.edit = "AddModifier";
            return View();
        }
        [HttpPost]
        public ActionResult AddModifier(ModifierViewModel modifierVm)
        {
            ViewBag.edit = "AddModifier";
            if (!ModelState.IsValid)
            {
                return View(modifierVm);
            }
            else
            {
                Modifier modifier = Mapper.Map<Modifier>(modifierVm);
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                modifier.StoreId = (int)user.StoreId;
                _unitOfWork.ModifierRepository.AddModifier(modifier);
                _unitOfWork.Complete();
                return RedirectToAction("ModifierList", "Products");
            }

        }
        [HttpGet]
        public ActionResult UpdateModifier(int id)
        {
            ViewBag.edit = "UpdateModifier";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            ModifierViewModel modifierVm =
                Mapper.Map<ModifierViewModel>(_unitOfWork.ModifierRepository.GetModifierById(id, (int)user.StoreId));
            return View("AddModifier", modifierVm);
        }
        [HttpPost]
        public ActionResult UpdateModifier(int id, ModifierViewModel modifierVm)
        {
            ViewBag.edit = "UpdateModifier";
            if (!ModelState.IsValid)
            {
                return View("AddModifier", modifierVm);
            }
            else
            {
                Modifier modifier = Mapper.Map<Modifier>(modifierVm);
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                modifier.StoreId = (int)user.StoreId;
                _unitOfWork.ModifierRepository.UpdateModifier(id, modifier.StoreId, modifier);
                _unitOfWork.Complete();
                return RedirectToAction("ModifierList", "Products");
            }
        }
        public ActionResult DeleteModifier(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            _unitOfWork.ModifierRepository.DeleteModifier(id, (int)user.StoreId);
            _unitOfWork.Complete();
            return RedirectToAction("ModifierList", "Products");
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