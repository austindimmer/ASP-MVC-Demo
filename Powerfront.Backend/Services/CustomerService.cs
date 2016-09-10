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

        public IQueryable<CustomerRecord> GetCustomerByID(string id)
        {
            try
            {
                return _CustomerRecords.GetAll().Where(c => c.CustomerId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failure getting Customer", ex);
            }
        }

        public void DeleteCustomerByID(string id)
        {
            try
            {
                IQueryable<CustomerRecord> customerRecords = _CustomerRecords.GetAll().Where(c => c.CustomerId == id);
                for (int i = 0; i < customerRecords.Count(); i++)
                {
                    _CustomerRecords.Delete(customerRecords.ElementAt(i));
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