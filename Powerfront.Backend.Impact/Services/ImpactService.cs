using Powerfront.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Powerfront.Backend.Impact;

namespace Powerfront.Backend.Impact.Services
{
    public class ImpactService
    {
        private IUnitOfWork _uow;

        private IRepository<EntityFramework.Impact> _ImpactRecords;

        public ImpactService(IUnitOfWork uow)
        {
            _uow = uow;

            _ImpactRecords = _uow.GetRepository<EntityFramework.Impact>();
        }

        public IEnumerable<EntityFramework.Impact> GetAllImpacts()
        {
            var impactRecords = _ImpactRecords.GetAll();

            return impactRecords;

        }

        public EntityFramework.Impact GetImpactByID(Guid id)
        {
            try
            {
                EntityFramework.Impact impactsMatchingId = _ImpactRecords.GetAll().Where(c => c.ImpactId == id).FirstOrDefault();
                return impactsMatchingId;
            }
            catch (Exception ex)
            {
                throw new Exception("Failure getting Impact", ex);
            }
        }

        public EntityFramework.Impact CreateImpact(EntityFramework.Impact impactToCreate)
        {
            try
            {
                _ImpactRecords.Add(impactToCreate);
                _uow.Save();
                EntityFramework.Impact retrievedImpact = GetImpactByID(impactToCreate.ImpactId);

                return retrievedImpact;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception("Failure creating Impact", ex);
            }
        }

        public void DeleteImpactByID(Guid id)
        {
            try
            {
                List<EntityFramework.Impact> impactRecords = _ImpactRecords.GetAll().Where(c => c.ImpactId == id).Select(r => r).ToList();
                for (int i = 0; i < impactRecords.Count(); i++)
                {
                    var recordToDelete = impactRecords[i];
                    _ImpactRecords.Delete(recordToDelete);
                    _uow.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failure deleting Impact", ex);
            }
        }

        public bool UpdateImpactRecord(EntityFramework.Impact impact)
        {
            try
            {
                    _ImpactRecords.Update(impact);
                    _uow.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Failure getting Customer", ex);
            }
        }
    }
}
