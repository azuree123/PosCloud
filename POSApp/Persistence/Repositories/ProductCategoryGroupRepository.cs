using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public ProductCategoryGroup GetProductCategoryGroup(int id,int storeId)
        {
            return _context.ProductCategoryGroups.FirstOrDefault(x => x.Id == id && x.StoreId==storeId && !x.IsDisabled);
        }
        public async Task<ProductCategoryGroup> GetProductCategoryGroupAsync(int id, int storeId)
        {
            return await _context.ProductCategoryGroups.FirstOrDefaultAsync(x => x.Id == id && x.StoreId == storeId && !x.IsDisabled);
        }
        public IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroups(int storeId)
        {
            //return _context.ProductCategoryGroup;
           
            return _context.ProductCategoryGroups
                
                .Where(a => a.StoreId == storeId)
                .Where(g => g.IsDisabled==false)
                .Select(a => new ProductCategoryGroupViewModel { Id = a.Id, Name = a.Name, StoreId = a.StoreId, ArabicName = a.ArabicName })
                .ToList();
        }
        public async Task<IEnumerable<ProductCategoryGroupViewModel>> GetProductCategoryGroupsAsync(int storeId)
        {
            //return _context.ProductCategoryGroup;

            return await _context.ProductCategoryGroups

                .Where(a => a.StoreId == storeId)
                .Where(g => g.IsDisabled == false)
                .Select(a => new ProductCategoryGroupViewModel { Id = a.Id, Name = a.Name, StoreId = a.StoreId ,ArabicName = a.ArabicName})
                .ToListAsync();
        }
        public IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroupsFiltered(string query, int storeId)
        {
            //return _context.ProductCategoryGroup;
            query = query.ToUpper();
            return _context.ProductCategoryGroups
                    .Where(x => x.Name.ToUpper().Contains(query) && x.StoreId == storeId)
                    .Select(a => new ProductCategoryGroupViewModel { Id = a.Id, Name = a.Name, StoreId = a.StoreId, ArabicName = a.ArabicName })
                ;
        }
        public IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroupsFiltered(int query, int storeId)
        {
            return _context.ProductCategoryGroups
                    .Where(x => x.Id == query && x.StoreId == storeId)
                    .Select(a => new ProductCategoryGroupViewModel { Id = a.Id, Name = a.Name, StoreId = a.StoreId, ArabicName = a.ArabicName })

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
            var productCategorygroup = _context.ProductCategoryGroups.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
            productCategorygroup.IsDisabled = true;
            _context.ProductCategoryGroups.Attach(productCategorygroup);
            _context.Entry(productCategorygroup).State = EntityState.Modified;
        }
        public void AddProductCategoryGroup(ProductCategoryGroup optcategory)
        {
            var inDb = _context.ProductCategoryGroups.FirstOrDefault(a =>
                a.Name == optcategory.Name && a.StoreId == optcategory.StoreId);
            if (inDb == null)
            {
                _context.ProductCategoryGroups.Add(optcategory);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    optcategory.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(optcategory);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public async Task AddProductCategoryGroupAsync(ProductCategoryGroup optcategory)
        {
            var inDb =await _context.ProductCategoryGroups.FirstOrDefaultAsync(a =>
                a.Name == optcategory.Name && a.StoreId == optcategory.StoreId);
            if (inDb == null)
            {
                _context.ProductCategoryGroups.Add(optcategory);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    optcategory.Id = inDb.Id;
                    _context.Entry(inDb).CurrentValues.SetValues(optcategory);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public void UpdateProductCategoryGroup(int id, int storeId, ProductCategoryGroup productCategoryGroup)
       {
            _context.ProductCategoryGroups.Attach(productCategoryGroup);
            _context.Entry(productCategoryGroup).State = EntityState.Modified;
        }


    }
}

