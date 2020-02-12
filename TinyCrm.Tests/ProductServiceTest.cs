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
    public class ProductServiceTest
    {
        private TinyCrmDbContext context_;

        public ProductServiceTest()
        {
            context_ = new TinyCrmDbContext();
        }

        [Fact]
        public void GetProductByIdService_Success()
        {
            IProductService services = new ProductService(context_);
            
            var getid = services.GetProductById("1");
            var productid = context_.Set<Product>()
                .Where(p => p.Equals(getid)).ToList();

            Assert.Equal(getid, productid.SingleOrDefault()); 

            
        }

        [Fact]
        public void GetProductByIdService_Fail_Null()
        {
            IProductService services = new ProductService(context_);

            var getid1 = services.GetProductById(null);
            Assert.Null(getid1);

            var getid2 = services.GetProductById("");
            Assert.Null(getid2);

            var getid3 = services.GetProductById(" ");
            Assert.Null(getid3);
        }

        [Fact]
        public void AddProductService_Success()
        {
            IProductService services =
                new ProductService(context_);

            var options = new AddProductOptions()
            {
                Id = "2",
                Name="13-ACamera",
                Price = 12.5m,
                Description = "It is Updated",
                ProductCategory = Core.Model.ProductCategory.Cameras

                
            };

            var product = services.AddProduct(options);

            var check = context_.Set<Product>().AsQueryable();
            check = check.Where(p => p.Id == options.Id);
            var length = check.Count();
            Assert.Equal(1, length);
           
            Assert.True(product);


        }
        
        [Fact]
        public void AddProductService_Fail_Null()
        {
             IProductService services =
                new ProductService(context_);




            //when options are null
            var options = new AddProductOptions()
            {
               
            };
            var addproduct = services.AddProduct(options);
            Assert.False(addproduct);

            //when Id is null
            var options2 = new AddProductOptions()
            {
                InStock = 23,
                Name = "The Name",
                Price=3
            };

            var addproduct2 = services.AddProduct(options2);
            Assert.False(addproduct2);

            //when Name is null
            var options3 = new AddProductOptions()
            {
                Id = "12al",
                InStock = 3,
                Price = 12,
                
            };

            var addproduct3 = services.AddProduct(options3);
            Assert.False(addproduct3);

            //when Price < 0
            var options4 = new AddProductOptions()
            {
                Name="The other Name",
                Id = "12al",
                InStock = 3,
                Price = -1,
            };

            var addproduct4 = services.AddProduct(options4);
            Assert.False(addproduct4);

            //when Price=0
            var options5 = new AddProductOptions()
            {
                Name = "The other Name",
                Id = "12al",
                InStock = 3,
                Price = 0,
            };

            var addproduct5 = services.AddProduct(options5);
            Assert.False(addproduct5);
            
            //when ProductCategory is Invalid 
            var options6 = new AddProductOptions()
            {
                Name = "The other Name",
                Id = "12al",
                InStock = 3,
                Price = 30,
                ProductCategory=ProductCategory.Invalid
            };

            var addproduct6 = services.AddProduct(options6);
            Assert.False(addproduct6);

        }


        [Fact]
        public void UpdateProduct_Success()
        {
            IProductService services = new ProductService(context_);
            var options = new UpdateProductOptions()
            {
                Price=12.5m,
                Description="It is Updated",
                Discount=20.4m
            };
            var productUpdate = services.UpdateProduct("1", options);
            Assert.True(productUpdate);

        
        }

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


        }
    }
}
