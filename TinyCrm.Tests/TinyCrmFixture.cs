﻿using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;

namespace TinyCrm.Tests
{
  public class TinyCrmFixture
    {
        public TinyCrmDbContext Context_ { get; private set; }
        public IProductService Products { get; private set; }
        public ICustomerService Customer { get; private set; }
        public IOrderService Order { get; private set; }

        public IReportService Report { get; private set; }
        public TinyCrmFixture() 
        {
            Report = new ReportService(Context_);
            Order = new OrderService(Context_);
            Context_= new TinyCrmDbContext();
            Products = new ProductService(Context_);
            Customer = new CustomerService(Context_);
            Order = new OrderService(Context_); 
        }



    }
}
