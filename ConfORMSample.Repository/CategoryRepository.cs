using System;
using ConfORMSample.ConfORM.Repository;
using ConfORMSample.Persistence.Entities;
using ConfORMSample.Repository.Contracts;

namespace ConfORMSample.Repository
{
    public class CategoryRepository : RepositoryWithTypedId<Category, Guid>, ICategoryRepository
    {
    }
}