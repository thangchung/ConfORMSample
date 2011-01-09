using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using ConfORMSample.Core;
using ConfORMSample.Core.Configuration;
using ConfORMSample.Core.Extensions;
using ConfORMSample.Core.Helpers;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Tool.hbm2ddl;

namespace ConfORMSample.ConfORM
{
    /// <summary>
    /// http://blog.eweibel.net/?p=745
    /// </summary>
    public abstract class ConfORMConfigBuilder : RootObject, IConfigBuilder
    {
        private static IConfigurator _configurator;

        protected IEnumerable<Type> DomainTypes;

        private readonly IEnumerable<string> _assemblies;

        private readonly string _databaseSchema;

        protected ConfORMConfigBuilder(IEnumerable<string> assemblies, string databaseSchema)
            : this(new Configurator(), assemblies, databaseSchema)
        {
        }

        protected ConfORMConfigBuilder(IConfigurator configurator, IEnumerable<string> assemblies, string databaseSchema)
        {
            _configurator = configurator;
            _assemblies = assemblies;
            _databaseSchema = databaseSchema;
        }

        public abstract void GetDatabaseIntegration(IDbIntegrationConfigurationProperties dBIntegration, string connectionString);

        protected abstract HbmMapping GetMapping();

        public Configuration BuildConfiguration(string connectionString, string sessionFactoryName)
        {
            Contract.Requires(!string.IsNullOrEmpty(connectionString), "ConnectionString is null or empty");
            Contract.Requires(!string.IsNullOrEmpty(sessionFactoryName), "SessionFactory name is null or empty");
            Contract.Requires(!string.IsNullOrEmpty(_databaseSchema), "Database Schema is null or empty");
            Contract.Requires(_configurator != null, "Configurator is null");

            return CatchExceptionHelper.TryCatchFunction(
                () =>
                {
                    DomainTypes = GetTypeOfEntities(_assemblies);

                    if (DomainTypes == null)
                        throw new Exception("Type of domains is null");

                    var configure = new Configuration();
                    configure.SessionFactoryName(sessionFactoryName);

                    configure.Proxy(p => p.ProxyFactoryFactory<ProxyFactoryFactory>());
                    configure.DataBaseIntegration(db => GetDatabaseIntegration(db, connectionString));

                    if (_configurator.GetAppSettingString("IsCreateNewDatabase").ConvertToBoolean())
                    {
                        configure.SetProperty("hbm2ddl.auto", "create-drop");
                    }

                    configure.Properties.Add("default_schema", _databaseSchema);
                    configure.AddDeserializedMapping(GetMapping(),
                                                     _configurator.GetAppSettingString("DocumentFileName"));

                    SchemaMetadataUpdater.QuoteTableAndColumns(configure);

                    return configure;
                }, Logger);
        }

        protected IEnumerable<Type> GetTypeOfEntities(IEnumerable<string> assemblies)
        {
            var type = typeof(EntityBase<Guid>);
            var domainTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var realAssembly = Assembly.LoadFrom(assembly);

                if (realAssembly == null)
                    throw new NullReferenceException();

                domainTypes.AddRange(realAssembly.GetTypes().Where(
                    t =>
                    {
                        if (t.BaseType != null)
                            return string.Compare(t.BaseType.FullName,
                                          type.FullName) == 0;
                        return false;
                    }));
            }

            return domainTypes;
        }
    }
}