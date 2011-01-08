using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ConfORMSample.Core.Constant;
using NHibernate;
using NHibernate.Cfg;

namespace ConfORMSample.ConfORM.NHibernate
{
    public static class NHibernateSession
    {
        #region private variables

        private static IInterceptor _registeredInterceptor;

        private static readonly Dictionary<string, ISessionFactory> SessionFactories = new Dictionary<string, ISessionFactory>();

        private static IConfigBuilder _configBuilder;

        #endregion private variables

        #region Init() overloads

        [CLSCompliant(false)]
        public static Configuration Init(
                IConfigBuilder configBuilder,
                ISessionStorage storage,
                string connectionString
            )
        {
            _configBuilder = configBuilder;

            InitStorage(storage);

            try
            {
                return AddConfiguration(DefaultFactoryKey, connectionString);
            }
            catch
            {
                Storage = null;
                throw;
            }
        }

        #endregion Init() overloads

        public static void InitStorage(ISessionStorage storage)
        {
            Contract.Requires(storage != null, "storage mechanism was null but must be provided");
            Contract.Requires(Storage == null, "A storage mechanism has already been configured for this application");

            Storage = storage;
        }

        [CLSCompliant(false)]
        public static Configuration AddConfiguration(
                string factoryKey,
                string connectionString
            )
        {
            var config = AddConfiguration(
                factoryKey,
                _configBuilder.BuildConfiguration(connectionString, factoryKey)
                );

            return config;
        }

        [CLSCompliant(false)]
        public static Configuration AddConfiguration(
            string factoryKey,
            Configuration cfg)
        {
            var sessionFactory = CreateSessionFactoryFor(cfg);

            return AddConfiguration(factoryKey, sessionFactory, cfg);
        }

        [CLSCompliant(false)]
        public static Configuration AddConfiguration(
            string factoryKey,
            ISessionFactory sessionFactory,
            Configuration cfg)
        {
            Contract.Requires(!SessionFactories.ContainsKey(factoryKey),
                "A session factory has already been configured with the key of " + factoryKey);

            SessionFactories.Add(factoryKey, sessionFactory);

            return cfg;
        }

        public static ISession Current
        {
            get
            {
                Contract.Requires(!IsConfiguredForMultipleDatabases(),
                    "The NHibernateSession.Current property may " +
                    "only be invoked if you only have one NHibernate session factory; i.e., you're " +
                    "only communicating with one database.  Since you're configured communications " +
                    "with multiple databases, you should instead call CurrentFor(string factoryKey)");

                return CurrentFor(DefaultFactoryKey);
            }
        }

        public static void RegisterInterceptor(IInterceptor interceptor)
        {
            Contract.Requires(interceptor != null, "interceptor may not be null");

            _registeredInterceptor = interceptor;
        }

        public static bool IsConfiguredForMultipleDatabases()
        {
            return SessionFactories.Count > 1;
        }

        public static ISession CurrentFor(string factoryKey)
        {
            Contract.Requires(!string.IsNullOrEmpty(factoryKey), "factoryKey may not be null or empty");
            Contract.Requires(Storage != null, "An ISessionStorage has not been configured");
            Contract.Requires(SessionFactories.ContainsKey(factoryKey), "An ISessionFactory does not exist with a factory key of " + factoryKey);

            var session = Storage.GetSessionForKey(factoryKey);

            if (session == null)
            {
                if (_registeredInterceptor != null)
                {
                    session = SessionFactories[factoryKey].OpenSession(_registeredInterceptor);
                }
                else
                {
                    session = SessionFactories[factoryKey].OpenSession();
                }

                Storage.SetSessionForKey(factoryKey, session);
            }

            return session;
        }

        public static void CloseAllSessions()
        {
            if (Storage != null)
                foreach (var session in Storage.GetAllSessions().Where(session => session.IsOpen))
                {
                    session.Close();
                }
        }

        public static void Reset()
        {
            if (Storage != null)
            {
                foreach (var session in Storage.GetAllSessions())
                {
                    session.Dispose();
                }
            }

            SessionFactories.Clear();

            Storage = null;
            _registeredInterceptor = null;
        }

        public static ISessionFactory GetSessionFactoryFor(string factoryKey)
        {
            if (!SessionFactories.ContainsKey(factoryKey))
                return null;

            return SessionFactories[factoryKey];
        }

        public static void RemoveSessionFactoryFor(string factoryKey)
        {
            if (GetSessionFactoryFor(factoryKey) != null)
            {
                SessionFactories.Remove(factoryKey);
            }
        }

        public static ISessionFactory GetDefaultSessionFactory()
        {
            return GetSessionFactoryFor(DefaultFactoryKey);
        }

        public static readonly string DefaultFactoryKey = PersistanceConstants.DEFAULT_FACTORY_SESSION_KEY;

        public static ISessionStorage Storage { get; set; }

        private static ISessionFactory CreateSessionFactoryFor(Configuration cfg)
        {
            return cfg.BuildSessionFactory();
        }
    }
}