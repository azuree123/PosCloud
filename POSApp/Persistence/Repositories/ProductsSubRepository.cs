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

        public  ProductsSub GetProductsSubById(int id, int comboProductId, int storeid)
        {
            return _context. ProductsSubs.Find(id,comboProductId ,storeid);
        }

        public void AddProductsSub( ProductsSub  productsSubs)
        {
            if (!_context. ProductsSubs.Where(a => a.ProductId == productsSubs.ProductId && a.ComboProductId == productsSubs.ComboProductId && a.StoreId == productsSubs.StoreId).Any())
            {
                _context. ProductsSubs.Add(productsSubs);
            }

        }

        public void UpdateProductsSub(int id, int comboProductId,  ProductsSub  productsSub, int storeid)
        {
            if ( productsSub.ProductId != id)
            {
                 productsSub.ProductId = id;
            }
            else { }
            if (productsSub.ComboProductId != comboProductId)
            {
                productsSub.ComboProductId = comboProductId;
            }
            else { }

            productsSub.StoreId = storeid;
            _context. ProductsSubs.Attach( productsSub);
            _context.Entry( productsSub).State = EntityState.Modified;
        }

        public void DeleteProductsSub(int id,int comboProductId, int storeid)
        {
            var  productsSub = new  ProductsSub { ProductId = id,ComboProductId = comboProductId, StoreId = storeid };
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