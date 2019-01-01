using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;
using EntityState = System.Data.Entity.EntityState;

namespace POSApp.Persistence.Repositories
{
    class TransDetailRepository : ITransDetailRepository
    {
        private readonly PosDbContext _context;

        public TransDetailRepository(PosDbContext context)
        {
            _context = context;
        }
        public TransDetail GetTransDetail(int id, int storeId)
        {
            return _context.TransDetails.FirstOrDefault(x => x.Id == id && x.StoreId == storeId && !x.IsDisabled);
        }
        public IEnumerable<TransDetailViewModel> GetTransDetails(int orderid, int storeId)
        {
            List<TransDetail> details = _context.TransDetails.Include(a => a.Product).Include(a=>a.ModifierTransDetail)
                .Where(a => a.TransMasterId == orderid && a.StoreId == storeId).ToList();

            foreach (var transDetail in details)
            {
                transDetail.Product =
                    _context.Products.FirstOrDefault(a => a.ProductCode == transDetail.ProductCode && a.StoreId == storeId);
                foreach (var modifierTransDetail in transDetail.ModifierTransDetail)
                {
                    modifierTransDetail.ModifierOption = _context.ModifierOptions.FirstOrDefault(a =>
                        a.Id == modifierTransDetail.ModifierOptionId && a.StoreId == modifierTransDetail.StoreId);
                }
               
            }
            return Mapper.Map<TransDetailViewModel[]>(details);
        }
        public IEnumerable<TransDetailViewModel> GetTransDetailsFiltered(int orderid, string query, int storeId)
        {
            //return _context.PurchaseOrderDetail;
            query = query.ToUpper();
            return _context.TransDetails
                //.Where(x => x.Name.ToUpper().Contains(query))
                .Select(p => new TransDetailViewModel { Id = p.Id });
        }
        public IEnumerable<TransDetailViewModel> GetTransDetailsFiltered(int orderid, int query, int storeId)
        {
            return _context.TransDetails
                .Where(x => x.Id == query && x.StoreId == storeId)
                .Select(p => new TransDetailViewModel { Id = p.Id });
        }

        //public int IsExisting(string purchaseOrderDetailName, int storeId)
        //{
        //    var purchaseOrderDetail = _context.TransDetails.Where(z => z.Name == purchaseOrderDetailName && z.StoreId == storeId);
        //    if (purchaseOrderDetail.Any())
        //    {
        //        return purchaseOrderDetail.FirstOrDefault().Id;
        //    }
        //    else
        //    {
        //        return 0;
        //    }

        //}
        public void DeleteTransDetail(int id, int storeId)
        {
            var transDetails = _context.TransDetails.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            transDetails.IsDisabled = true;
            _context.TransDetails.Attach(transDetails);
            _context.Entry(transDetails).State = EntityState.Modified;
        }
        public void AddTransDetail(TransDetail transdetail)
        {
            _context.TransDetails.Add(transdetail);

        }
    }
}