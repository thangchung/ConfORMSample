using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ConfORMSample.ConfORM.NHibernate;
using ConfORMSample.Core;
using ConfORMSample.Core.Constant;
using ConfORMSample.Core.Persistence;
using ConfORMSample.Persistence.Entities;
using NHibernate;
using NHibernate.Criterion;

namespace ConfORMSample.ConfORM.Repository
{
    public abstract class RepositoryBase<T> : RepositoryWithTypedId<T, int>, IRepository<T> { }

    public abstract class RepositoryWithTypedId<T, TId> : RootObject, IRepositoryWithTypedId<T, TId>
    {
        private IDbContext _dbContext;

        protected virtual ISession GetSession(string factoryKey = "")
        {
            if (string.IsNullOrEmpty(factoryKey))
            {
                return NHibernateSession.CurrentFor(PersistanceConstants.DEFAULT_FACTORY_SESSION_KEY);
            }

            return NHibernateSession.CurrentFor(factoryKey);
        }

        public virtual IDbContext GetDbContext(string factoryKey = "")
        {
            if (string.IsNullOrEmpty(factoryKey))
            {
                return new DbContext(PersistanceConstants.DEFAULT_FACTORY_SESSION_KEY);
            }

            return _dbContext ?? (_dbContext = new DbContext(factoryKey));
        }

        public virtual T Get(TId id)
        {
            return GetSession().Get<T>(id);
        }

        public virtual IList<T> GetAll()
        {
            var criteria = GetSession().CreateCriteria(typeof(T));
            return criteria.List<T>();
        }

        public virtual IList<T> FindAll(IDictionary<string, object> propertyValuePairs)
        {
            Contract.Requires(propertyValuePairs != null && propertyValuePairs.Count > 0,
                "propertyValuePairs was null or empty; " +
                "it has to have at least one property/value pair in it");

            var criteria = GetSession().CreateCriteria(typeof(T));

            foreach (var key in propertyValuePairs.Keys)
            {
                if (propertyValuePairs[key] != null)
                {
                    criteria.Add(Restrictions.Eq(key, propertyValuePairs[key]));
                }
                else
                {
                    criteria.Add(Restrictions.IsNull(key));
                }
            }

            return criteria.List<T>();
        }

        public virtual T FindOne(IDictionary<string, object> propertyValuePairs)
        {
            var foundList = FindAll(propertyValuePairs);

            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }

            return foundList.Count == 1 ? foundList[0] : default(T);
        }

        public virtual void Delete(T entity)
        {
            GetSession().Delete(entity);
        }

        public virtual T SaveOrUpdate(T entity)
        {
            Contract.Requires(!(entity is IEntity<TId>),
                "For better clarity and reliability, Entities with an assigned Id must call Save or Update");

            GetSession().SaveOrUpdate(entity);
            return entity;
        }
    }
}