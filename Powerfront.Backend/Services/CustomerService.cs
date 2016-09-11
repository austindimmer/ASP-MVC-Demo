using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Model;
using Powerfront.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Powerfront.Backend.Services
{
    public class CustomerService
    {
        private IUnitOfWork _uow;

        private IRepository<CustomerRecord> _CustomerRecords;

        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;

            _CustomerRecords = _uow.GetRepository<CustomerRecord>();
        }

        public IEnumerable<AggregateCustomer> GetAllCustomers()
        {
            var customerRecords = _CustomerRecords.GetAll();
            var aggregatedCustomers = customerRecords.GroupBy(c => c.CustomerId);
            List<AggregateCustomer> customersToReturn = new List<AggregateCustomer>();
            foreach (var groupedCustomerRecords in aggregatedCustomers)
            {
                AggregateCustomer ac = new AggregateCustomer();
                ac.CustomerDataRecords = new List<CustomerRecord>();
                ac.CustomerDataRecords.AddRange(groupedCustomerRecords);
                customersToReturn.Add(ac);
            }
            return customersToReturn;

            //return _CustomerRecords.GetAll();
        }

        public AggregateCustomer GetCustomerByID(string id)
        {
            try
            {
                var customerRecordsMatchingId = _CustomerRecords.GetAll().Where(c => c.CustomerId == id);
                AggregateCustomer ac = new AggregateCustomer();
                ac.CustomerDataRecords = new List<CustomerRecord>();
                ac.CustomerDataRecords.AddRange(customerRecordsMatchingId);
                return ac;
            }
            catch (Exception ex)
            {
                throw new Exception("Failure getting Customer", ex);
            }
        }

        public bool CreateCustomer(AggregateCustomer aggregateCustomer)
        {
            try
            {
                foreach (var dataRecord in aggregateCustomer.CustomerDataRecords)
                {
                    _CustomerRecords.Add(dataRecord);
                    _uow.Save();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Failure creating Customer", ex);
            }
        }

        public bool UpdateCustomerRecords(AggregateCustomer aggregateCustomer)
        {
            try
            {
                foreach (var dataRecord in aggregateCustomer.CustomerDataRecords)
                {
                    _CustomerRecords.Update(dataRecord);
                    _uow.Save();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Failure getting Customer", ex);
            }
        }

        public void DeleteCustomerByID(string id)
        {
            try
            {
                List<CustomerRecord> customerRecords = _CustomerRecords.GetAll().Where(c => c.CustomerId == id).Select(r => r).ToList();
                for (int i = 0; i < customerRecords.Count(); i++)
                {
                    var recordToDelete = customerRecords[i];
                    _CustomerRecords.Delete(recordToDelete);
                    _uow.Save();
                }
                //foreach (var customer in customerRecords)
                //{
                //    _Customer.Delete(customer);
                //    _uow.Save();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Failure deleting Customer", ex);
            }
        }
    }
}