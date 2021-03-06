﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Entities;

namespace Catalog.API.Repositories.Interfaces
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<IEnumerable<Product>> GetProductsByName(string name);
    Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);

    Task Create(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(string id);
  }
}