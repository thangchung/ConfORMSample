using System;
using System.Diagnostics.Contracts;
using ConfORMSample.Core;
using ConfORMSample.Core.Persistence;
using NHibernate;

namespace ConfORMSample.ConfORM.NHibernate
{
    public class DbContext : RootObject, IDbContext
    {
        public DbContext(string factoryKey)
        {
            Contract.Requires(!string.IsNullOrEmpty(factoryKey), "factoryKey may not be null or empty");

            FactoryKey = factoryKey;
        }

        private ISession Session
        {
            get
            {
                return NHibernateSession.CurrentFor(FactoryKey);
            }
        }

        public void CommitChanges()
        {
            Session.Flush();
        }

        public IDisposable BeginTransaction()
        {
            return Session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Session.Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            Session.Transaction.Rollback();
        }

        public string FactoryKey { get; set; }
    }
}