using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Powerfront.Backend.Repository;
using Powerfront.Backend.Services;
using Powerfront.Frontend.Tests.Mock;

namespace Powerfront.Frontend.Tests.Services
{
    [TestClass]
    public class CustomerServiceTest
    {
        IUnitOfWork _uow;
        CustomerService _CustomerService;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockDataContext>();
            _CustomerService = new CustomerService(_uow);
        }

        [TestCleanup]
        public void TearDown()
        {
            // Clean resources
        }

        [TestMethod]
        public void TestGetCustomerHadar()
        {
            var customer = _CustomerService.GetCustomerByID("1");
            Assert.AreEqual(customer.FirstOrDefault().Property.Name, "Name");
            Assert.AreEqual(customer.FirstOrDefault().Type.Name, "Customer");
            Assert.AreEqual(customer.FirstOrDefault().Value, "Hadar");
        }

        [TestMethod]
        public void TestDeleteCustomerHadar()
        {
            try
            {
                _CustomerService.DeleteCustomerByID("1");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}