using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Powerfront.Backend.Repository;
using Powerfront.Backend.Services;
using Powerfront.Frontend.Tests.Mock;
using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Model;

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
        public void CanGetAggregatedCustomerRecords ()
        {
            var customers = _CustomerService.GetAllCustomers();
            var numberOfCustomers = customers.Count();
            Assert.AreEqual(3, numberOfCustomers);
        }

        [TestMethod]
        public void CanUpdateAggregateCustomerRecords()
        {
            try
            {
                var customer = _CustomerService.GetCustomerByID("1");
                CustomerRecord nameRecord = customer.CustomerDataRecords.Where(r => r.Property.Name == "Name").FirstOrDefault();
                Assert.AreEqual(nameRecord.Property.Name, "Name");
                Assert.AreEqual(nameRecord.Type.Name, "Customer");
                Assert.AreEqual(nameRecord.Value, "Hadar");
                CustomerRecord ageRecord = customer.CustomerDataRecords.Where(r => r.Property.Name == "Age").FirstOrDefault();
                Assert.AreEqual(ageRecord.Property.Name, "Age");
                Assert.AreEqual(ageRecord.Type.Name, "Customer");
                Assert.AreEqual(ageRecord.Value, "47");
                nameRecord.Value = "UpdatedName";
                ageRecord.Value = "42";
                _CustomerService.UpdateCustomerRecords(customer);

                AggregateCustomer retreivedCreatedCustomer = _CustomerService.GetCustomerByID("1");
                CustomerRecord updatedNameRecord = customer.CustomerDataRecords.Where(r => r.Property.Name == "Name").FirstOrDefault();
                Assert.AreEqual(updatedNameRecord.Property.Name, "Name");
                Assert.AreEqual(updatedNameRecord.Type.Name, "Customer");
                Assert.AreEqual(updatedNameRecord.Value, "UpdatedName");
                CustomerRecord updatedAgeRecord = customer.CustomerDataRecords.Where(r => r.Property.Name == "Age").FirstOrDefault();
                Assert.AreEqual(updatedAgeRecord.Property.Name, "Age");
                Assert.AreEqual(updatedAgeRecord.Type.Name, "Customer");
                Assert.AreEqual(updatedAgeRecord.Value, "42");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        public void CanCreateNewCustomer()
        {
            try
            {
                AggregateCustomer customer = new AggregateCustomer();
                customer.CustomerId = Guid.NewGuid().ToString();
                customer.CustomerDataRecords = new List<CustomerRecord>();
                var nameRecord = new CustomerRecord
                {
                    CustomerId = "4",
                    TypeId = "1",
                    PropertyId = "1",
                    Value = "NewCustomer",
                    Type = new Backend.EntityFramework.Type { TypeId = "1", Name = "Customer" },
                    Property = new Backend.EntityFramework.Property { PropertyId = "1", Name = "Name" },
                    RecordId = Guid.NewGuid()
                };
                customer.CustomerDataRecords.Add(nameRecord);
                var ageRecord = new CustomerRecord
                {
                    CustomerId = "4",
                    TypeId = "1",
                    PropertyId = "2",
                    Value = "42",
                    Type = new Backend.EntityFramework.Type { TypeId = "1", Name = "Customer" },
                    Property = new Backend.EntityFramework.Property { PropertyId = "2", Name = "Age" },
                    RecordId = Guid.NewGuid()
                };
                customer.CustomerDataRecords.Add(ageRecord);

                var customers = _CustomerService.GetAllCustomers();
                var numberOfCustomers = customers.Count();
                Assert.AreEqual(3, numberOfCustomers);

                _CustomerService.CreateCustomer(customer);
                customers = _CustomerService.GetAllCustomers();
                numberOfCustomers = customers.Count();
                Assert.AreEqual(4, numberOfCustomers);
                AggregateCustomer retreivedCreatedCustomer = _CustomerService.GetCustomerByID("4");
                CustomerRecord createdNameRecord = customer.CustomerDataRecords.Where(r => r.Property.Name == "Name").FirstOrDefault();
                Assert.AreEqual(createdNameRecord.Property.Name, "Name");
                Assert.AreEqual(createdNameRecord.Type.Name, "Customer");
                Assert.AreEqual(createdNameRecord.Value, "NewCustomer");
                CustomerRecord createdAgeRecord = customer.CustomerDataRecords.Where(r => r.Property.Name == "Age").FirstOrDefault();
                Assert.AreEqual(createdAgeRecord.Property.Name, "Age");
                Assert.AreEqual(createdAgeRecord.Type.Name, "Customer");
                Assert.AreEqual(createdAgeRecord.Value, "42");
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreatingNewCustomerWithSameIdThrows()
        {
            AggregateCustomer customer = new AggregateCustomer();
            customer.CustomerId = Guid.NewGuid().ToString();
            customer.CustomerDataRecords[0] = new CustomerRecord
            {
                CustomerId = "1",
                TypeId = "1",
                PropertyId = "1",
                Value = "Hadar",
                Type = new Backend.EntityFramework.Type { TypeId = "1", Name = "Customer" },
                Property = new Backend.EntityFramework.Property { PropertyId = "1", Name = "Name" },
                RecordId = Guid.NewGuid()
            };
            customer.CustomerDataRecords[1] = new CustomerRecord
            {
                CustomerId = "1",
                TypeId = "1",
                PropertyId = "2",
                Value = "47",
                Type = new Backend.EntityFramework.Type { TypeId = "1", Name = "Customer" },
                Property = new Backend.EntityFramework.Property { PropertyId = "2", Name = "Age" },
                RecordId = Guid.NewGuid()
            };
            _CustomerService.CreateCustomer(customer);
        }

        [TestMethod]
        public void CanGetCustomerByID()
        {
            var customer = _CustomerService.GetCustomerByID("1");
            CustomerRecord nameRecord = customer.CustomerDataRecords.Where(r => r.Property.Name == "Name").FirstOrDefault();
            Assert.AreEqual(nameRecord.Property.Name, "Name");
            Assert.AreEqual(nameRecord.Type.Name, "Customer");
            Assert.AreEqual(nameRecord.Value, "Hadar");
            CustomerRecord ageRecord = customer.CustomerDataRecords.Where(r => r.Property.Name == "Age").FirstOrDefault();
            Assert.AreEqual(ageRecord.Property.Name, "Age");
            Assert.AreEqual(ageRecord.Type.Name, "Customer");
            Assert.AreEqual(ageRecord.Value, "47");
        }

        [TestMethod]
        public void CanDeleteCustomerByID()
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