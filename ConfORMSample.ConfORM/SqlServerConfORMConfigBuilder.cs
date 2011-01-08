using System;
using System.Collections.Generic;
using System.Data;
using ConfOrm;
using ConfOrm.NH;
using ConfOrm.Patterns;
using ConfOrm.Shop.CoolNaming;
using ConfORMSample.ConfORM.Appliers;
using ConfORMSample.Persistence.Entities;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace ConfORMSample.ConfORM
{
    public class SqlServerConfORMConfigBuilder : ConfORMConfigBuilder
    {
        public SqlServerConfORMConfigBuilder(IEnumerable<string> assemblies)
            : base(assemblies)
        {
        }

        public override void GetDatabaseIntegration(IDbIntegrationConfigurationProperties dBIntegration, string connectionString)
        {
            dBIntegration.Dialect<MsSql2008Dialect>();
            dBIntegration.Driver<SqlClientDriver>();
            dBIntegration.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            dBIntegration.IsolationLevel = IsolationLevel.ReadCommitted;
            dBIntegration.ConnectionString = connectionString;
            dBIntegration.LogSqlInConsole = true;
            dBIntegration.Timeout = 10;
            dBIntegration.LogFormatedSql = true;
            dBIntegration.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";
        }

        protected override HbmMapping GetMapping()
        {
            var orm = new ObjectRelationalMapper();

            orm.Patterns.PoidStrategies.Add(new GuidPoidPattern());

            var patternsAppliers = new CoolPatternsAppliersHolder(orm);
            //patternsAppliers.Merge(new DatePropertyByNameApplier()).Merge(new MsSQL2008DateTimeApplier());
            patternsAppliers.Merge(new ManyToOneColumnNamingApplier());
            patternsAppliers.Merge(new OneToManyKeyColumnNamingApplier(orm));

            var mapper = new Mapper(orm, patternsAppliers);

            var entities = new List<Type>();

            DomainDefinition(orm);
            Customize(mapper);

            entities.AddRange(DomainTypes);

            return mapper.CompileMappingFor(entities);
        }

        private void DomainDefinition(IObjectRelationalMapper orm)
        {
            orm.TablePerClassHierarchy(new[] { typeof(EntityBase<Guid>) });
            orm.TablePerClass(DomainTypes);

            orm.OneToOne<News, Poll>();
            orm.ManyToOne<Category, News>();

            orm.Cascade<Category, News>(Cascade.All);
            orm.Cascade<News, Poll>(Cascade.All);
            orm.Cascade<User, Poll>(Cascade.All);
        }

        private static void Customize(Mapper mapper)
        {
            CustomizeRelations(mapper);
            CustomizeTables(mapper);
            CustomizeColumns(mapper);
        }

        private static void CustomizeRelations(Mapper mapper)
        {
        }

        private static void CustomizeTables(Mapper mapper)
        {
        }

        private static void CustomizeColumns(Mapper mapper)
        {
            mapper.Class<Category>(
                cm =>
                {
                    cm.Property(x => x.Name, m => m.NotNullable(true));
                    cm.Property(x => x.CreatedDate, m => m.NotNullable(true));
                });

            mapper.Class<News>(
                cm =>
                {
                    cm.Property(x => x.Title, m => m.NotNullable(true));
                    cm.Property(x => x.ShortDescription, m => m.NotNullable(true));
                    cm.Property(x => x.Content, m => m.NotNullable(true));
                });

            mapper.Class<Poll>(
                cm =>
                {
                    cm.Property(x => x.Value, m => m.NotNullable(true));
                    cm.Property(x => x.VoteDate, m => m.NotNullable(true));
                    cm.Property(x => x.WhoVote, m => m.NotNullable(true));
                });

            mapper.Class<User>(
                cm =>
                {
                    cm.Property(x => x.UserName, m => m.NotNullable(true));
                    cm.Property(x => x.Password, m => m.NotNullable(true));
                });
        }
    }
}