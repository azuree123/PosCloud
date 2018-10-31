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

        public static void AddToTemptTransDetail(Product product,int qty,decimal cost,string userId)
        {
            TransDetailViewModel transDetail=new TransDetailViewModel
            {
                Discount = 0,
                StoreId = product.StoreId,
                ProductId = product.Id,
                Quantity = qty,
                UnitPrice = cost,
                CreatedByUserId = userId,
                ProductName = product.Name
            };
            temptTransDetail.Add(transDetail);
        }
        public static void RemoveFromTemptTransDetail(int  product,int storeId, string userId)
        {
            TransDetailViewModel transDetail = temptTransDetail
                .Where(a => a.ProductId == product && a.CreatedByUserId == userId && a.StoreId==storeId).ToList().FirstOrDefault();
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
    }
}