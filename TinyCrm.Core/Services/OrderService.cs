using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{

    public class OrderService : IOrderService
    {
        private readonly TinyCrmDbContext context_;

        private readonly ICustomerService customer_;

        private readonly IProductService product_;

        public OrderService(
            TinyCrmDbContext context
            )
        {
            context_ = context;
            customer_ = new CustomerService(context_);
            product_ = new ProductService(context_);

        }
        public ApiResult<Order> CreateOrder(
            CreateOrderOptions createoptions)
        {
            if (createoptions == null)
            {
                return new ApiResult<Order>(
                    StatusCode.Bad_Request, "null options");
            }

            var cresult = customer_
                .GetCustomerById(createoptions.CustomerId);
            if (!cresult.Success)
            {
                return ApiResult<Order>.Create(cresult);
            }

            var order = new Order();

            foreach (var id in createoptions.ProductIds)
            {
                var prodResult = product_
                     .GetProductById(id);

                if (!prodResult.Success)
                {
                    return ApiResult<Order>.Create(
                        prodResult);
                }

                order.OrderProducts.Add(
                    new OrderProduct()
                    {
                        Product = prodResult.Data
                    });
            }

            context_.Add(order);
            cresult.Data.Orders.Add(order);
            context_.SaveChanges();

            return ApiResult<Order>.CreateSuccessful(order);
        }
    }
}
