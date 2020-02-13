using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using Xunit;
using System.Linq;
using TinyCrm.Core.Services;

namespace TinyCrm.Tests
{
   public class OrderServiceTests: IClassFixture<TinyCrmFixtures>
    {
        private TinyCrmDbContext context;
        private ICustomerService services;
        public IProductService servicesp;
        public IOrderService order;
        public OrderServiceTests(TinyCrmFixtures fixture)
        {
            context = fixture.Context_;
            services = fixture.Customer;
            order = fixture.Order;
        }
/*        [Fact]
        public void CreateOrder()
        {

            // Step 1: Create products
            var poptions = new AddProductOptions()
            {
                Name = "product 1",
                Price = 155.33M,
                ProductCategory = ProductCategory.Computers
            };
            var presult1 = servicesp.AddProduct(poptions);
            Assert.Equal(StatusCode.Success, presult1.ErrorCode);
            poptions = new AddProductOptions()
            {
                Name = "product 2",
                Price = 113.33M,
                ProductCategory = ProductCategory.Computers
            };
            var presult2 = servicesp.AddProduct(poptions);
            Assert.Equal(StatusCode.Success, presult2.ErrorCode);



            // Step 2: Create a new customer
            var options = new CreateCustomerOptions()
            {
                FirstName = "Dimitris",
                VatNumber = $"11{DateTime.Now:fffffff}",
                Email = "dd@Codehub.com",
            };
            var customer = services.Create(options);
            Assert.NotNull(customer);



            // Step 3: Create the order
            var order = new Order()
            {
                DeliveryAddress = "Athens",
                Status =DeliveryStatus.Pending,
                CreateDatetime = DateTimeOffset.Now
            };


            // Step 4: Add products
            order.OrderProducts.Add(
                new OrderProduct()
                {
                    Product = presult1.Data
                });
            order.OrderProducts.Add(
                new OrderProduct()
                {
                    Product = presult2.Data
                });
            customer.Orders.Add(order);
            context.SaveChanges();
            var dbOrder = context
                .Set<Order>()
                .SingleOrDefault(o => o.Id == order.Id);
            Assert.NotNull(dbOrder);
            Assert.Equal(order.DeliveryAddress, dbOrder.DeliveryAddress);
        }*/

        [Fact]
        public void CreateOrders()
        {
            var options = new CreateOrderOptions()
            {
                CreateDatetime=DateTimeOffset.Now,
                DeliveryAddress="Panormou"
                
            };

            Assert.NotNull(options);

            var customeroptions = new SearchCustomerOptions()
            {
                VatNumber= "117001289"
            };


            var productoptions = new SearchProductOptions()
            {
                Id=Guid.Parse("E50612FD-49C3-4C33-93C2-08D7B0C9B3D1"),
                Name = "13-ACamera"
            };


            var newOrder = order
                .CreateOrder(options,
                            customeroptions,
                            productoptions);
            Assert.NotNull(newOrder);
            Assert.Equal(StatusCode.Success, newOrder.ErrorCode);
        }

        [Fact]
        public void SearchOrder_Success() 
        {

            var orderoptions = new SearchOrderOptions()
            { 
                Id=Guid
                .Parse("213C5FFB-5B12-4DDA-F326-08D7B0CA21A9")
            };
            var search = order.SearchOrder(orderoptions);
            var length = search.Count();
            Assert.NotNull(search);
            Assert.True(length!=0);
        }

    }
}
