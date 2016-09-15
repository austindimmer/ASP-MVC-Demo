using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powerfront.Backend.Services
{
    public class PropertyService
    {
        private IUnitOfWork _uow;

        private IRepository<Property> _PropertyRecords;

        public PropertyService(IUnitOfWork uow)
        {
            _uow = uow;

            _PropertyRecords = _uow.GetRepository<Property>();
        }

        public IEnumerable<Property> GetAllProperties()
        {
            var propertyRecords = _PropertyRecords.GetAll();

            return propertyRecords;

        }

        public Property GetPropertyByID(string id)
        {
            try
            {
                Property propertiesMatchingId = _PropertyRecords.GetAll().Where(c => c.PropertyId == id).FirstOrDefault();
                return propertiesMatchingId;
            }
            catch (Exception ex)
            {
                throw new Exception("Failure getting Property", ex);
            }
        }

        public Property CreateProperty(Property propertyToCreate)
        {
            try
            {
                _PropertyRecords.Add(propertyToCreate);
                _uow.Save();
                Property retrievedProperty = GetPropertyByID(propertyToCreate.PropertyId);

                return retrievedProperty;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("Failure creating Property", ex);
            }
        }
    }
}
