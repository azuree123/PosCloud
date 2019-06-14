using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Shared
{
    public class PoHelper
    {
        public static List<TransDetailViewModel> temptTransDetail;
       

        public static void AddToTemptTransDetail(Product product,decimal qty,decimal cost , string userId)
        {
            TransDetailViewModel checkTrans = temptTransDetail
                .Where(a => a.ProductCode == product.ProductCode && a.UnitPrice == cost && a.CreatedByUserId == userId).ToList()
                .FirstOrDefault();
            if (checkTrans != null)
            { 
                checkTrans.Quantity+=qty;
            }  
            else
            {

            TransDetailViewModel transDetail=new TransDetailViewModel
            {
                Discount = 0,
                StoreId = product.StoreId,
                ProductCode = product.ProductCode,
                UnitName = product.PurchaseUnit,
                Quantity = qty,
                UnitPrice = cost,
                CreatedByUserId = userId,
                ProductName = product.Name,
                


            };
            
            temptTransDetail.Add(transDetail);
            }

        }
        public static void RemoveFromTemptTransDetail(string  product,int storeId, string userId)
        {
            TransDetailViewModel transDetail = temptTransDetail
                .Where(a => a.ProductCode == product && a.CreatedByUserId == userId && a.StoreId==storeId).ToList().FirstOrDefault();
            temptTransDetail.Remove(transDetail);
        }
        public static void EmptyTemptTransDetail(string userId, int storeId)
        {
            List<TransDetailViewModel> transDetail = temptTransDetail
                .Where(a=> a.CreatedByUserId == userId && a.StoreId==storeId).ToList().ToList();
            foreach (var transDetailViewModel in transDetail)
            {
            temptTransDetail.Remove(transDetailViewModel);
            }
        }

        //public static List<ProductSubViewModel> TempComboOptions;
        //public static void AddToTempComboOptions(ProductSubViewModel ComboOptionViewModel, string userId)
        //{

        //    ProductSubViewModel checkTrans = TempComboOptions
        //        .Where(a => a.ProductCode == ComboOptionViewModel.ProductCode && a.CreatedBy == userId).ToList()
        //        .FirstOrDefault();
        //    ComboOptionViewModel.CreatedBy = userId;
        //    if (checkTrans != null)
        //    {
        //        TempComboOptions.Remove(checkTrans);
        //        TempComboOptions.Add(ComboOptionViewModel);
        //    }
        //    else
        //    {
        //        ProductSubViewModel transDetail = ComboOptionViewModel;
        //        TempComboOptions.Add(transDetail);
        //    }

        //}
        //public static void RemoveFromTempComboOptions(string product, int storeId, string userId)
        //{
        //    ProductSubViewModel transDetail = TempComboOptions
        //        .Where(a => a.ProductCode == product && a.CreatedBy == userId && a.StoreId == storeId).ToList().FirstOrDefault();
        //    TempComboOptions.Remove(transDetail);
        //}
        //public static void EmptyTempComboOptions(string userId, int storeId)
        //{
        //    List<ProductSubViewModel> transDetail = TempComboOptions
        //        .Where(a => a.CreatedBy == userId && a.StoreId == storeId).ToList().ToList();
        //    foreach (var transDetailViewModel in transDetail)
        //    {
        //        TempComboOptions.Remove(transDetailViewModel);
        //    }
        //}
    }
}