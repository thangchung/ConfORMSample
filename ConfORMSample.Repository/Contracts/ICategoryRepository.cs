using System;
using ConfORMSample.Core.Persistence;
using ConfORMSample.NewsMgt.Entities;

namespace ConfORMSample.Repository.Contracts
{
    public interface ICategoryRepository : IRepositoryWithTypedId<Category, Guid>
    {
    }
}