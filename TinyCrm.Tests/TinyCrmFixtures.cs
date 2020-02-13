using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;

namespace TinyCrm.Tests
{
  public class TinyCrmFixtures
    {
        public TinyCrmDbContext Context_ { get; private set; }
        public IProductService Products { get; private set; }
        public ICustomerService Customer { get; private set; }
       


        public TinyCrmFixtures() 
        {
            
            Context_= new TinyCrmDbContext();
            Products = new ProductService(Context_);
            Customer = new CustomerService(Context_);
        }



    }
}
