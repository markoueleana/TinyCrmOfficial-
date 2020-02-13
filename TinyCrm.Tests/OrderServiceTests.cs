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
        public OrderServiceTests(TinyCrmFixtures fixture)
        {
            context = fixture.Context_;
            services = fixture.Customer;
        }
        [Fact]
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
        }


    }
}
