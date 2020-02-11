using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;

namespace TinyCrm.Tests
{
    public class ProductServiceTest
    {
        private TinyCrmDbContext context_;

        public ProductServiceTest()
        {
            context_ = new TinyCrmDbContext();
        }

        [Fact]
        public void PucblicCustomer_Success()
        {
            IProductService services = 
                new ProductService(context_);

            var options = new AddProductOptions()
            {
                Id = "1",
                Description = "koitame odhgies",
                Name = "auto edw",
                InStock = 21,
                ProductCategory = Core.Model.ProductCategory.Cameras,
                Price=12m
            };

            var product = services.AddProduct(options);

            Assert.True(product);


        }
    }
}
