﻿using System;
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
    public class OrderServiceTests :
          IClassFixture<TinyCrmFixture>
    {
        private ICustomerService customer_;
        private IProductService products_;
        private TinyCrmDbContext context_;
        private IOrderService order_;
        private ProductServiceTest productServiceTests_;
        private CustomerServiceTests customerServiceTests_;


        public OrderServiceTests(
            TinyCrmFixture fixture)
        {
            productServiceTests_ = new ProductServiceTest(fixture);
            context_ = fixture.Context_;
            customer_ = fixture.Customer;
            products_ = fixture.Products;
            order_ = fixture.Order;
            customerServiceTests_ = new CustomerServiceTests(fixture);
        }

        [Fact]
        public void CreateOrder_Success()
        {
            var customer = customerServiceTests_.CreateCustomer_Success();
            var p1 = productServiceTests_.AddProductService_Success();
            var p2 = productServiceTests_.AddProductService_Success();

            var orderOptions = new CreateOrderOptions
            {
                CustomerId = customer.Id,
                ProductIds = new List<Guid>() { p1.Id, p2.Id }
            };
            var createorder = order_.CreateOrder(orderOptions);

            Assert.True(createorder.Success);


            var orderId = createorder.Data.Id;
            var order = context_.Set<Order>()
                //.Include(o=> o.OrderProducts)
                .Where(o => o.Id == orderId)
                .SingleOrDefault();
            Assert.NotNull(order);

            foreach (var id in orderOptions.ProductIds)
            {
                var op = order.OrderProducts
                    .Where(p => p.ProductId == id)
                    .SingleOrDefault();

                Assert.NotNull(op);
            }
        }

        [Fact]
        public void GetOrder()
        {
            var c1 = new TinyCrmDbContext();

            var customer = new Customer()
            {
                FirstName = "Dimitris",
                VatNumber = $"123{DateTimeOffset.Now:ffffff}"
            };

            c1.Add(customer);
            c1.SaveChanges();

            customer.FirstName = "eleana";
            var cust = customer;
            cust.Id = 0;

            

            var orderId = Guid.Parse("3C516069-F3A1-4CB7-8225-08D7B14E7C62");

            var q = context_
                .Set<OrderProduct>()
                .Where(o => o.OrderId == orderId)
                .Select(o => o.Order.Customer);
            //.ToList();

            var ord = q.ToList();



            var order = context_
                .Set<Order>()
                .ToList()
                .Where(o => o.Id == orderId);


        }
    }
}
