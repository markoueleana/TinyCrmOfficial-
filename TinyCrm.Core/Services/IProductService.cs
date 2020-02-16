using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyCrm.Core.Services
{
    public interface IProductService
    {
    
        ApiResult< Product> AddProduct(AddProductOptions options);



        IQueryable<Product> SearchProduct(
          SearchProductOptions options);
    
        ApiResult<Product> GetProductById(Guid id);
        public int SumOfStocks();
    }
}
