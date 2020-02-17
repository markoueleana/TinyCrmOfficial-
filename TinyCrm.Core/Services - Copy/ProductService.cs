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
                Category = options.ProductCategory,
                Description=options.Description
            };


            result.Data = product;
            result.ErrorCode = StatusCode.Success;
            ProductsList.Add(product);
            context.Set<Product>()
                .Add(product);
            context.SaveChanges();
            
            return result ;
        }


        public ApiResult<Product> GetProductById(Guid id)
        {
            var product = context
                .Set<Product>()
                .SingleOrDefault(s => s.Id == id);

            if (product == null)
            {
                return new ApiResult<Product>(
                    StatusCode.NotFound, $"Product {id} not found");
            }

            return ApiResult<Product>.CreateSuccessful(product);
        }


       
        public int SumOfStocks()
        {
            var sum = context.Set<Product>().AsQueryable().
                Sum(c => c.InStock);

            return sum;
        }


        public IQueryable<Product> SearchProduct(
           SearchProductOptions options)
        {

            if (options == null)
            {

                return null;
            }

            if (options.Id== new Guid()

                && string.IsNullOrEmpty(options.Name)

                && options.Category==ProductCategory.Invalid)
            {

                return null;
            }

            var query = context
                    .Set<Product>()
                    .AsQueryable();



            if (options.Id!=new Guid())
            {
                query = query.Where(
                    c => c.Id == options.Id);
            }

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                query = query.Where(
                    c => c.Name == options.Name);
            }

            if (options.Category!=ProductCategory.Invalid)
            {
                query = query
                      .Where(c => c.Category==options.Category);
            }

            return query;

        }
    }
}
