using System;
using Xunit;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Tests
{
    public class CustomerServiceTests
    {
        private TinyCrmDbContext context_;

        public CustomerServiceTests()
        {
            context_ = new TinyCrmDbContext();
        }

        [Fact]
        public void CreateCustomer_Success()
        {
            ICustomerService customerService =
                new CustomerService(context_);

            var options = new CreateCustomerOptions()
            {   
                Email = "dd@dfdd.gr",
                FirstName = "Dimmitrios",
                VatNumber = "1170012899"
            };

            var customer = customerService.Create(options);

            Assert.NotNull(customer);
            Assert.Equal(options.Email, customer.Email);
            Assert.Equal(options.VatNumber, customer.VatNumber);
            Assert.Equal(options.FirstName, customer.FirstName);

            var searchcustomer = new SearchCustomerOptions()
            {
                Email = options.Email,
                FistName=options.FirstName,
                VatNumber=options.VatNumber
            
            };

            var lookupcustomer = customerService.Search(searchcustomer);
            
            Assert.True(lookupcustomer.Count() == 1);
            Assert.Single(lookupcustomer);
            

        }

        [Fact]
        public void CreateCustomer_Fail_Null_VatNumber()
        {
            var options = new CreateCustomerOptions()
            {
                Email="ddd@mark.gr",
                FirstName="Vivi"
                
            };

            ICustomerService customerService = new CustomerService(context_);
            var customer = customerService.Create(options);
            Assert.Null(customer);

        }
        [Fact]
        public void CreateCustomer_Fail_Null_Email()
        {

            var options1 = new CreateCustomerOptions()
            {
                VatNumber="fedfe",
                FirstName = "Vivi"

            };

            var options2= new CreateCustomerOptions()
            {
                Email="ekrke.om",
                VatNumber = "fedfe",
                FirstName = "Vivi"

            };

            var options3 = new CreateCustomerOptions()
            {   
                Email="  ",
                VatNumber = "fedfe",
                FirstName = "Vivi"

            };

            ICustomerService customerService = 
                new CustomerService(context_);

            var customer1 = customerService.Create(options1);
            Assert.Null(customer1);

            var customer2 = customerService.Create(options2);
            Assert.Null(customer2);

            var customer3 = customerService.Create(options3);
            Assert.Null(customer3);


        }
        


    }
}
