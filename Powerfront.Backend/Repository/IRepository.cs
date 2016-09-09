using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Powerfront.Backend.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void DeleteAll(IEnumerable<T> entity);
        void Update(T entity);
        bool Any();
    }
}