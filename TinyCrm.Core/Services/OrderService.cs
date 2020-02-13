using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
   public class OrderService:IOrderService
    {
        private TinyCrmDbContext context;
        public OrderService(TinyCrmDbContext db)
        {
            context = db;
        }
        public ApiResult<Order> CreateOrder(
                CreateOrderOptions orderOptions,
                SearchCustomerOptions customerOptions,
                SearchProductOptions productOptions) 
        {
            var servicesCust = new CustomerService(context);
            var customer = servicesCust.Search(customerOptions);
            var orderCust = customer[0];


            var productServ = new ProductService(context);
            var searchProduct = productServ.SearchProduct(productOptions);
            var productOfOrder = searchProduct[0];


            var results = new ApiResult<Order>();

            if (orderOptions == null) {
                results.ErrorCode = StatusCode.Bad_Request;
                results.ErrorText = "Null options";
                return results;
            }


            var order = new Order()
            {
                DeliveryAddress=orderOptions.DeliveryAddress,
                Customer=orderCust,
                CustomerId=orderCust.Id
               
             };

            var orderproduct = new OrderProduct()
            {
                Order = order,
                Product = productOfOrder,
                OrderId = order.Id,
                ProductId = productOfOrder.Id
            };

            context.Set<Order>().Add(order);
            context.SaveChanges();
            context.Set<OrderProduct>().Add(orderproduct);
            context.SaveChanges();
            results.Data = order;
            results.ErrorCode = StatusCode.Success;
            results.ErrorText = "Ok";
            return results;

        }



        public List<Order> SearchOrder(SearchOrderOptions options)
        {
          
            if (options == null) 
            {
                return null;
            }
            if (options.Id == new Guid()

                && options.CustomerId == new Guid()

                && string.IsNullOrEmpty(options.Customer.VatNumber)){

                return null;
            }

            var orders = context
                    .Set<Order>().
                    AsQueryable();
               
            if (options.Id != new Guid()){

                orders = orders.Where(
                    o => o.Id == options.Id);
            }

            if (options.CustomerId != new Guid()) {

                orders = orders.Where
                    (o => o.CustomerId == options.CustomerId);
            }
/*
            if (!string.IsNullOrWhiteSpace(options.Customer.VatNumber)){
                orders = orders.Where
                    (o => o.Customer.VatNumber==options.Customer.VatNumber);
            }*/
            return orders.ToList();
        }
        
       
    }
}
