using NHibernate.Cfg;

namespace ConfORMSample.ConfORM
{
    public interface IConfigBuilder
    {
        Configuration BuildConfiguration(string connectionString, string sessionFactoryName);
    }
}