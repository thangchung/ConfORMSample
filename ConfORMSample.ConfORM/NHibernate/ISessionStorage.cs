using System.Collections.Generic;
using NHibernate;

namespace ConfORMSample.ConfORM.NHibernate
{
    public interface ISessionStorage
    {
        ISession GetSessionForKey(string factoryKey);

        void SetSessionForKey(string factoryKey, ISession session);

        IEnumerable<ISession> GetAllSessions();
    }
}