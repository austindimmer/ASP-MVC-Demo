using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Powerfront.Backend.Repository;

namespace Powerfront.Frontend.Tests.Mock
{
    public class MockUnitOfWork<T> : IUnitOfWork where T : class, new()
    {
        private T _ctx;
        private Dictionary<Type, object> _repositories;

        public MockUnitOfWork()
        {
            _ctx = new T();
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
            {
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
            }

            var entityName = typeof(TEntity).Name;
            var prop = _ctx.GetType().GetProperty(entityName);
            MockRepository<TEntity> repository = null;
            if (prop != null)
            {
                var entityValue = prop.GetValue(_ctx, null);
                repository = new MockRepository<TEntity>(entityValue as List<TEntity>);
            }
            else
            {
                repository = new MockRepository<TEntity>(new List<TEntity>());
            }
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void SetRepositoryData<TEntity>(List<TEntity> data) where TEntity : class
        {
            IRepository<TEntity> repo = GetRepository<TEntity>();

            var mockRepo = repo as MockRepository<TEntity>;
            if (mockRepo != null)
            {
                mockRepo._context = data;
            }
        }

        public void Save()
        {
        }

        public void Dispose()
        {
        }
    }
}