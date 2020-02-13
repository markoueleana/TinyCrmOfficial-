using System.Linq;
using System.Collections.Generic;

using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System;

namespace TinyCrm.Core.Services
{
      /// <summary>
    /// 
    /// </summary>
    public class ProductService : IProductService
    {
        private TinyCrm.Core.Data.TinyCrmDbContext context;
        private List<Product> ProductsList = new List<Product>();
        public ProductService(Data.TinyCrmDbContext dbContext)
        {
           context=dbContext;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public ApiResult<Product> AddProduct(AddProductOptions options)
        {
            var result = new ApiResult<Product>();
            if (options == null) {
                result.ErrorCode = StatusCode.Bad_Request;
                result.ErrorText = "Null options";
                return result;
            }


            if (string.IsNullOrWhiteSpace(options.Name)) {
                result.ErrorCode = StatusCode.Bad_Request;
                result.ErrorText = "Null or empty name";
                return result;
                
            }

            if (options.Price <= 0) {
                result.ErrorCode = StatusCode.Bad_Request;
                result.ErrorText = "Negative of zero price ";
                return result;
            }

            if (options.ProductCategory ==
              ProductCategory.Invalid) {
                result.ErrorCode = StatusCode.Bad_Request;
                result.ErrorText = "Invalid category";
                return result;
            }

           var product = new Product() {
                Name = options.Name,
                Price = options.Price,
                Category = options.ProductCategory
            };


            result.Data = product;
            ProductsList.Add(product);
            context.Set<Product>()
                .Add(product);
            context.SaveChanges();
            
            return result ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Product UpdateProduct(Guid productId,
            UpdateProductOptions options)
        {
            if (options == null) {
                return null;
            }

            var product = GetProductById(productId);
            if (product == null) { 
                return null; 
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Description = options.Description;
            }

            if (options.Price != null &&
              options.Price <= 0) {
                return null;
            }

            if (options.Price != null) {
                if (options.Price <= 0) {
                    return null;
                } else {
                    product.Price = options.Price.Value;
                }
            }

            if (options.Discount != null &&
              options.Discount < 0) {
                return null;
            }

            return 
                product;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(Guid id)
        {
            var o = new Guid();
            if (o == id) {
                return null;
            } 

            return ProductsList.
                SingleOrDefault(s => s.Id.Equals(id));
        }
        public int SumOfStocks()
        {
            var sum = context.Set<Product>().AsQueryable().
                Sum(c => c.InStock);

            return sum;
        }
    }
}
