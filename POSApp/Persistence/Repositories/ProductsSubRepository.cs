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
            return _context. ProductsSubs.Where(a => a.StoreId == storeid).ToList();
        }

        public  ProductsSub GetProductsSubById(string id, string comboProductId, int storeid)
        {
            return _context. ProductsSubs.Find(id,comboProductId ,storeid);
        }

        public void AddProductsSub( ProductsSub  productsSubs)
        {
            if (!_context. ProductsSubs.Where(a => a.ProductCode == productsSubs.ProductCode && a.ComboProductCode == productsSubs.ComboProductCode && a.StoreId == productsSubs.StoreId).Any())
            {
                _context. ProductsSubs.Add(productsSubs);
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
            var  productsSub = new  ProductsSub { ProductCode = id,ComboProductCode = comboProductId, StoreId = storeid };
            _context. ProductsSubs.Attach( productsSub);
            _context.Entry( productsSub).State = EntityState.Deleted;
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