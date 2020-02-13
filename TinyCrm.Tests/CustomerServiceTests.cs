using System;
using Xunit;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Services;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Tests
{
    public class CustomerServiceTests:IClassFixture<TinyCrmFixtures>
    {
        private TinyCrmDbContext context_;
        private ICustomerService service_;


        public CustomerServiceTests(TinyCrmFixtures fixtures)
        {
            context_ = fixtures.Context_;
            service_ = fixtures.Customer;
            
        }

        [Fact]
        public void CreateCustomer_Success()
        {
          

            var options = new CreateCustomerOptions()
            {   
                Email = "dd@dfdd.gr",
                FirstName = "Dimmitrios",
                VatNumber = "117001289"
            };

            var customer = service_.Create(options);

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

            var lookupcustomer = service_.Search(searchcustomer);
            
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

           
            var customer = service_.Create(options);
            Assert.Null(customer);


            //customer with vatnumber count >9
            var options1 = new CreateCustomerOptions()
            {
                Email = "ddd@mark.gr",
                FirstName = "Vivi",
                VatNumber="1234567890"
                
            };
            var customer1 = service_.Create(options1);
            Assert.Null(customer1);

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


            var customer1 = service_.Create(options1);
            Assert.Null(customer1);

            var customer2 = service_.Create(options2);
            Assert.Null(customer2);

            var customer3 = service_.Create(options3);
            Assert.Null(customer3);


        }

        [Fact]
        public void Search_Success()
        {
            
            var options = new SearchCustomerOptions()
            {
                VatNumber = "117001289",
              

            };

            var search = service_.Search(options);
            var length = search.Count();
            Assert.True(length == 1);
        }

        [Fact]
        public void Search_Fail()
        {


            var options = new SearchCustomerOptions();
            
            var search = service_.Search(options);
            
            Assert.Null(search);

            
        
        
        
        }
        


    }
}
