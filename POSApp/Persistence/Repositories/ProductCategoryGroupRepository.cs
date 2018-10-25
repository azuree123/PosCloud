using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;

namespace POSApp.Persistence.Repositories
{
    public class ProductCategoryGroupRepository:IProductCategoryGroupRepository
    {
        private readonly PosDbContext _context;

        public ProductCategoryGroupRepository(PosDbContext context)
        {
            _context = context;
        }
        public ProductCategoryGroup GetProductCategoryGroup(int id)
        {
            return _context.ProductCategoryGroups.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroups(int storeId)
        {
            //return _context.ProductCategoryGroup;
           
            return _context.ProductCategoryGroups
                
                .Where(a => a.StoreId == storeId)
                .Select(a => new ProductCategoryGroupViewModel { Id = a.Id, Name = a.Name, StoreId = a.StoreId })
                .ToList();
        }

        public IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroupsFiltered(string query, int storeId)
        {
            //return _context.ProductCategoryGroup;
            query = query.ToUpper();
            return _context.ProductCategoryGroups
                    .Where(x => x.Name.ToUpper().Contains(query) && x.StoreId == storeId)
                    .Select(a => new ProductCategoryGroupViewModel { Id = a.Id, Name = a.Name, StoreId = a.StoreId })
                ;
        }
        public IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroupsFiltered(int query, int storeId)
        {
            return _context.ProductCategoryGroups
                    .Where(x => x.Id == query && x.StoreId == storeId)
                    .Select(a => new ProductCategoryGroupViewModel { Id = a.Id, Name = a.Name, StoreId = a.StoreId })

                ;
        }

        public int IsExisting(string productCategoryGroupName, int storeId)
        {
            var productCategoryGroup = _context.ProductCategoryGroups.Where(z => z.Name == productCategoryGroupName && z.StoreId == storeId);
            if (productCategoryGroup.Any())
            {
                return productCategoryGroup.FirstOrDefault().Id;
            }
            else
            {
                return 0;
            }

        }
        public void DeleteProductCategoryGroup(int id, int storeId)
        {
            var dept = _context.ProductCategoryGroups.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            _context.ProductCategoryGroups.Remove(dept);
        }
        public void AddProductCategoryGroup(ProductCategoryGroup optcategory)
        {
            _context.ProductCategoryGroups.Add(optcategory);

        }

       public void UpdateProductCategoryGroup(int id, int storeId, ProductCategoryGroup productCategoryGroup)
       {
            _context.ProductCategoryGroups.Attach(productCategoryGroup);
            _context.Entry(productCategoryGroup).State = EntityState.Modified;
        }


    }
}

