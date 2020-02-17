using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TinyCrm.Core.Services
{
    public class ReportService : IReportService
    {
        readonly IProductService product;
        readonly TinyCrmDbContext context;

        public ReportService(TinyCrmDbContext tinycontext)
        {
            context = tinycontext;
            product = new ProductService(context);
        }

        public List<Product> Top10SoldProducts()
        {
            var top10 = context.Set<OrderProduct>()
                 .Where(o=>o.Order.Status
                 .Equals(DeliveryStatus.Delivered))
                 .GroupBy(o => o.Product)
                 .OrderByDescending(p => p.Count())
                 .Take(10)
                 .ToList();

            var listOfProducts = new List<Product>();
           
            foreach (OrderProduct product in top10)
            {
                listOfProducts.Add(product.Product);
            }
            return listOfProducts;

        }
        public List<Customer> TopCustomersByGross()
        {
            var top = context.Set<Customer>()
                .Include(O => O.Orders
                .Where(o => o.Status.Equals(DeliveryStatus.Delivered)))
                .OrderByDescending(o=> o.Orders
                .Select(o => o.OrderProducts
                .Select(p => p.Product.Price))).Take(10);

            return top.ToList();
               
        }

        public decimal TotalSaleOfAPeriod(DateTimeOffset starts, DateTimeOffset ends)
        {

            var orderProductsOfaPeriod = context.Set<OrderProduct>()
                .Where(o => o.Order.CreateDatetime >= starts)
                .Where(o => o.Order.CreateDatetime <= ends)
                .GroupBy(o => o.Product.Price);
            var sum = 0m;

             foreach(Product product in orderProductsOfaPeriod) 
            {
                sum += product.Price;
            
            }

            return sum ;
        }

        public ApiResult<int> NumberOfPendingOrders()
        {
           var pending = context.Set<Order>()
               .Where(o => o.Status == DeliveryStatus.Pending)
               .Count();
           
            if (pending == 0) {

                return ApiResult<int>
                    .CreateUnsuccesfull(StatusCode.NotFound, "no pending items");
                    
            }
            
            return ApiResult<int>.CreateSuccessful(pending);
        }
        public ApiResult<int> NumberOfPCanceldOrders()
        {
            var caneled = context.Set<Order>()
            .Where(o => o.Status == DeliveryStatus.Canceled)
            .Count();

            if (caneled == 0)
            {

                return ApiResult<int>
                    .CreateUnsuccesfull(StatusCode.NotFound, "no canceled items");

            }

              return ApiResult<int>
                    .CreateSuccessful(caneled);
            
        }
        public ApiResult<int> NumberOfDeliveredOrders()
        {
            var delivered = context.Set<Order>()
                .Where(o => o.Status == DeliveryStatus.Delivered)
                .Count();
            if (delivered == 0)
            {

                return ApiResult<int>
                    .CreateUnsuccesfull(StatusCode.NotFound, "no delivered items");

            }
            
                return ApiResult<int>
                    .CreateSuccessful(delivered);
            
        }


    }
}

