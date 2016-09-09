using Powerfront.Backend.EntityFramework;
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

        private IRepository<Customer> _Customer;

        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;

            _Customer = _uow.GetRepository<Customer>();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _Customer.GetAll();
        }

        public IQueryable<Customer> GetCustomerByID(string id)
        {
            try
            {
                return _Customer.GetAll().Where(c => c.CustomerId == id);
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
                IQueryable<Customer> customerRecords = _Customer.GetAll().Where(c => c.CustomerId == id);
                for (int i = 0; i < customerRecords.Count(); i++)
                {
                    _Customer.Delete(customerRecords.ElementAt(i));
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