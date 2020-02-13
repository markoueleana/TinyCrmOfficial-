using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System;
using System.Collections.Generic;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        ApiResult< Product> AddProduct(AddProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Product UpdateProduct(Guid productId,
            UpdateProductOptions options);
        List<Product> SearchProduct(
          SearchProductOptions options);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProductById(Guid id);
        public int SumOfStocks();
    }
}
