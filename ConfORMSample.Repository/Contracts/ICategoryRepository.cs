using System;
using ConfORMSample.Core.Persistence;
using ConfORMSample.Persistence.Entities;

namespace ConfORMSample.Repository.Contracts
{
    public interface ICategoryRepository : IRepositoryWithTypedId<Category, Guid>
    {
    }
}