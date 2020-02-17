using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using System.Linq;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
   public interface IReportService
    {
        List<Product> Top10SoldProducts();
        List<Customer> TopCustomersByGross();
        decimal TotalSaleOfAPeriod(DateTimeOffset starts, DateTimeOffset ends);
        ApiResult<int> NumberOfPendingOrders();
        ApiResult<int> NumberOfDeliveredOrders();
        ApiResult<int> NumberOfPCanceldOrders();

    }
}
