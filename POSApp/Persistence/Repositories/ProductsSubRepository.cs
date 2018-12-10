using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class ProductsSubRepository : IProductsSubRepository
    {
        private PosDbContext _context;

        public  ProductsSubRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable< ProductsSub> GetProductsSubs(int storeid)
        {
            return _context. ProductsSubs.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }
        public IEnumerable<ProductsSub> GetProductsSubs(string productCode,int storeid)
        {
            return _context.ProductsSubs.Where(a => a.StoreId == storeid && !a.IsDisabled &&a.ComboProductCode==productCode).ToList();
        }

        public  ProductsSub GetProductsSubById(string id, string comboProductId, int storeid)
        {
            return _context. ProductsSubs.Find(id,comboProductId ,storeid);
        }

        public void AddProductsSub( ProductsSub  productsSubs)
        {
            var inDb = _context.ProductsSubs.FirstOrDefault(a =>
                a.ProductCode == productsSubs.ProductCode && a.ComboProductCode == productsSubs.ComboProductCode &&
                a.StoreId == productsSubs.StoreId);
            if (inDb == null)
            {
                _context.ProductsSubs.Add(productsSubs);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    productsSubs.ComboProductCode = inDb.ComboProductCode;
                    _context.Entry(inDb).CurrentValues.SetValues(productsSubs);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }

        public void UpdateProductsSub(string id, string comboProductId,  ProductsSub  productsSub, int storeid)
        {
            if ( productsSub.ProductCode != id)
            {
                 productsSub.ProductCode = id;
            }
            else { }
            if (productsSub.ComboProductCode != comboProductId)
            {
                productsSub.ComboProductCode = comboProductId;
            }
            else { }

            productsSub.StoreId = storeid;
            _context. ProductsSubs.Attach( productsSub);
            _context.Entry( productsSub).State = EntityState.Modified;
        }

        public void DeleteProductsSub(string id, string comboProductId, int storeid)
        {
            
            var productSub = _context.ProductsSubs.FirstOrDefault(a => a.ProductCode == id && a.ComboProductCode == comboProductId  && a.StoreId == storeid);
            productSub.IsDisabled = true;
            _context.ProductsSubs.Attach(productSub);
            _context.Entry(productSub).State = EntityState.Modified;
        }
        public IEnumerable< ProductsSub> GetApiProductsSubs()
        {
            IEnumerable< ProductsSub>  productsSubs = _context. ProductsSubs.Where(a => !a.Synced).ToList();
            foreach (var  productsSub in  productsSubs)
            {
                 productsSub.Synced = true;
                 productsSub.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return  productsSubs;
        }
    }
}