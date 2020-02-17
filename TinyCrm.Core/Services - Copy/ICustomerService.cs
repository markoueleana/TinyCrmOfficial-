using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
        public IQueryable<Customer> Search(
            SearchCustomerOptions options);
        public ApiResult<Customer> GetCustomerById(int customerId);
        public Customer Create(CreateCustomerOptions options);
    }
}
