using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Shared
{
    public class ProductHelper
    {
        public static List<ProductHelperViewModel> temptProduct;


        public static void AddToTemptProduct(string size, string barcode,decimal price,string userId,int storeId)
        {
            ProductHelperViewModel checkProduct = temptProduct
                .Where(a => a.Size == size && a.Barcode == barcode && a.StoreId==storeId  && a.UserId == userId && a.Price == price).ToList()
                .FirstOrDefault();
            if (checkProduct != null)
            {
                checkProduct.Price += price;
            }
            else
           
            {

                ProductHelperViewModel product = new ProductHelperViewModel
                {
                    Size = size,
                    Barcode = barcode,
                    Price = price,
                    UserId = userId,
                    StoreId = storeId
                   
                };

                temptProduct.Add(product);
            }

        }
        public static void RemoveFromTemptProduct(string size, int storeId, string userId)
        {
            ProductHelperViewModel productDetail = temptProduct
                .Where(a => a.Size == size && a.UserId == userId && a.StoreId == storeId).ToList()
                .FirstOrDefault();
            temptProduct.Remove(productDetail);
        }
        public static void EmptyTemptProduct(string userId, int storeId)
        {
            List<ProductHelperViewModel> productDetail = temptProduct
                .Where(a => a.UserId == userId && a.StoreId == storeId).ToList();
            foreach (var productHelperViewModel in productDetail.ToList())
            {
                temptProduct.Remove(productHelperViewModel);
            }
        }
    }
}