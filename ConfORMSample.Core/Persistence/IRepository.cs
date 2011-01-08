using System.Collections.Generic;

namespace ConfORMSample.Core.Persistence
{
    public interface IRepository<T> : IRepositoryWithTypedId<T, int> { }

    public interface IRepositoryWithTypedId<T, in TId>
    {
        T Get(TId id);

        IList<T> GetAll();

        IList<T> FindAll(IDictionary<string, object> propertyValuePairs);

        T FindOne(IDictionary<string, object> propertyValuePairs);

        T SaveOrUpdate(T entity);

        void Delete(T entity);

        IDbContext GetDbContext(string factoryKey = "");
    }
}