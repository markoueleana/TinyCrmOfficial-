using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
   public interface IOrderService
    {
        List<Order> SearchOrder(SearchOrderOptions options);
        ApiResult<Order> CreateOrder(CreateOrderOptions opionsOrd,
            SearchCustomerOptions optionsCus,
            SearchProductOptions optionsProd);
    }
}
