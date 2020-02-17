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

        public IQueryable<Product> Top10SoldProducts()
        {
            var ListOftheTopProducts=new List<Product>()
           var top10= context.Set<OrderProduct>().OrderByDescending(p => p.ProductId).Take(10);
         
               
            return null;
           
        }

    }
}
