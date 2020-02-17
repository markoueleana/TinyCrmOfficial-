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
      IQueryable<OrderProduct> Top10SoldProducts();
    }
}
