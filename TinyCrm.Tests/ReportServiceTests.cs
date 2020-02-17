using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Services;
using Xunit;

namespace TinyCrm.Tests
{
   
   public class ReportServiceTests:
        IClassFixture<TinyCrmFixture>
    {
        readonly TinyCrmDbContext context_;
        readonly IReportService report_;
        public ReportServiceTests(TinyCrmFixture fixture) 
        {
            context_ = fixture.Context_;
            report_ = fixture.Report;
        }

        [Fact]
        public void Top10SoldProducts_Success() 
        {
            var method = new ReportService(context_);
            var top10 = method.Top10SoldProducts();
            Assert.NotEmpty(top10);
        }

      
        [Fact]
        public void Top10Customers_Success()
        {
            var top10customer = report_.TopCustomersByGross();
            Assert.NotEmpty(top10customer);
            Assert.NotNull(top10customer);
        }
        [Fact]
        public void TotalSale_Success()
        {
            var start = new DateTimeOffset { };
            var ends = new DateTimeOffset { };
            var sales = report_.TotalSaleOfAPeriod(start,ends);
            Assert.False(sales==0);
        }
        [Fact]
        public void TotalSale_Fail()
        {
            var start = new DateTimeOffset { };
            var ends = new DateTimeOffset { };
            var sales = report_.TotalSaleOfAPeriod(start, ends);
            Assert.True(sales == 0);
        }
    }
}
