using System.Collections.Generic;
using ConfORMSample.Core;
using NHibernate;

namespace ConfORMSample.ConfORM.NHibernate
{
    public class SimpleSessionStorage : RootObject, ISessionStorage
    {
        public SimpleSessionStorage() { }

        public ISession GetSessionForKey(string factoryKey)
        {
            ISession session;

            return !_storage.TryGetValue(factoryKey, out session) ? null : session;
        }

        public void SetSessionForKey(string factoryKey, ISession session)
        {
            _storage[factoryKey] = session;
        }

        public IEnumerable<ISession> GetAllSessions()
        {
            return _storage.Values;
        }

        private readonly Dictionary<string, ISession> _storage = new Dictionary<string, ISession>();
    }
}