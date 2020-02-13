using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;
using System.Linq;

namespace TinyCrm.Tests
{
    public class ProductServiceTest:
        IClassFixture<TinyCrmFixtures>
    {
        private TinyCrmDbContext context_;
        private IProductService services_;

        public ProductServiceTest(TinyCrmFixtures fixture)
        {
            context_ = fixture.Context_;
            services_ = fixture.Products;

        }


        [Fact]
        public void SearchProduct_Success()
        {
            var option = new SearchProductOptions()
            {
                Name = "13-ACamera",
            };

            var results = services_.SearchProduct(option);

            Assert.NotNull(results);
        }

        [Fact]
        public void AddProductService_Success()
        {
          

            var options = new AddProductOptions()
            {
                Name="13-ACamera",
                Price = 12.5m,
                Description = "It is Updated",
                ProductCategory = Core.Model.ProductCategory.Cameras

                
            };

            var product = services_.AddProduct(options);

            Assert.NotNull(product);

       
           
            Assert.Equal(StatusCode.Success,product.ErrorCode);


        }
        
        [Fact]
        public void AddProductService_Fail_Null()
        {
            
            
            //when options are null
            var options = new AddProductOptions()
            {

            };

           var addproduct = services_.AddProduct(options);
           Assert.Equal(StatusCode.Bad_Request, addproduct.ErrorCode);

            //when Id is null
            var options2 = new AddProductOptions()
            {
                InStock = 23,
                Name = "The Name",
                Price=3
            };

            var addproduct2 = services_.AddProduct(options2);
            Assert.Equal(StatusCode.Bad_Request, addproduct2.ErrorCode);

            //when Name is null
            var options3 = new AddProductOptions()
            {
                
                InStock = 3,
                Price = 12,
                
            };

            var addproduct3 = services_.AddProduct(options3);
            Assert.Equal(StatusCode.Bad_Request, addproduct3.ErrorCode);

            //when Price < 0
            var options4 = new AddProductOptions()
            {
                Name="The other Name",
                InStock = 3,
                Price = -1,
            };

            var addproduct4 = services_.AddProduct(options4);
            Assert.Equal(StatusCode.Bad_Request, addproduct4.ErrorCode);

            //when Price=0
            var options5 = new AddProductOptions()
            {
                Name = "The other Name",
                InStock = 3,
                Price = 0,
            };

            var addproduct5 = services_.AddProduct(options5);
            Assert.Equal(StatusCode.Bad_Request, addproduct5.ErrorCode);

            //when ProductCategory is Invalid 
            var options6 = new AddProductOptions()
            {
                Name = "The other Name",
          
                InStock = 3,
                Price = 30,
                ProductCategory=ProductCategory.Invalid
            };

            var addproduct6 = services_.AddProduct(options6);
            Assert.Equal(StatusCode.Bad_Request, addproduct6.ErrorCode);

        }

/*
        [Fact]
        public void UpdateProduct_Success()
        {
            
            var options = new UpdateProductOptions()
            {
                Price=12.5m,
                Description="It is Updated",
                Discount=20.4m
            };
            var productUpdate = services_.UpdateProduct("1", options);
            Assert.True(productUpdate);

        
        }*/
/*
        [Fact]
        public void UpdateProduct_Fail_Null()
        {
            IProductService services = new ProductService(context_);
           
            var options = new UpdateProductOptions()
            {
              
            };
            var productUpdate = services.UpdateProduct("1", options);
            Assert.False(productUpdate);

            //Null Opions
            var options2 = new UpdateProductOptions()
            {

            };
            var productUpdate2 = services.UpdateProduct("3", options2);
            Assert.False(productUpdate);

            //Null ProductId
            var options3 = new UpdateProductOptions()
            {
                Price = 12.5m,
                Description = "It is Updated",
                Discount = 20.4m
            };
            var productUpdate3 = services.UpdateProduct("3", options3);
            Assert.False(productUpdate);


        }*/
    }
}
