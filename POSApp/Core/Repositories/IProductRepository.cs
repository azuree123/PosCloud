﻿using System.Collections.Generic;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
   public interface IProductRepository
   {
       IEnumerable<Product> GetAllProducts(int storeId);
       IEnumerable<Product> GetProducts(int productCategoryId);
       Product GetProductById(int id, int storeid);
       void AddProduct(Product product);
       void UpdateProduct(string id, int storeid,Product product);
       void DeleteProduct(string id,int storeid);
       IEnumerable<Product> GetApiProducts();
       Product GetProductByCode(string id, int storeid);
   }
}
