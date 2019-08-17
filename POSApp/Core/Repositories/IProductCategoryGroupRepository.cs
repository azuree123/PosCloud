using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
    public interface IProductCategoryGroupRepository
    {
        ProductCategoryGroup GetProductCategoryGroup(int id, int storeId);
        IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroups(int storeId);
        IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroupsFiltered(string query, int storeId);
        IEnumerable<ProductCategoryGroupViewModel> GetProductCategoryGroupsFiltered(int query, int storeId);
        int IsExisting(string productCategoryGroupName, int storeId);
        void DeleteProductCategoryGroup(int id, int storeId);
        void AddProductCategoryGroup(ProductCategoryGroup optcategory);
        void UpdateProductCategoryGroup(int id, int storeId, ProductCategoryGroup productCategoryGroup);
        Task<ProductCategoryGroup> GetProductCategoryGroupAsync(int id, int storeId);
        Task<IEnumerable<ProductCategoryGroupViewModel>> GetProductCategoryGroupsAsync(int storeId);
        Task AddProductCategoryGroupAsync(ProductCategoryGroup optcategory);
        Task<IEnumerable<ProductCategoryGroup>> GetAllProductCategoryGroupsAsyncIncremental(int storeId, DateTime date);
    }
}
