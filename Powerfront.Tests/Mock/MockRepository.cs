using Powerfront.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Powerfront.Frontend.Tests.Mock
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        public List<T> _context;

        public MockRepository(List<T> ctx)
        {
            _context = ctx;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.AsQueryable();
        }

        public virtual void Add(T entity)
        {
            _context.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public virtual void DeleteAll(IEnumerable<T> entity)
        {
            _context.RemoveAll(s => s == entity);
        }

        public virtual void Update(T entity)
        {
            var entry = _context.Where(s => s == entity).SingleOrDefault();
            entry = entity;
        }

        public virtual bool Any()
        {
            return _context.Any();
        }
    }
}